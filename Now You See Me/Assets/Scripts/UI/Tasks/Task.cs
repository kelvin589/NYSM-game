using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

/// <summary>
/// Represent a Task object. Which has
/// The name of the task
/// The completion status
/// Send the custom object across the network via:
/// Serialising the Task object into a stream of bytes
/// Deserialising a stream of bytes into a Task object
/// </summary>
[DisallowMultipleComponent]
public class Task
{
    // Name of the task
    private string _task;
    // Task completion status
    private bool _IsComplete;

    /// <summary>
    /// Default task constructor with no parameters has name "undefined"
    /// </summary>
    public Task()
    {
        this._task = "Undefined";
    }

    /// <summary>
    /// Task constructor passing in name of task
    /// </summary>
    /// <param name="task">Name of the task</param>
    public Task(string task) 
    {
        this._task = task;
    }

    /// <summary>
    /// Get the name (the title) of the task
    /// </summary>
    /// <returns>The name of the task</returns>
    public string GetTask()
    {
        return _task;
    }

    /// <summary>
    /// Mark the task as complete
    /// </summary>
    public void MarkComplete()
    {
        _IsComplete = true;
    }

    /// <summary>
    /// Check if the task is complete
    /// </summary>
    /// <returns>True if task is complete, else false</returns>
    public bool IsComplete()
    {
        return _IsComplete;
    }

    /// <summary>
    /// Return the string representation of this task.
    /// If this task is completed, return Complete or return its name
    /// </summary>
    /// <returns>"Complete" if true, else the name of task</returns>
    public override string ToString()
    {
        if (IsComplete()) 
        {
            return "Complete";
        }
        return _task;
    }

    /// <summary>
    /// Returning a stream of bytes which represents the name of the task and its completion status
    /// The byte array is in the format: task name as bytes, with the last byte of the array for the boolean
    /// </summary>
    /// <param name="task">The object to turn into bytes</param>
    /// <returns>A byte array representing task and its completion status</returns>
    public static byte[] Serialize(object task)
    {
        // The task recieved
        var recievedTask = (Task)task;
        // Array of bytes represents the task name
        byte[] nameByteArray = Encoding.UTF8.GetBytes(recievedTask._task);
        // Array of bytes extending the length array (above) by one to include _IsComplete
        byte[] nameStatusByteArray = new byte[nameByteArray.Length+1];

        // Copy the data over from string byte array to the extended one
        System.Array.Copy(nameByteArray, nameStatusByteArray, nameByteArray.Length);

        // Add the _IsComplete (and convert to byte) to the last element in the extended byte array
        nameStatusByteArray[nameStatusByteArray.Length-1] = Convert.ToByte(recievedTask._IsComplete);

        // Return the byte array
        return nameStatusByteArray;
    }

    /// <summary>
    /// Turning a stream of bytes into a <c>Task</c> object
    /// </summary>
    /// <param name="data">The byte data</param>
    /// <returns>New task object with its task and completion status</returns>
    public static object Deserialize(byte[] data)
    {
        var result = new Task();
        // Turning the bytes into string (not including last byte)
        result._task = (Encoding.UTF8.GetString(data, 0, data.Length-1));
        // The last byte in the array is for the boolean completion status
        result._IsComplete = (Convert.ToBoolean(data[data.Length-1]));
        return result;
    }
}