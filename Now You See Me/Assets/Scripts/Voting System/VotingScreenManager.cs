using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using System;
using System.Linq;
using UnityEngine.UI;

/// <summary>
/// This class mananges the opening and closing of the voting screen as well as setting up a list of players to be used by the voting system.
/// </summary>
public class VotingScreenManager : MonoBehaviourPunCallbacks
{
    public GameObject[] playerList; 
    public GameObject[] virusList; 
    public List<GameObject> players;
    public GameObject votingSystemSetup; 
    public GameObject votingScreen;
    public GameObject voteCalledAlert;

    /// <summary>
    /// Ensures that the object the script is attatched to does not get destroyed.
    /// </summary>
    public void Start()
    {
        // Gets the Voting Screen game object
        GameObject canvas = GameObject.Find("Canvas");
        votingScreen = canvas.transform.Find("Voting Screen").gameObject;
        voteCalledAlert = canvas.transform.Find("Vote Called Alert").gameObject;

        // The following section will retrieve the Voting System Setup game object
        // It is done like this because the game object is not active and cannot be directly referenced
        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];

        // Loops through the list of transforms to find the Voting System Setup object
        for (int i = 0; i < objs.Length; i++)
        {
            try
            {
                if (objs[i].hideFlags == HideFlags.None)
                {
                    if (objs[i].name == "Voting System Setup")
                    {
                        votingSystemSetup = objs[i].gameObject;
                    }
                }
            }
            catch (NullReferenceException)
            {
                Debug.Log("The Voting System Setup game object could not be found.");
            }
        }

        DontDestroyOnLoad(this.gameObject);
    }

    /// <summary>
    /// Loads the voting screen for all players in the game.
    /// The method allows the two Remote Procedure Calls to be executed when the report button is pressed.
    /// </summary>
    public void LoadVotingScreen()
    {
        photonView.RPC("GetListofPlayers", RpcTarget.All);
        photonView.RPC("DisplayVotingScreen", RpcTarget.All);
    }

    /// <summary>
    /// Calls an RPC that will close the voting screen.
    /// </summary>
    public void CloseVotingScreen()
    {
        photonView.RPC("HideVotingScreen", RpcTarget.All);
    }

    /// <summary>
    /// A Remote Procedure Call that will display the voting screen to all players in the game.
    /// It also displays an alert to all players that a voting round is about to commence.
    /// </summary>
    [PunRPC]
    private void DisplayVotingScreen()
    {
        // Sets the Voting Screen, Voting System Setup and Vote Called Alert game objects to active
        voteCalledAlert.SetActive(true);
        votingSystemSetup.SetActive(true);
        votingScreen.SetActive(true);
    }

    /// <summary>
    /// A Remote Procedure call that will close the voting screen for all players in the game.
    /// </summary>
    [PunRPC]
    private void HideVotingScreen()
    {
        // Gets the Report Button and Report Pedastool game objects
        GameObject canvas = GameObject.Find("Canvas");
        GameObject reportButton = canvas.transform.GetChild(2).GetChild(3).gameObject;
        GameObject reportPedastool = GameObject.Find("Report Pedastool");      

        // If no serums are left, the ability to vote is disabled
        if (GetComponent<SerumManager>().CheckIfNoSerumsRemaining() == true)
        {
            reportButton.GetComponent<Button>().interactable = false;
            reportPedastool.SetActive(false);
        }

        // Voting Screen and Voting System Setup game objects are deactivated so the screen can be closed
        votingSystemSetup.SetActive(false);
        votingScreen.SetActive(false);
    }

    /// <summary>
    /// Retrieves the list of players who are in the game.
    /// </summary>
    [PunRPC]
    private void GetListofPlayers()
    {
        // Gets all student and virus players
        playerList  = GameObject.FindGameObjectsWithTag("Player");
        virusList = GameObject.FindGameObjectsWithTag("Virus");

        foreach (GameObject player in playerList)
            {
                // Ensures student players don't get destoyed
                DontDestroyOnLoad(player);
            }

        foreach (GameObject virus in virusList)
        {
            // Ensures virus players don't get destoyed
            DontDestroyOnLoad(virus);
        }
    }

    /// <summary>
    /// Puts each student and virus player into a collective list of all of the players in the game.
    /// </summary>
    public void SetupPlayers()
    {
        // Loops through all student players and adds them to the list
        foreach (GameObject player in playerList)
        {
            players.Add(player);
            Debug.Log("PLAYER ADDED");
        }

        // Loops through all virus players and adds them to the list
        foreach (GameObject virus in virusList)
        {
            players.Add(virus);
            Debug.Log("PLAYER ADDED");
        }

        // Orders the players in the list in alphabetical order according to their nickname
        players.OrderBy(player => player.name).ToList();

    }

    /// <summary>
    /// Returns the list of players in the game.
    /// </summary>
    /// <returns></returns>
    public List<GameObject> GetPlayersList()
    {
        List<GameObject> playerListCopy = new List<GameObject>();

        // Loops through all players
        foreach (GameObject p in players)
        {
            // Ensures there are no null references returned in the list
            if (!(p == null))
            {
                // Player is added to a copy of the list, to make sure there are no null references
                playerListCopy.Add(p);
            }
        }
        return playerListCopy;
    }

}
