using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

/// <summary>
/// This class manages the minimap that appears at the top right of the game UI.
/// </summary>
public class MinimapManager : MonoBehaviourPunCallbacks
   
{
    /// <summary>
    /// Called before the first frame update.
    /// Displays the minimap icon on the screen.
    /// </summary>
    void Start()
    {
        displayPlayerMinimapIcon();
    }

    /// <summary>
    /// Displays the players icon on the minimap.
    /// The icon is displayed locally so each player can only see their minimap icon.
    /// </summary>
    void displayPlayerMinimapIcon()
    {
        // Gets all photon views
        var photonViews = FindObjectsOfType<PhotonView>();
        foreach (var view in photonViews)
        {
            var viewMine = view.IsMine;
            var playerView = view.Owner;
           
            // Checks the view has an owner and matches the player's
            if ((playerView != null) && (viewMine == true))
            {
                // Check the game object is the player controller object
                var playerObject = view.gameObject;
                if (playerObject.name.Contains("Controller(Clone)"))
                {
                    // Gets the minimap icon object for the specific player controller
                    GameObject player = playerObject.transform.GetChild(1).gameObject;

                    // Retrive the colour of the SpriteRenderer component (icon image) and change its alpha value so it can be seen
                    Color sr = player.GetComponent<SpriteRenderer>().color;
                    sr.a = 1f;
                    // Applies changes
                    player.GetComponent<SpriteRenderer>().color = sr;
                }



            }
        }

    }

}
