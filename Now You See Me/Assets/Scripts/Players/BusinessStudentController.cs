using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/// <summary>
/// Define the behaviour of a business student
/// A business student may pick up items with a 10% time reduction
/// </summary>
[DisallowMultipleComponent]
public class BusinessStudentController : PlayerController
{   
    /// <summary>
    /// Get this player's role
    /// </summary>
    /// <returns>Role is Student</returns>
    public override string GetRole()
    {
        return "You are a business student";
    }

    /// <summary>
    /// Performs an action specific to a business student
    /// This is called when the action button is pressed
    /// </summary>
    public override void PerformAction()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected) { return; }
        Debug.Log("Perform some business student action");
    }

    /// <summary>
    /// Display the student version of the action button
    /// and reduce task completion time by 10%
    /// </summary>
    protected override void Start()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected) { return; }
        base.Start();
        UIActionButton.instance.DisplayStudent();
        taskTimePercent = 0.9f;
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
