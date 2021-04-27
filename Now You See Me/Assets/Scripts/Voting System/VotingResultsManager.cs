using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

/// <summary>
/// This class handles the results of a voting round.
/// It will determine the result of a voting round, display the result to the players and remove a player if necessary.
/// </summary>
public class VotingResultsManager : MonoBehaviourPunCallbacks
{
    /// <summary>
    /// Counts all the votes and kills someone or skips the round depending on majoirty.
    /// </summary>
    public void CheckVotingResult()
    {
        GameObject[] votingButtons = GameObject.FindGameObjectsWithTag("Votebutton");
        GameObject skipButton = GameObject.FindGameObjectWithTag("SkipButton");

        // Puts list of voting buttons in order so that the corresponding elements in the lists of nicknames and counts are associated with the same voting button
        Array.Sort(votingButtons, delegate (GameObject button1, GameObject button2)
        {
            return button1.name.CompareTo(button2.name);
        });

        // Stores nicknames of all palyers
        string[] playerNicknames = new string[8];
        // Stores how many vote each player received
        int[] votesForPlayers = new int[8];
        // Used to place nicknames and vote numbers in the position that corresponds to the position of their button in the list of buttons
        int count = 0;

        // Loops through all of the player voting buttons to get the nicknames and vote counts associated with them
        foreach (GameObject currentButton in votingButtons)
        {
            // Gets the nickname and vote count associated with a button
            var nickname = currentButton.transform.GetChild(1);
            var voteCount = currentButton.transform.GetChild(2);

            string tempName = nickname.GetComponent<Text>().text;
            string tempCount = voteCount.GetComponent<Text>().text;
            int numberOfVotes = int.Parse(tempCount);

            // Adds the vote count and nicknames to their respective lists
            votesForPlayers[count] = numberOfVotes;
            playerNicknames[count] = tempName;

            count++;

        }

        // Stores the highest vote count for the voting round
        int Highestcount = votesForPlayers[0];
        // Tracks how many times the highest number of votes occurs (used to check for a draw)
        int occurencesOfHighestCount = 1;

        // If the majority of votes are to skip, then a skip is declared
        var numOfSkipVotes = skipButton.transform.GetChild(1);
        if (int.Parse(numOfSkipVotes.GetComponent<Text>().text) > votingButtons.Length / 2)
        {
            photonView.RPC("DisplayVotingResult", RpcTarget.All, "skip", " ");
            Debug.Log("Majority have skipped");
            return;
        }

        // Loops through the list of vote counts to get the highest number and how many time it occurs
        for (int i = 1; i < votesForPlayers.Length; i++)
        {
            if (votesForPlayers[i] > Highestcount)
            {
                Highestcount = votesForPlayers[i];
                occurencesOfHighestCount = 1;
            }

            else if (votesForPlayers[i] == Highestcount)
            {
                occurencesOfHighestCount += 1;
            }
        }

        // If multiple occurences of the highest count, then a draw (skip) is declared
        if (occurencesOfHighestCount > 1)
        {
            photonView.RPC("DisplayVotingResult", RpcTarget.All, "skip", " ");
            Debug.Log("No majoirty, no one has been eliminated");
            return;
        }
        // If the vote is not skipped or their is no draw, the only other result is an elimination
        else
        {
            int indexOfPlayerName = System.Array.IndexOf(votesForPlayers, Highestcount); //get the player name who got the most votes
            photonView.RPC("DisplayVotingResult", RpcTarget.All, "elimination", playerNicknames[indexOfPlayerName]);

            EliminatePlayer(playerNicknames[indexOfPlayerName]);
            Debug.Log(playerNicknames[indexOfPlayerName] + " was killed");
        }
    }

    /// <summary>
    /// Determines which alert message will be displayed on the screen after voting is finished.
    /// </summary>
    /// <param name="result"></param>
    /// <param name="playerName"></param>
    [PunRPC]
    public void DisplayVotingResult(string result, string playerName)
    {
        GameObject canvas = GameObject.Find("Canvas");

        if (result == "elimination")
        {
            GameObject eliminationAlert = canvas.transform.Find("Elimination Alert").gameObject;
            GameObject nicknameTextObject = eliminationAlert.transform.GetChild(0).gameObject;

            // Gets the text component and modifies the alert message to include the eliminated player's name
            string nicknameText = nicknameTextObject.GetComponent<Text>().text;
            nicknameText = playerName + " Has Been Eliminated! \n \n They were not the virus...";
            nicknameTextObject.GetComponent<Text>().text = nicknameText; ;

            // Displays the elimination alert message
            eliminationAlert.SetActive(true);
        }
        else if (result == "skip")
        {
            GameObject skipAlert = canvas.transform.Find("Skip Alert").gameObject;

            // Displays the skip alert message
            skipAlert.SetActive(true);
        }

    }

