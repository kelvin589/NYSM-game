using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Add to UI button to teleport the player back to spawn when pressed
/// (calling on PlayerControllers.UnStick)
/// </summary>
public class UIStuckButton : UIGeneralButton
{
    public static UIStuckButton instance;
    private PlayerController _currentPlayer;

    /// <summary>
    /// Unstick the player (teleport them to spawn) when they click the button
    /// </summary>
    public override void ButtonPressed()
    {   
        if (this._currentPlayer == null) { return; } 
        this._currentPlayer.UnStick();
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
    /// Initialise this button and enable it
    /// It is always available
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        instance = this;
        instance.SetHasTarget(true);
    }
}