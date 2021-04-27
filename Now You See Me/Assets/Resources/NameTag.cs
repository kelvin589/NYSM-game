using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;


public class NameTag : MonoBehaviourPunCallbacks
{
    public Text playerNameTag; //reference to name tag game object of player.

    void Start()
    {
        SetNametag();
    }
    public void SetNametag()
    {
        Player[] players = PhotonNetwork.PlayerList; //gets all players in game
        Player localPlayer = null; //will hold the local player
        GameObject playerControllerTextObject = null; //stores object that displays nickname
        string nicknameText = "";
        //GameObject[] controllers = GameObject.FindGameObjectsWithTag("Player");

        List<GameObject> controllers = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
        controllers.AddRange(new List<GameObject>(GameObject.FindGameObjectsWithTag("Virus")));

        foreach (Player p in players)
        {
            foreach (GameObject player in controllers)
            {
                if (player.GetComponent<PhotonView>().Owner == p)
                {
                    playerControllerTextObject = player.transform.GetChild(0).GetChild(0).gameObject; //gets the nickname text object for the specific player controller
                    nicknameText = playerControllerTextObject.GetComponent<Text>().text; //gets text component
                    nicknameText = p.NickName;
                    playerControllerTextObject.GetComponent<Text>().text = nicknameText; //local player nickname assigned to text component
                }
            }
        }


    }
}