    /// <summary>
    /// Finds the player that needs to be eliminated from the game.
    /// </summary>
    /// <param name="name"></param>
    public void EliminatePlayer(string name)
    {
        GameObject votingManager = GameObject.Find("Voting Manager");
        List<GameObject> listOfPlayers = votingManager.GetComponent<VotingScreenManager>().GetPlayersList();
        var sortedPlayerList = listOfPlayers.OrderBy(player => player.transform.GetChild(0).GetChild(0).GetComponent<Text>().text).ToList();
        int playerToRemoveID = 0;
        string typeOfPlayerToRemove = " ";
        var buttonToDeactivate = " ";

        // Loops through the list of players to find the player who is going to be eliminated
        foreach (GameObject p in sortedPlayerList)
        {
            // Ensures the is not a null reference
            if (p == null)
            {
                Debug.Log("PLAYER ALREADY DESTROYED");
            }
            else if (p.transform.GetChild(0).GetChild(0).GetComponent<Text>().text == name)
            {
                // Retireves the PhotonView for the player
                playerToRemoveID = p.GetComponent<PhotonView>().ViewID;
                // Name associated with the button is the nickname of the player to be eliminated
                buttonToDeactivate = name;

                // Checks what type of player is going to be removed from the game
                if (p.tag == "Virus")
                {
                    typeOfPlayerToRemove = "Virus";
                }
                else
                {
                    typeOfPlayerToRemove = "Student";
                }
            }
        }

        photonView.RPC("RemovePlayerFromGame", RpcTarget.All, playerToRemoveID, typeOfPlayerToRemove);
        photonView.RPC("DeactivateEliminatedPlayerButton", RpcTarget.All, buttonToDeactivate);
        votingManager.GetComponent<SerumManager>().photonView.RPC("DecreaseSerumCount", RpcTarget.All);

    }

    /// <summary>
    /// Deactivates the button associated with a player that has been eliminated.
    /// This is so no one can interact with the button and vote for an eliminated player.
    /// </summary>
    /// <param name="name"></param>
    [PunRPC]
    public void DeactivateEliminatedPlayerButton(string name)
    {
        GameObject[] votingButtons = GameObject.FindGameObjectsWithTag("Votebutton");

        // Loops through the buttons list
        foreach (GameObject button in votingButtons)
        {
            // Get the name associated with the button
            var nickname = button.transform.GetChild(1);
            string tempName = nickname.GetComponent<Text>().text;

            // If the correct button is found, then make it so it can't be interacted with
            if (tempName == name)
            {
                button.GetComponent<Button>().interactable = false;
            }

        }
    }

    /// <summary>
    /// Eliminates a player from the game and turns them into a ghost.
    /// </summary>
    /// <param name="playerID"></param>
    /// <param name="typeOfPlayer"></param>
    [PunRPC]
    public void RemovePlayerFromGame(int playerID, string typeOfPlayer)
    {
        PhotonView playerView = PhotonView.Find(playerID);

        // Ensures there is no null reference
        if (playerView.gameObject == null)
        {
            Debug.Log("No player found");
        }
        else
        {
            // Gets the game object associated with the PhotonView (the player)
            GameObject playerGameObject = playerView.gameObject;
            // Gets the current position of the player in the game
            var pos = playerGameObject.transform.position;

            // The player is removed and will control a ghost for the rest of the game
            if (playerView.IsMine)
            {
                PhotonNetwork.Destroy(playerView);
                PhotonNetwork.Instantiate("Ghost2", pos, Quaternion.identity);
            }
        }

        // If the virus player is removed from the game, then declare a win for the students
        if (typeOfPlayer == "Virus")
        {
            SceneManager.LoadScene("Students Win");  
        }
    }
}
