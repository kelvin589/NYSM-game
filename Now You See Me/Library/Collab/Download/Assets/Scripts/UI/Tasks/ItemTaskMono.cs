using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Photon.Pun;
using TMPro;

/// <summary>
/// Attach to a GameObject to become a regular item (pickup/hold) task
/// Make sure to update TaskList size in the inspector
/// </summary>
[DisallowMultipleComponent]
public class ItemTaskMono : GeneralTaskMono
{
    // Set time to complete to anything but 0 to make it a timed task
    [Tooltip("The time in seconds to hold button to complete task")]
    [SerializeField]
    private float timeToComplete;
    [Tooltip("Temp for counting up to time to complete")]
    [SerializeField]
    private float _tempTime = 0.0f;

    /// <summary>
    /// This is called when the collect button is pressed
    /// For this item task we just complete it by removing it and setting it to complete
    /// </summary>
    public override void TaskAction()
    {
        // double temp = 0.0f;
        // if (UICollectButton.instance.IsPressed) {
        //     temp+=Time.deltaTime;
        //     Debug.Log("temp");
        // } else {
        //     temp = 0.0f;
        // }
        //CompleteTask();
        // Need to split timer code into separate thing to be able to use with button
    }

    /// <summary>
    /// Initialise using base Awake
    /// If we take time to complete, change text of the tooltip
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        if (timeToComplete != 0)
        {
            tooltip.GetComponent<TextMeshProUGUI>().SetText("Hold E to collect");
        }
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
    /// Every frame check if we HasCollider
    /// If we do and E pressed, then mark the task as complete in the TaskList and make an RPC call to destory ourselves
    /// Incrementing temp time for temp tasks so they only complete when E is held
    /// </summary>
    private void Update()
    {
        // THIS IS A MESSSSSSSSS!!! CLEAN IT UP
        if (!HasCollider) {return;}
        if (!collidingPlayer.GetComponent<PhotonView>().IsMine && PhotonNetwork.IsConnected) { return; }  
        if ((Input.GetKeyDown(KeyCode.E) || UICollectButton.instance.IsPressed) && timeToComplete==0.0f)
        {
            //TaskAction();
            CompleteTask();
        } else if ((Input.GetKey(KeyCode.E) || UICollectButton.instance.IsPressed) && timeToComplete!=0.0f)
        {
            float timePercentage = collidingPlayer.GetComponent<PlayerController>() == null ? 1.0f : collidingPlayer.GetComponent<PlayerController>().GetTaskTimePercent();
            float timeToCompleteNew = timeToComplete * timePercentage;
            if (_tempTime < timeToCompleteNew) {
                //Debug.Log(_tempTime);
                _tempTime+=Time.deltaTime;
                tooltip.GetComponent<TextMeshProUGUI>().SetText("Hold E for " + Math.Round((timeToCompleteNew-_tempTime), 1) + " seconds");
            } else {
                _tempTime = 0.0f;
                //TaskAction();
                CompleteTask();
            }
        } else 
        {
            if (timeToComplete!=0.0f) {
                tooltip.GetComponent<TextMeshProUGUI>().SetText("Hold E to collect");
            }
            _tempTime = 0.0f;
        }
    }
}
