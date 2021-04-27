using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/// <summary>
/// This  class handles the serums that can be administered to players when they are voted off, which will eliminate them from the game.
/// There is only a limited number allowed in each game, once there are none left then voting cannot continue.
/// </summary>
public class SerumManager : MonoBehaviourPunCallbacks
{
    private int numOfSerumsRemaining = 3;

    /// <summary>
    /// Checks if there are any serums left by checking that the amount is not zero.
    /// </summary>
    /// <returns></returns>
    public bool CheckIfNoSerumsRemaining()
    {
        if (numOfSerumsRemaining == 0)
        {
            // No serums left
            return true;
        } 
        else
        {
            // Serums remaining
            return false;
        }
    }

    /// <summary>
    /// Decreases the serum count when a serum is administered (when a voting round ends).
    /// It will remove the serum from the voting screen UI so players know how many they have left.
    /// </summary>
    [PunRPC]
    public void DecreaseSerumCount()
    {
        GameObject[] serumIcons = GameObject.FindGameObjectsWithTag("Serum");

        // Removes a serum from the voting screen as long as there are still serums available
        // Serums are removed from right to left on the voting screen
        if (CheckIfNoSerumsRemaining() == false)
        {
            // Removes far right serum on the voting screen
            if (numOfSerumsRemaining == 3)
            {
                GameObject serumIconToRemove = serumIcons[2];
                serumIconToRemove.SetActive(false);
            }
            // Removes middle serum on the voting screen
            else if (numOfSerumsRemaining == 2)
            {
                GameObject serumIconToRemove = serumIcons[1];
                serumIconToRemove.SetActive(false);
            }
            // Removes far left serum on the voting screen
            else if (numOfSerumsRemaining == 1)
            {
                GameObject serumIconToRemove = serumIcons[0];
                serumIconToRemove.SetActive(false);
            }

            // Decrement the number of serums left
            numOfSerumsRemaining -= 1;
        }
    }

}
