﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Photon.Pun;
using TMPro;

/// <summary>
/// Attach to a GameObject to become a GUI task
/// Make sure to update TaskList size in the inspector
/// </summary>
[DisallowMultipleComponent]
public class GUITaskMono : GeneralTaskMono
{
    // The canvas to open
    private GameObject _GUICanvas;

    /// <summary>
    /// This is what is called when the collect button is pressed
    /// For GUI task that is showing the mini game
    /// </summary>
    public override void TaskAction()
    {
        _GUICanvas.SetActive(true);
        //MenuManager.Instance.OpenMenu("Options Menu (Menu)");
    }

    /// <summary>
    /// Called from GUI on task completion
    /// Completes the task and hides the GUI
    /// </summary>
    public void CloseGUITask()
    {
        CompleteTask();
        _GUICanvas.SetActive(false);
    }

    /// <summary>
    /// Initialise the <c>_GUICanvas</c> and set the tooltip text
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        _GUICanvas = this.transform.GetChild(1).gameObject;
        tooltip.GetComponent<TextMeshProUGUI>().SetText("Press E to play");
    }

    /// <summary>
    /// Do something to mark the task as complete
    /// If you remove just use item tasks remove complete task
    /// </summary>
    protected override void CompleteTask()
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
    private void DestroyTaskObjectRPC()
    {
        if (photonView.IsMine) 
        {
            PhotonNetwork.Destroy(photonView);
        }
    }

    /// <summary>
    /// Every frame check if we have a collider <c>HasCollider</c>
    /// If we do, and we press E then do something
    /// </summary>
    private void Update()
    {
        if (!HasCollider) { return; }
        if (Input.GetKeyDown(KeyCode.E))
        {
            TaskAction();
        }
    }
}
