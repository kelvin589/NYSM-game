using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/// <summary>
/// Define the behaviour of a computer science student. This student can view the cameras from their phone.
/// </summary>
[DisallowMultipleComponent]
public class CompSciController : PlayerController
{
    private bool _state;
    private GameObject _canvas;
    private GameObject _phonePanel;

    /// <summary>
    /// Get what role this player is
    /// </summary>
    /// <returns>Role is CS student</returns>
    public override string GetRole()
    {
        return "You are a computer science student";
    }
    
    /// <summary>
    /// Performs an action specific to CS student, which is view cameras
    /// This is called when the action button is pressed
    /// </summary>
    public override void PerformAction()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected) { return; }
        viewCameras();
    }

    /// <summary>
    /// Display the computer scientist student version of the action button 
    /// and initialise the <c>_phonePanel</c>
    /// </summary>
    protected override void Start()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected) { return; }
        base.Start();
        UIActionButton.instance.SetHasTarget(true);
        UIActionButton.instance.DisplayComputerScience();
        _canvas = GameObject.Find("Canvas");
        _phonePanel = _canvas.transform.GetChild(0).gameObject;
    }

    /// <summary>
    /// Enable action button always since player can view the cameras any time
    /// </summary>
    protected override void FixedUpdate()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected) { return; }
        base.FixedUpdate();
    }

    /// <summary>
    /// Opens <c>_phonePanel</c> to view cameras.
    /// </summary>
    private void viewCameras()
    {
        if (_phonePanel != null)
        {
            if (_state == false)
            {
                _phonePanel.SetActive(true);
                _state = true;
            }
            else
            {
                _phonePanel.SetActive(false);
                _state = false;
            }
        }
    }
}
