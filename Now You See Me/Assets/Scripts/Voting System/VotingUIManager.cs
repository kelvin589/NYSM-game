using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;

/// <summary>
/// This class manages the voting screen user interface.
/// It control the creation of the UI and also modifies it at certain points in the game.
/// </summary>
public class VotingUIManager : MonoBehaviourPunCallbacks
{
    public GameObject[] playerList;
    public List<GameObject> players;
    public GameObject votingManager;
    [SerializeField]public bool screenDrawn = false;

    /// <summary>
    /// Checks if the screen has already been drawn/created.
    /// The purpose of this is to make sure that the layout of the voting screen interface is consistent throughout the game.
    /// </summary>
    public void CheckIfScreenDrawn()
    {
        if (screenDrawn == false)
        {
            photonView.RPC("CreateUI", RpcTarget.All);
        }

        // Calls method to create the voting screen layout
        photonView.RPC("StopScreenBeingRedrawn", RpcTarget.All); 
    }

    /// <summary>
    /// A Remote Procedure Call that prevents the screen layout being changed for each connected player.
    /// </summary>
    [PunRPC]
    public void StopScreenBeingRedrawn()
    {
        // Will be set to true for the duration of the game
        screenDrawn = true;
        Debug.Log("The voting screen will not be drawn anymore.");
    }

    /// <summary>
    /// A Remote Procedure Call that will create the voting screen user interface.
    /// </summary>
    [PunRPC]
    public void CreateUI()
    {
        GameObject[] Votingbuttons = GameObject.FindGameObjectsWithTag("Votebutton");
        int count = 0;

        // Gets the voting manager object so it can call methods from the scripts attatched to it
        votingManager = GameObject.Find("Voting Manager");
        votingManager.GetComponent<VotingScreenManager>().SetupPlayers();
        // Retrives the list of players that are in the game
        players = votingManager.GetComponent<VotingScreenManager>().GetPlayersList();

        // Sorts the list of players in ascending order according to their name so that the buttons appear in the same order on each player's screen
        var sortedPlayerList = players.OrderBy(player => player.transform.GetChild(0).GetChild(0).GetComponent<Text>().text).ToList();

        // Loops through the list of players and creates the voting buttons on the voting screen
        while (count < sortedPlayerList.Count)
        {
            foreach (GameObject button in Votingbuttons)
            {
                try
                    {
                        // Set the image and text for the button to the sprite of the player character and the nickname of the player respectively
                        GameObject playerFS = sortedPlayerList[count]; 
                        var image = button.transform.GetChild(0); 
                        var text = button.transform.GetChild(1); 

                        image.GetComponent<Image>().sprite = playerFS.GetComponent<SpriteRenderer>().sprite;
                        text.GetComponent<Text>().text = playerFS.transform.GetChild(0).GetChild(0).GetComponent<Text>().text;
                        count++;
                    }

                    catch (ArgumentOutOfRangeException)
                    {
                        // Prevents more buttons than are required from appearing
                        button.SetActive(false);
                        Debug.Log("NO MORE PLAYERS FOUND");
                    }
            }
        }
    }

    /// <summary>
    /// A Remote Procedure Call that 
    /// </summary>
    [PunRPC]
    public void PrepareScreenForNextRound()
    {
        GameObject[] Votingbuttons = GameObject.FindGameObjectsWithTag("Votebutton");
        GameObject[] skipButton = GameObject.FindGameObjectsWithTag("SkipButton");

        // Loops through the voting buttons to reset their vote counts to 0
        foreach (GameObject button in Votingbuttons)
        {
            var text = button.transform.GetChild(2);
            text.GetComponent<Text>().text = "0";
            button.GetComponent<PlayerVoteCaster>().ResetCount();
        }

        // Resets the skip vote count to 0
        foreach (GameObject button in skipButton)
        {
            var text = button.transform.GetChild(1);
            text.GetComponent<Text>().text = "0";
            button.GetComponent<SkipVoteCaster>().ResetCount();
        }

        // Reset the voting timer and voting permissions
        GetComponent<VotingCountdownTimer>().ResetTimer();
        GetComponent<VotingManager>().ResetAbilityToVote();
    }
}
