using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Text;
using Hashtable = ExitGames.Client.Photon.Hashtable;
//using ExitGames.Client.Photon;

/// <summary>
/// Represents the list to store the tasks
/// </summary>
[DisallowMultipleComponent]
public class TaskList : MonoBehaviour
{
    // The number of tasks we have (corresponds to naming of task prefabs)
    private const int numberOfTasks = 13;
    // Using array to save from having to create another custom type for a list
    private static Task[] _tasks = new Task[numberOfTasks];
    // The key for the list for the room properties
    private const string taskListPropertyKey = "tasklist";

    /// <summary>
    /// Add a task to the list
    /// </summary>
    /// <param name="task">The task object to add</param>
    /// <param name="index">The index to add the task object</param>
    public static void Add(Task task, int index)
    {
        _tasks[index] = task;
    }
    
    /// <summary>
    /// Mark a task in the list as complete
    /// </summary>
    /// <param name="index">The index of the task in the list to complete</param>
    public static void Complete(int index)
    {
        if (index>_tasks.Length-1 || index<0) {return;};
        if (_tasks[index]==null) {return;};
        _tasks[index].MarkComplete();
        // Update the room properties since there is a change
        SetRoomProperties();
    }

    /// <summary>
    /// Return the string representation of this list.
    /// </summary>
    /// <returns>String representation of this list</returns>
    public static string ToString()
    {
        StringBuilder listAsString = new StringBuilder();
        listAsString.Append("Task List:");
        foreach (Task task in _tasks)
        {
            if (task != null) {
                listAsString.AppendLine();
                listAsString.Append(task.ToString());
            }
        }
        return listAsString.ToString();
    }

    /// <summary>
    /// Return the size of the task list
    /// </summary>
    /// <returns>Number of items in task list</returns>
    public static int GetSize()
    {
        return _tasks.Length;
    }

    /// <summary>
    /// Get the number of completed tasks from the task list
    /// </summary>
    /// <returns></returns>
    public static int GetCompleteSize()
    {
        int numCompleted = 0;
        foreach (Task task in _tasks)
        {
            if (task!=null && task.IsComplete())
            {
                numCompleted++;
            }
        }
        return numCompleted;
    }

    /// <summary>
    /// Check if all the tasks are complete when
    /// the number of complete tasks == the number of tasks
    /// </summary>
    /// <returns>True if all tasks are completed, else false</returns>
    public static bool AllComplete()
    {
        if (GetCompleteSize() == _tasks.Length)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Set (or update if the key is already there) the room properties named tasklist
    /// using the <c>_tasks</c> list
    /// </summary>
    public static void SetRoomProperties()
    {
        Hashtable value = new Hashtable();
        // The hashtable 'value' to add to the current room custom properties
        // The value to add is the list of tasks
        value.Add(taskListPropertyKey, _tasks);
        PhotonNetwork.CurrentRoom.SetCustomProperties(value);
    }

    /// <summary>
    /// Get the property from the room using tasklist as key.
    /// Set our tasks to the returned version
    /// </summary>
    public static void GetRoomProperty()
    {
        Hashtable properties = PhotonNetwork.CurrentRoom.CustomProperties;
        if (properties.ContainsKey(taskListPropertyKey))
        {
            _tasks = (Task[])properties[taskListPropertyKey];
        } 
    }
}