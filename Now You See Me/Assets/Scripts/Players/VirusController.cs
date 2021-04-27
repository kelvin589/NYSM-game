using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

/// <summary>
/// Defines the behaviour of a virus.
/// A virus may kill students
/// </summary>
[DisallowMultipleComponent]
public class VirusController : PlayerController
{
    private bool _IsHoldingObject;
    private Transform _currentHeldObject;

    /// <summary>
    /// This is called when you press the grab button
    /// to 'pick-up' a dead body or task
    /// </summary>
    public void GrabObject()
    {
        // Toggle whether holding to pick-up or put-down
        _IsHoldingObject = !_IsHoldingObject;
        // If the photon view of held object is not mine, request ownership to move it
        if (!_currentHeldObject.GetComponent<PhotonView>().IsMine) {
            _currentHeldObject.GetComponent<PhotonView>().RequestOwnership();
        }
        if (_IsHoldingObject == false) {
            _currentHeldObject = null;
        }
    }

    /// <summary>
    /// Get this player's role
    /// </summary>
    /// <returns>Role is virus</returns>
    public override string GetRole()
    {
        return "You are the virus";
    }

    /// <summary>
    /// Performs an action specific to virus, which is to kill a player.
    /// This is called when the action button is pressed
    /// </summary>
    public override void PerformAction()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected) { return; }
        kill(_currentTarget.gameObject);
    }

    /// <summary>
    /// Initialise virus:
    /// Initially not holding object
    /// Display virus's action button
    /// Enable grab button
    /// Set the grab button current player target to this
    /// </summary>
    protected override void Start()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected) { return; }
        base.Start();
        _IsHoldingObject = false;
        UIActionButton.instance.DisplayVirus();
        UIGrabButton.instance.SetActive(true);
        UIGrabButton.instance.SetCurrentTarget(this);
    }

    /// <summary>
    /// Determine who this is colliding with.
    /// If colliding with a player then set this target to the colliding player
    /// and enable the action button
    /// If holding an object, do not run the next if statement
    /// If colliding with a dead body or task, set this current held object
    /// and enable the grab button
    /// </summary>
    protected override void FixedUpdate()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected) { return; }
        base.FixedUpdate();
        foreach (Collider2D collider in _collidingWith) {
            // Ignore this object
            if (collider.transform.gameObject == this.gameObject) {continue;}
            if (collider.transform.gameObject.tag == "Player") 
            {
                _currentTarget = collider.GetComponent<PlayerController>();
                UIActionButton.instance.SetHasTarget(true);
                return;
            }
            UIActionButton.instance.SetHasTarget(false);
            // If holding an object, return as to not allow for holding another
            if (_IsHoldingObject)
            {
                UIGrabButton.instance.SetHasTarget(true);
                return;
            }
            if (collider.transform.gameObject.tag == "Dead" || collider.transform.gameObject.tag == "Task") 
            {
                _currentHeldObject = collider.GetComponent<Transform>();
                UIGrabButton.instance.SetHasTarget(true);
                return;
            }
            UIGrabButton.instance.SetHasTarget(false);
        }
    }

    /// <summary>
    /// Each frame check if we should be moving an object
    /// </summary>
    protected override void Update()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected) { return; }
        base.Update();
        MoveHeldObjectPosition();
    }

    /// <summary>
    /// Set the transform of the held object to our transform so it moves relative to us
    /// so it looks like it is moving with us as if we have 'picked' it up
    /// </summary>
    private void MoveHeldObjectPosition()
    {
        // If we are not holding an object, do nothing
        if (_currentHeldObject == null) {return;};
        // If we are holding an object
        if (_IsHoldingObject)
        {
            // Set its position to ours
            _currentHeldObject.position = transform.position;
        } 
    }

    /// <summary>
    /// Essentially called when action button is pressed
    /// Send an RPC to kill (remove and replace with ghost + body) on all clients
    /// </summary>
    /// <param name="target">The target GameObject to kill</param>
    private void kill(GameObject target) 
    {
        int targetViewID = target.GetComponent<PhotonView>().ViewID;
        photonView.RPC("KillStudentRPC", RpcTarget.All, targetViewID);
    }

    /// <summary>
    /// Called on every client
    /// Find the target to remove using the ID passed
    /// Then make them spawn their own dead prefab + ghost and destroy themselves
    /// </summary>
    /// <param name="targetViewID">The target Photon ViewID to kill</param>
    [PunRPC]
    private void KillStudentRPC(int targetViewID)
    {
        PhotonView target = PhotonView.Find(targetViewID);
        GameObject targetGameObject = target.gameObject;
        // Get the name of the prefab
        string name = targetGameObject.name;
        // If I am the person to get killed and I own the GameObject
        if (target.IsMine)
        {
            // Substring my name to remove the (clone) part and spawn body at the current location
            PhotonNetwork.Instantiate(Path.Combine("DeadPhotonPrefabs", "Dead " + name.Substring(0, name.Length-7)), targetGameObject.transform.position, Quaternion.identity);
            // Spawn the ghost at current location
            PhotonNetwork.Instantiate("Ghost2", targetGameObject.transform.position, Quaternion.identity);
            // Destroy ourselves (that being the target destroying themselves)
            PhotonNetwork.Destroy(target);
        }
    }
}
