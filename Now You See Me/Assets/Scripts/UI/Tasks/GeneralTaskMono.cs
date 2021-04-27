using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Photon.Pun;
using TMPro;

/// <summary>
/// Represents a general task GameObject
/// Get the task (its name)
/// Enabling the tooltip
/// Do something when the collect button is pressed
/// Do something when completing a task
/// Another collider enters/exits this collider
/// </summary>
public abstract class GeneralTaskMono : MonoBehaviourPun
{
    [Tooltip("The title of the task (displayed on task list)")]
    [SerializeField]
    protected string task;
    // True someone is colliding with us (i.e. a player)
    protected bool HasCollider;
    // The text mesh pro text for the tooltip
    protected TextMeshProUGUI tooltip;
    // The other player entering within range of task  
    protected GameObject collidingPlayer;

    /// <summary>
    /// Get the name of this task
    /// </summary>
    /// <returns>The name of this task</returns>
    public string GetTask()
    {
        return task;
    }

    /// <summary>
    /// This is called when the collect button is pressed
    /// </summary>
    public abstract void TaskAction();

    /// <summary>
    /// Initialise label tooltip and make it inactive (hide it)
    /// </summary>
    protected virtual void Awake()
    {
        tooltip = this.transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        tooltip.gameObject.SetActive(false);
    }

    /// <summary>
    /// Do something to mark the task as complete
    /// If you remove just use item tasks remove complete task
    /// </summary>
    protected virtual void CompleteTask()
    {
        // Task to complete based on its name: 
        // xxTask(Clone) where xx is an index + (Clone) bit from Photon
        TaskList.Complete((Int32.Parse(this.gameObject.name.Replace("Task(Clone)", ""))));
        // Send an RPC call to everyone to remove this object
        photonView.RPC("DestroyTaskObjectRPC", RpcTarget.All);
    }

    /// <summary>
    /// An RPC to destroy this object only if I own it.
    /// </summary>
    [PunRPC]
    protected void DestroyTaskObjectRPC()
    {
        if (photonView.IsMine) 
        {
            PhotonNetwork.Destroy(photonView);
        }
    }

    /// <summary>
    /// When collider enters, enable collect button and tooltip, related to collect, then hide action button
    /// </summary>
    /// <param name="other">The one we are colliding with</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.GetComponent<PhotonView>().IsMine) { return; }
        if (other.gameObject.tag != "Player") { return; }
        HasCollider = true;
        collidingPlayer = other.gameObject;
        tooltip.gameObject.SetActive(true);
        UICollectButton.instance.SetActive(true);
        UICollectButton.instance.IsPressed = false;
        UICollectButton.instance.SetCurrentTask(this);
        UICollectButton.instance.SetHasTarget(true);
        UIActionButton.instance.SetActive(false);
    }

    /// <summary>
    /// When collider exits, disable button and tooltip, related to collect, then show action button
    /// </summary>
    /// <param name="other">The one we are colliding with</param>
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.GetComponent<PhotonView>().IsMine) { return; }
        if (other.gameObject.tag != "Player") { return; }
        HasCollider = false;
        collidingPlayer = other.gameObject;
        tooltip.gameObject.SetActive(false);
        UICollectButton.instance.SetActive(false);
        UICollectButton.instance.IsPressed = false;
        UICollectButton.instance.SetCurrentTask(null);
        UICollectButton.instance.SetHasTarget(true);
        UIActionButton.instance.SetActive(true);
    }
}