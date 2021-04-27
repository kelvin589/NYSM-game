using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using System.Diagnostics;
using System.Linq;
using UnityEngine.UI;

public class Launcher : MonoBehaviourPunCallbacks
{
    public static Launcher Instance;

    public TMP_InputField NickNameIF; //NickNameInputField
    public TMP_InputField RoomNameIF; //RoomNameInputField
    public TMP_Text ET;
    public TMP_Text RoomNameText;
    public Transform RoomListContent;
    public GameObject RoomListItemPrefab;
    public GameObject StartButton;
    public Transform playerListContent;
    public GameObject PlayerListItemPrefab;
    public Button setNameBtn;


    void Awake()
	{
		Instance = this;
	}


    void Start()
    {
       // Debug.Log("Connecting to Master")
        PhotonNetwork.ConnectUsingSettings();
    }

    override public void OnConnectedToMaster()
    {
        //  Debug.Log("Connected to Master");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void OnSetName()
    {
        PhotonNetwork.NickName = NickNameIF.text;
    }

    override public void OnJoinedLobby()
    {
        MenuManager.Instance.OpenMenu("Title");
    }

    /// <summary>
    /// Create the room with the roomname for players to be added.
    /// </summary>

    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(RoomNameIF.text))
        {
            return;
        }
        System.Diagnostics.Debug.WriteLine("RoomCreated");
        PhotonNetwork.CreateRoom(RoomNameIF.text);
        MenuManager.Instance.OpenMenu("Loading");
    }

    /// <summary>
    /// Open Room Menu, Add player to PlayerList in the current Room, Instantiate the players and have the Start Button set up.
    /// </summary>

    public override void OnJoinedRoom()
    {
        MenuManager.Instance.OpenMenu("room");
        RoomNameText.text = PhotonNetwork.CurrentRoom.Name;

        Player[] players = PhotonNetwork.PlayerList;

        foreach (Transform child in playerListContent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < players.Length; i++)
        {
            Instantiate(PlayerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
        }
        {
            StartButton.SetActive(PhotonNetwork.IsMasterClient);
        }
    }
    
    public void OnMasterClientSwitched(Player newMasterClient)
    {
        StartButton.SetActive(PhotonNetwork.IsMasterClient);
    }


    /// <summary>
    /// Sync all players to the same photon scene and start to load level 1 (lab)
    /// </summary>
    public void StartGame()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.LoadLevel(1);
    }

    /// <summary>
    /// Error Message for failed Room Creation.
    /// </summary>

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        ET.text = "Room Creation Failed: " + message;
        System.Diagnostics.Debug.WriteLine("Room Creation Failed: " + message);
        MenuManager.Instance.OpenMenu("error");
    }

    /// <summary>
    /// Leave Photon Room, Opening Loading Menu to take them back to main menu
    /// </summary>
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        MenuManager.Instance.OpenMenu("Loading");
    }

    /// <summary>
    /// Adds people directly to the Room using Information to get the correct room and opening loading menu to then change to room menu.
    /// </summary>

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        MenuManager.Instance.OpenMenu("Loading");
    }

    public override void OnLeftRoom()
    {
        MenuManager.Instance.OpenMenu("Title");
    }

    /// <summary>
    /// Updates the List of rooms avaliable so when a new room is created it is added and when a room has 0 players it is destroyed.
    /// </summary>

    public override void OnRoomListUpdate(List<RoomInfo> RoomList)
    {
        foreach (Transform trans in RoomListContent)
        {
            Destroy(trans.gameObject);
        }

        for (int i = 0; i < RoomList.Count; i++)
        {
            if (RoomList[i].RemovedFromList)
                continue;
            Instantiate(RoomListItemPrefab, RoomListContent).GetComponent<RoomListItem>().SetUp(RoomList[i]);
        }
    }


    /// <summary>
    /// Starts the playerlist sets up the player channel.
    /// </summary>
    public override void OnPlayerEnteredRoom(Player NPlayer)
    {
        Instantiate(PlayerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(NPlayer);
    }

}
