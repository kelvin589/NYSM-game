using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MovementController;
using Photon.Pun;

/// <summary>
/// Define the behaviour of a Ghost
/// A Ghost may push a virus.
/// </summary>
[DisallowMultipleComponent]
public class GhostController : PlayerController
{
    // Force at which to push a target
    private const int _PushForce = 500;

    /// <summary>
    /// Get this player's role
    /// </summary>
    /// <returns>Role is Ghost</returns>
    public override string GetRole()
    {
        return "You are a ghost";
    }

    /// <summary>
    /// Performs an action specific to ghost, which is pushing a virus
    /// This is called when the action button is pressed
    /// </summary>
    public override void PerformAction()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected) { return; }
        PushVirus(_currentTarget.gameObject);
    }

    /// <summary>
    /// Display the ghost version of the action button
    /// </summary>
    protected override void Start()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected) { return; }
        base.Start();
        UIActionButton.instance.DisplayGhost();
    }

    /// <summary>
    /// Colliding with an object of Vius tag updates the <c>_currentTarget</c> 
    /// and enables the action button
    /// </summary>
    protected override void FixedUpdate()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected) { return; }
        base.FixedUpdate();
        foreach (Collider2D collider in _collidingWith) {
            if (collider.transform.gameObject.tag == "Virus" && collider.transform.gameObject != this.gameObject) 
            {
                _currentTarget = collider.GetComponent<PlayerController>();
                UIActionButton.instance.SetHasTarget(true);
                return;
            }
        }
        UIActionButton.instance.SetHasTarget(false);
    }

    /// <summary>
    /// Take a GameObject, the target, to get the PhotonView ID to cal PushVirusRPC
    /// </summary>
    /// <param name="target">The GameObject target to push</param>
    private void PushVirus(GameObject target) {
        int targetViewID = target.GetComponent<PhotonView>().ViewID;
        photonView.RPC("PushVirusRPC", RpcTarget.All, targetViewID);
    }

    /// <summary>
    /// Push the <c>target</c> in a certain direction,
    /// depending on the direction this ghost is facing.
    /// </summary>
    /// <param name="targetViewID">The PhotonView ID of the target</param>
    [PunRPC]
    private void PushVirusRPC(int targetViewID)
    {
        PhotonView target = PhotonView.Find(targetViewID);
        if (target == null) { return; }
        if (target.gameObject.tag.Equals("Virus"))
        {
            Rigidbody2D rigidbodyTarget = target.GetComponent<Rigidbody2D>();
            MovementController movementController = this.gameObject.GetComponent<MovementController>();
            FacingDirection pushDirection = movementController.GetFacingDirection();

            if (pushDirection == MovementController.FacingDirection.DOWN) 
            {
                rigidbodyTarget.AddForce(-rigidbodyTarget.transform.up * _PushForce);
            }
            if (pushDirection == MovementController.FacingDirection.LEFT) 
            {
                rigidbodyTarget.AddForce(-rigidbodyTarget.transform.right * _PushForce);
            }
            if (pushDirection == MovementController.FacingDirection.RIGHT) 
            {
                rigidbodyTarget.AddForce(rigidbodyTarget.transform.right * _PushForce);
            }
            if (pushDirection == MovementController.FacingDirection.UP) 
            {
                rigidbodyTarget.AddForce(rigidbodyTarget.transform.up * _PushForce);
            }
        }
    }
}
