using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/// <summary>
/// Define the behaviour of a basic student who can only complete tasks
/// </summary>
[DisallowMultipleComponent]
public class StudentController : PlayerController
{   
    /// <summary>
    /// Get this player's role
    /// </summary>
    /// <returns>Role is Student</returns>
    public override string GetRole()
    {
        return "You are a student";
    }

    /// <summary>
    /// Performs an action specific to student
    /// This is called when the action button is pressed
    /// </summary>
    public override void PerformAction()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected) { return; }
        Debug.Log("Perform some student action");
    }

    /// <summary>
    /// Display the student version of the action button
    /// </summary>
    protected override void Start()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected) { return; }
        base.Start();
        UIActionButton.instance.DisplayStudent();
    }

    /// <summary>
    /// Colliding with a player updates the <c>_currentTarget</c> 
    /// </summary>
    protected override void FixedUpdate()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected) { return; }
        base.FixedUpdate();
        foreach (Collider2D collider in _collidingWith) {
            if (collider.transform.gameObject.tag == "Player" && collider.transform.gameObject != this.gameObject) 
            {
                _currentTarget = collider.GetComponent<PlayerController>();
                UIActionButton.instance.SetHasTarget(true);
                return;
            }
        }
        UIActionButton.instance.SetHasTarget(false);
    }
}
