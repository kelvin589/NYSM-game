using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A grab button which should only be accessible to the virus.
/// Calls virus controller to grab a task or a dead body
/// </summary>
public class UIGrabButton : UIGeneralButton
{
    public static UIGrabButton instance;
    // The player that will execute their functions when this button is pressed
    // The current virus player we have reference to
    private VirusController _currentPlayer;

    /// <summary>
    /// Set the current player we have reference to
    /// </summary>
    /// <param name="pc">The current player controller (virus)</param>
    public void SetCurrentTarget(VirusController pc)
    {
        this._currentPlayer = pc;
    }

    /// <summary>
    /// When this button is pressed call <see cref="GrabObject"/> of <see cref="VirusController"/>
    /// which essentially picks up an object when button is pressed
    /// </summary>
    public override void ButtonPressed()
    {   
        if (this._currentPlayer == null) { return; } 
        this._currentPlayer.GrabObject();
    }

    /// <summary>
    /// Set <c>instance</c> to this object
    /// Initialise the colour and text to something appropriate
    /// Set active to false to disable the button as it is not appropriate for every player (only for virus)
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        instance = this;
        SetColour(new Color(0.851f, 0.851f, 0.851f));
        SetText("Grab");
        SetActive(false);
    }
}