using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/// <summary>
/// This class manages the ability of players to vote.
/// </summary>
public class VotingManager : MonoBehaviourPunCallbacks
{
    // Determines if the player has voted or not
    [SerializeField] private bool vote = false;

    /// <summary>
    /// Indicates that the player has cast their vote for the round.
    /// </summary>
    public void VoteCasted()
    {
        vote = true;
        Debug.Log("PLAYER HAS VOTED");
    }

    /// <summary>
    /// Returns whether the player has voted or not.
    /// </summary>
    /// <returns></returns>
    public bool GetVote()
    {
        checkIfPlayerEliminated();
        return vote;
    }

    /// <summary>
    /// Checks if the player has already been eliminated in a previous round.
    /// This prevents an eliminated player from casting a vote.
    /// </summary>
    public void checkIfPlayerEliminated()
    {
        // List of ghost players (eliminated players)
        GameObject[] eliminatedPlayers = GameObject.FindGameObjectsWithTag("Ghost");
        int playerID = 0;

        // Loops through the eliminated players
        foreach (GameObject ghost in eliminatedPlayers)
        {
            // Ensures no null reference
            if (ghost == null)
            {
                Debug.Log("PLAYER NOT FOUND IN LIST OF ELIMINATED PLAYERS");
            }
            else
            {
                // Checks if the ghost is controlled by the player by checking if the PhotonViews are the same
                playerID = ghost.GetComponent<PhotonView>().ViewID;
                PhotonView playerView = PhotonView.Find(playerID);
                if (playerView.IsMine)
                {
                    // Stops player from voting while eliminated
                    VoteCasted();
                }
            }
        }
    }


    /// <summary>
    /// Sets the ability to vote so that players can cast a vote in the next round. 
    /// </summary>
    public void ResetAbilityToVote()
    {
        vote = false;
    }
}
