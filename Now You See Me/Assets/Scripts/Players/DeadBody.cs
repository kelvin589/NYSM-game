using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/// <summary>
/// Enables or disables the action button when a player is in range
/// </summary>
public class DeadBody : MonoBehaviourPun
{  
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!PhotonView.Get(other.gameObject).IsMine && PhotonNetwork.IsConnected) { return; }
        if (other.gameObject.tag != "Player"){return;}
        UIReportButton.instance.SetHasTarget(true);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!PhotonView.Get(other.gameObject).IsMine && PhotonNetwork.IsConnected) { return; }
        if (other.gameObject.tag != "Player"){return;}
        UIReportButton.instance.SetHasTarget(false);
    }
}
