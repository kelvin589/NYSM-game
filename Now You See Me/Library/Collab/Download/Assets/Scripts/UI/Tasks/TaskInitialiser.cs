using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using ExitGames.Client.Photon;
//using Random = System.Random;

/// <summary>
/// Initialises the tasks in the game by randomly selecting a room to spawn a task item
/// then picking a random location in that room to spawn the item.
/// </summary>
[DisallowMultipleComponent]
[System.Serializable]
public class TaskInitialiser : MonoBehaviour
{
    public SpriteRenderer spawnRoom;
    public SpriteRenderer bookRoom;
    public SpriteRenderer alleyWay;
    public SpriteRenderer room1;
    public SpriteRenderer room2;
    private SpriteRenderer[] _rooms;
    
    /// <summary>
    /// Register a new type with Photon of type Task. Then randomly pick a room
    /// as well as a random location in the room to spawn a task item.
    /// </summary>
    void Awake()
    {
        PhotonPeer.RegisterType(typeof(Task), (byte) 'T', Task.Serialize, Task.Deserialize);
        // Only spawn items if i am the master   
        if (PhotonNetwork.IsMasterClient) 
        {
            // Loop through how many tasks there should be in the TaskList
            for (int index=0; index<TaskList.GetSize(); index++) 
            {
                // The prefab name. The first character should be the index
                string taskName = "Task";
                // Instanciate the task item by calling GetRandomLocation to get the location to place the item
                // Again use index to get the necessary prefab
                GameObject toAdd = PhotonNetwork.Instantiate(Path.Combine("TaskPrefabs", index+taskName), GetRandomLocation(), Quaternion.identity);
                // Get the TaskMono of the instantiated prefab.
                GeneralTaskMono toAddTask = toAdd.GetComponent<GeneralTaskMono>();
                // Get the name found in TaskMono and add create a new Task
                Task task = new Task(toAddTask.GetTask());
                // Add this Task to the TaskList using the index
                TaskList.Add(task, index);
            }
            // Making sure to update the room properties since we have added the items to the list
            TaskList.SetRoomProperties();
        }
    }

    /// <summary>
    /// First pick a random room, then pick a random location in that romo using the bounds of the room
    /// </summary>
    /// <returns>The random location to spawn the task item</returns>
    Vector3 GetRandomLocation()
    {
        // The available rooms in an array
        // SpriteRenderer is used so we can get the min/max bounds
        _rooms = new SpriteRenderer[] {spawnRoom,
                                        bookRoom,
                                        alleyWay,
                                        room1,
                                        room2};
        // Pick a random room 
        int randomRoomIndex = Random.Range(0, _rooms.Length);
        //randomRoomIndex = 0; // temporary for testing purposes making it always spawn room room
        // Get the SpriteRenderer of the room we randomly picked
        SpriteRenderer selectedRoom = _rooms[randomRoomIndex];
        // Get the min and max bounds of our randomly chosen room
        Vector3 maxBound = selectedRoom.bounds.max;
        Vector3 minBound = selectedRoom.bounds.min;
        // Make new Vector3 using the max/min bound x and y
        // The bounds are to make sure we only spawn within chosen the room
        Vector3 randomTaskLocation = new Vector3(Random.Range(minBound.x, maxBound.x), Random.Range(minBound.y, maxBound.y), 0);

        return randomTaskLocation;
        Debug.Log(randomTaskLocation);

        // spawn room min and max bounds
        // max bound (-22.8, -4.9, 0.1)
        // min bound (-41.9, -13.0,-0.1)

        // Debug.Log(maxBound);
        // Debug.Log(minBound);
        // Debug.Log(randomRoomIndex);
    }



}
