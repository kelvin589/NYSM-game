using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for loading the main menu of the game.
/// </summary>
public class MainMenu : MonoBehaviour
{

    /// <summary>
    /// Loads the main menu scene when called.
    /// </summary>
    /// <param name="level"></param>
    public void LoadScene(int level)
    {
        Application.LoadLevel(level);
    }
}