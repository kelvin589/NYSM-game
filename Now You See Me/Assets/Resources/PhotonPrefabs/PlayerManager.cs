using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    PhotonView PView;
    public List<string> activePlayers; //List of PlayerControllers that are in Use.
    public List<string> InactivePlayers; //List of PlayerControllers that aren't in Use.
    public string Name;

    void Awake()
    {
        activePlayers.Add("1");
        activePlayers.Add("2");
        activePlayers.Add("3");
        activePlayers.Add("4");
        activePlayers.Add("5");
        activePlayers.Add("6");
        activePlayers.Add("7");
        activePlayers.Add("8");
        PView = GetComponent<PhotonView>();
    }

    /// <summary>
    /// Creates Controller for relevent PView.
    /// </summary>

    void Start()
    {
        if (PView.IsMine) //If the player is mine - If You Own
        {
            CreateController(); //Give me controls/Make controls - Do this cmnd     

        }   
    }

    /// <summary>
    /// Spawns the player Prefab in
    /// </summary>

    void CreateController()
    {
        
        int spawnPointX = Random.Range(-40, -24);
        int spawnPointY = Random.Range(-11, -5);
        //var random = new Random(); // randomly choose 1-8

        Vector3 spawnPosition = new Vector3(spawnPointX, spawnPointY, 0); //Chooses a random spawn point for the player in hopes of not allowing them to all spawned bunched together
        string index = activePlayers[Random.Range(0, activePlayers.Count)]; // Chooses a random string from the array
        if (PView.IsMine)
        {
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player " + index + " Controller"), spawnPosition, Quaternion.identity); //Picks the random controller using the string chosen
         //       Name.text = PhotonNetwork.NickName;
        }
        activePlayers.Remove(index);
        InactivePlayers.Add(index); //Add this number to players not active
    }
} 
