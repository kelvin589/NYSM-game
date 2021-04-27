using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Linq;

/// <summary>
/// This class controls the countdown timer that appears on the voting screen.
/// </summary>
public class VotingCountdownTimer : MonoBehaviourPunCallbacks
{
   [SerializeField] private Text timer;
   [SerializeField] private float timeLeft = 30;
   [SerializeField] private int timeToShow = 30;
   [SerializeField] private bool isCounting = false;

    /// <summary>
    /// Sets the timer to start counting when the voting screen is opened.
    /// </summary>
    void Start()
    {
        isCounting = true;
    }

    /// <summary>
    /// This method is called once per frame which allows the simulation of a countdown timer in seconds.
    /// </summary>
    void Update()
    {
        // Controls the countdown of the timer
        if (isCounting)
        {
            DisplayTimer(timeToShow);
            // If there is time left, decrease the time by one second and update the time on-screen
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                timeToShow = (int)timeLeft;
            }
            // Timer reaches 0
            else
            {
                // Keeps the timer from going below 0
                timeLeft = 0;
                Debug.Log("Voting time up");
                // Stop counting
                isCounting = false;

                // Check the result of the current voting round
                GetComponent<VotingResultsManager>().CheckVotingResult();
                // Get the screen ready for the next round
                GetComponent<VotingUIManager>().photonView.RPC("PrepareScreenForNextRound", RpcTarget.All);
                // Close the voting screen
                GameObject votingManager = GameObject.Find("Voting Manager");
                votingManager.GetComponent<VotingScreenManager>().CloseVotingScreen();
            }
        }
    }

    /// <summary>
    /// Displays the time remaining on the voting screen.
    /// </summary>
    /// <param name="time"></param>
    void DisplayTimer(int time)
    {
        timer.text = time.ToString();
    }

    /// <summary>
    /// Resets the timer ready for the next round of voting.
    /// </summary>
    public void ResetTimer()
    {
        timeToShow = 30;
        timeLeft = 30;
        isCounting = true;
    }
}
