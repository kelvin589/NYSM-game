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

    }

    /// <summary>
    /// If we take time to complete, change text of the tooltip
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        if (timeToComplete != 0.0f)
        {
            tooltip.SetText("Hold E to collect");
        }
    }

    /// <summary>
    /// Every frame check if we HasCollider
    /// If we do and E pressed, then mark the task as complete
    /// Incrementing temp time for temp tasks so they only complete when E is held
    /// </summary>
    private void Update()
    {
        if (!HasCollider) {return;}
        if (!collidingPlayer.GetComponent<PhotonView>().IsMine && PhotonNetwork.IsConnected) { return; }  
        HandleInput();
    }

    /// <summary>
    /// Check if E is pressed or Button is held down
    /// Depending on timed or not timed task
    /// </summary>
    private void HandleInput()
    {
        if ((Input.GetKeyDown(KeyCode.E) || UICollectButton.instance.IsPressed) && timeToComplete == 0.0f)
        {
            CompleteTask();
        } else if ((Input.GetKey(KeyCode.E) || UICollectButton.instance.IsPressed) && timeToComplete != 0.0f)
        {
            CalculateTime();
        } else 
        {
            if (timeToComplete!=0.0f)
            {
                tooltip.SetText("Hold E to collect");
            }
            _tempTime = 0.0f;
        }
    }

    /// <summary>
    /// If it is a timed task, update <c>_tempTime</c>
    /// until completion time has elapsed to complete task
    /// </summary>
    private void CalculateTime()
    {
        PlayerController collidingPlayerController = collidingPlayer.GetComponent<PlayerController>();
        float timePercentage = collidingPlayerController == null ? 1.0f : collidingPlayerController.GetTaskTimePercent();
        float timeToCompleteNew = timeToComplete * timePercentage;
        if (_tempTime < timeToCompleteNew)
        {
            _tempTime+=Time.deltaTime;
            tooltip.SetText("Hold E for " + Math.Round((timeToCompleteNew - _tempTime), 1) + " seconds");
        } else
        {
            _tempTime = 0.0f;
            CompleteTask();
        }
    }
}