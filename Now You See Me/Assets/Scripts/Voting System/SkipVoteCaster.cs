using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

/// <summary>
/// This class manages the actual process of casting a vote for a particular player.
/// </summary>
public class SkipVoteCaster : MonoBehaviourPunCallbacks
{
    [SerializeField] private VotingManager manager;
    private int count;
    private GameObject currentButton;

    /// <summary>
    /// Cast a vote on behalf of the player.
    /// </summary>
    /// <param name="button"></param>
    public void CastVote(GameObject button)
    {
        // Reference to the button selected
        currentButton = button;

        // Gets the game object containing the number of votes to skip
        var text = currentButton.transform.GetChild(1);
        string temp = text.GetComponent<Text>().text; 
        count = int.Parse(temp);

        // Checks if player has not voted, if not their vote is cast
        if (manager.GetVote() == false) 
        {
            manager.VoteCasted();
            photonView.RPC("SkipVote", RpcTarget.All);
        }
    }

    /// <summary>
    /// Resets the number of votes cast to 0.
    /// </summary>
    public void ResetCount()
    {
        count = 0;
    }

    /// <summary>
    /// Updates the number of votes for the skip button for all players.
    /// </summary>
    [PunRPC]
    public void SkipVote()
    {
        count++;
        GameObject skipbutton = GameObject.FindGameObjectWithTag("SkipButton");
        var text = skipbutton.transform.GetChild(1);
        text.GetComponent<Text>().text = count.ToString();
    }

}
