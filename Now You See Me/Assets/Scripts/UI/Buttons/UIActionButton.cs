using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the button on screen for the action ability e.g. push, kill
/// </summary>
public class UIActionButton : UIGeneralButton
{
    public static UIActionButton instance;
    // The player that will execute their functions when this button is pressed
    // The current player we have reference to
    private PlayerController _currentPlayer;

    /// <summary>
    /// When this button is pressed call <see cref="PerformAction"/> of <see cref="PlayerController"/>
    /// which is the <c>currentPlayer</c> we are referencing.
    /// The action is different depending on the actual type i.e. student, virus - kills, or ghost - pushes
    /// </summary>
    public override void ButtonPressed()
    {   
        if (this._currentPlayer == null) { return; } 
        this._currentPlayer.PerformAction();
    }

    /// <summary>
    /// Set the current player we have reference to
    /// </summary>
    /// <param name="pc">The current player controller</param>
    public void SetCurrentTarget(PlayerController pc)
    {
        this._currentPlayer = pc;
    }

    /// <summary>
    /// Set the colour and text of the button for a Virus
    /// </summary>
    public void DisplayVirus()
    {
        SetColour(new Color(0.98f, 0.675f, 0.675f));
        SetText("Kill");
    }

    /// <summary>
    /// Set the colour and text of the button for a Ghost
    /// </summary>
    public void DisplayGhost()
    {
        SetColour(new Color(0.675f, 0.894f, 0.98f));
        SetText("Push");  
    }

    /// <summary>
    /// Set the colour and text of the button for a Student
    /// </summary>
    public void DisplayStudent()
    {
        SetColour(new Color(0.98f, 0.839f, 0.675f));
        SetText("Interact");  
    }

    /// <summary>
    /// Set the colour and text of the button for a Computer Scientist
    /// </summary>
    public void DisplayComputerScience()
    {
        SetColour(new Color(0.98f, 0.839f, 0.675f));
        SetText("Cameras");
    }

    /// <summary>
    /// Set <c>instance</c> to this object
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        instance = this;
    }
}