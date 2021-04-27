using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

/// <summary>
/// This class manages the actual process of casting a vote for a particular player.
/// </summary>
public class PlayerVoteCaster : MonoBehaviourPunCallbacks
{
    [SerializeField] private VotingManager manager;
    private int count;
    private GameObject currentButton;
    public GameObject Buttonclick;

    /// <summary>
    /// Cast a vote on behalf of the player.
    /// </summary>
    /// <param name="button"></param>
    public void CastVote(GameObject button)
    {
        // Reference to the button selected
        currentButton = button;

        // Gets the game object containing the number of votes
        var text = currentButton.transform.GetChild(2);
        string temp = text.GetComponent<Text>().text; 
        count = int.Parse(temp);

        // Checks if player has not voted, if not their vote is cast
        if (manager.GetVote() == false)
        {
            manager.VoteCasted();
            photonView.RPC("sendVote", RpcTarget.All);
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
    /// Updates the number of votes for particular button for all players.
    /// </summary>
    [PunRPC]
    public void sendVote()
    {
        count++;
        var text = Buttonclick.transform.GetChild(2);
        text.GetComponent<Text>().text = count.ToString();

    }

}
