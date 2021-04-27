using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI Report button to start voting
/// Simply enable or disable. It is called by votingscreenmanger
/// </summary>
public class UIReportButton : UIGeneralButton
{
    public static UIReportButton instance;

    // Do nothing because votingscreenmanager load voting screen called when 
    // button pressed
    public override void ButtonPressed()
    {   

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