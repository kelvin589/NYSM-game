using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The sole purpose is to close the application when the quit button is clicked.
/// </summary>
public class QuitGame : MonoBehaviour
{

    /// <summary>
    /// Closes the game.
    /// </summary>
    public void exitGame()
    {
        Application.Quit();
        Debug.Log("Quitting");
    }

}

