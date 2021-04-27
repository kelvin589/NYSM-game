using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using ExitGames.Client.Photon;

/// <summary>
/// Initialises the tasks in the game by randomly selecting a room to spawn a task item
/// then picking a random location in that room to spawn the item.
/// </summary>
[DisallowMultipleComponent]
[System.Serializable]
public class TaskInitialiser : MonoBehaviour
{
    public SpriteRenderer room1;
    public SpriteRenderer room2;
    public SpriteRenderer room3;
    public SpriteRenderer room4;
    public SpriteRenderer room5;
    public SpriteRenderer room6;
    public SpriteRenderer room7;
    public SpriteRenderer room8;
    public SpriteRenderer room9;
    public SpriteRenderer room10;
    public SpriteRenderer room11;
    public SpriteRenderer room12;
    public SpriteRenderer room13;
    public SpriteRenderer room14;
    public SpriteRenderer room15;
    public SpriteRenderer room16;
    public SpriteRenderer room17;
    private SpriteRenderer[] _rooms;
    // The prefab name. The first character should be the index
    private const string _PrefabTaskName = "Task";

    /// <summary>
    /// Register a new type with Photon of type Task. Then randomly pick a room
    /// as well as a random location in the room to spawn a task item.
    /// </summary>
    private void Awake()
    {
        // Register a new type with photon
        PhotonPeer.RegisterType(typeof(Task), (byte) 'T', Task.Serialize, Task.Deserialize);
        // Only spawn items if I am the master   
        if (PhotonNetwork.IsMasterClient) 
        {
            // Loop through how many tasks there should be in the TaskList
            for (int index=0; index<TaskList.GetSize(); index++) 
            {
                // Instantiate the task item by calling GetRandomLocation to get the location to place the item
                // Use index to get the necessary prefab
                GameObject toAdd = PhotonNetwork.Instantiate(Path.Combine("TaskPrefabs", index+_PrefabTaskName),
                                                                GetRandomLocation(), 
                                                                Quaternion.identity);
                GeneralTaskMono toAddTask = toAdd.GetComponent<GeneralTaskMono>();
                Task task = new Task(toAddTask.GetTask());
                TaskList.Add(task, index);
            }
            // Update room properties since we have added the items to the list
            TaskList.SetRoomProperties();
        }
    }

    /// <summary>
    /// First pick a random room, then pick a random location in that romo using the bounds of the room
    /// </summary>
    /// <returns>The random location to spawn the task item</returns>
    private Vector3 GetRandomLocation()
    {
        // The available rooms in an array
        _rooms = new SpriteRenderer[] {room1,
                                        room2,
                                        room3,
                                        room4,
                                        room5,
                                        room6,
                                        room7,
                                        room8,
                                        room9,
                                        room10,
                                        room11,
                                        room12};
        // Pick a random room 
        int randomRoomIndex = Random.Range(0, _rooms.Length);
        // Get the SpriteRenderer of the room we randomly picked
        SpriteRenderer selectedRoom = _rooms[randomRoomIndex];
        // Get the min and max bounds of our randomly chosen room
        Vector3 maxBound = selectedRoom.bounds.max;
        Vector3 minBound = selectedRoom.bounds.min;
        // Make a new Vector3 using the max/min bound x and y
        Vector3 randomTaskLocation = new Vector3(Random.Range(minBound.x, maxBound.x),
                                                    Random.Range(minBound.y, maxBound.y),
                                                    0);

        return randomTaskLocation;
    }
}