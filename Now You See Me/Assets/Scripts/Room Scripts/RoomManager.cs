using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager Instance;
    PhotonView PView;
    public List<string> activePlayers; //List of PlayerControllers that are in Use.
    public List<string> InactivePlayers; //List of PlayerControllers that aren't in Use.
    public List<string> activeRoles;
    public string Name;
    GameObject test;
    public Menu Virus; //Creates the Array of Menus
    public Menu CSStudent;
    public Menu BusinessStudenet;
    public Menu PreMed;
    public Menu GeneralStudent;

    void Awake()
    {

        /// <summary>
        /// ActiveRoles List used to keep track of all active roles.
        /// </summary>
        /// 
        //activeRoles.Add("virus");
        //activeRoles.Add("ComputerScience");
        //activeRoles.Add("Business");
        //activeRoles.Add("Ecnomics");
        //activeRoles.Add("Pre-Med");
        //activeRoles.Add("Physics");
        //activeRoles.Add("Map");
        //activeRoles.Add("GeneralStudent");
        //activePlayers.Add("1");
        //activePlayers.Add("2");
        //activePlayers.Add("3");
        //activePlayers.Add("4");
        //activePlayers.Add("5");
        //activePlayers.Add("2");
        activePlayers.Add("4");
        activePlayers.Add("7");
        PView = GetComponent<PhotonView>();
       
        if (Instance) //Checks if another RoomManager Exists
        {
            Destroy(gameObject); //If so destroys it
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    /// <summary>
    /// Spawns the Player into the spawn area
    /// </summary>

    void CreateController()
    {
        int spawnPointX = Random.Range(-40, -24);
        int spawnPointY = Random.Range(-11, -5);

        Vector3 spawnPosition = new Vector3(spawnPointX, spawnPointY, 0); //Chooses a random spawn point for the player in hopes of not allowing them to all spawned bunched together
        //string Role = activeRoles[Random.Range(0, activeRoles.Count)];
       /// Debug.Log("Role" + Role);
        string index = activePlayers[Random.Range(0, activePlayers.Count)]; // Chooses a random string from the array
        Debug.Log(PView.ViewID);
        if (PhotonNetwork.IsMasterClient) {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player " + 2 + " Controller"), spawnPosition, Quaternion.identity);
            return;
        }
        test = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player " + index + " Controller"), spawnPosition, Quaternion.identity); //Picks the random controller using the string chosen
        //if (Role == "virus")
        //{
        //    test.AddComponent<VirusController>();
        //    Virus.Open();
        //    Destroy(test.GetComponent<StudentController>());
        //}
        //    if (Role == "GeneralStudent")
        //    {
        //        test.AddComponent<VirusController>();
        //        GeneralStudent.Open();
        //}
        //        if (Role == "ComputerScience")
        //        {
        //            test.AddComponent<CompSciController>();
        //            CSStudent.Open();
        //        }
        //            if (Role == "Pre-Med")
        //            {
        //                test.AddComponent<VirusController>();
        //                PreMed.Open();
        Debug.Log(index);                                                                                                                       
        activePlayers.Remove(index);
        InactivePlayers.Add(index); //Add this number to players not active
    }

	public override void OnEnable()
	{
		base.OnEnable();
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	public override void OnDisable()
	{
		base.OnDisable();
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

    /// <summary>
    /// When the scene loads in create new controller.
    /// </summary>

    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
	{
		if (scene.buildIndex == 1) // makes sure its the game scene
		{
        CreateController();
       // PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManager"), Vector3.zero, Quaternion.identity);
		}
	}
}