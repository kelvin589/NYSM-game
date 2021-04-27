using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Linq;

/// <summary>
/// This class controls how long an alert should show on the screen for and ensures it stays visible for the specified amount of time.
/// </summary>
public class VotingAlertsTimer : MonoBehaviourPunCallbacks
{
    [SerializeField] private float timeLeft = 3;
    [SerializeField] private bool isCounting = false;

    /// <summary>
    /// Sets the timer to start counting when the alert appears on the screen.
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
            // If there is time left, decrease the time by one second
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
            }
            // Timer reaches 0
            else
            {
                // Keeps the timer from going below 0
                timeLeft = 0;
                Debug.Log("ALERT CLOSING");
                // Stop counting
                isCounting = false;
                ResetTimer();
                // Alert is closed
                gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Resets the timer ready for the next alert to appear.
    /// </summary>
    public void ResetTimer()
    {
        timeLeft = 3;
        isCounting = true;
    }
}
