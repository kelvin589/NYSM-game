using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using ExitGames.Client.Photon;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls the task list on the UI to display the list in TaskList
/// </summary>
[DisallowMultipleComponent]
public class UITaskList : MonoBehaviourPunCallbacks
{
    // Text box to display the task list
    private Text textBox;

    /// <summary>
    /// A PUN call back
    /// When the room properties update, call TaskList to get the changes
    /// and then update ourselves so the list text on the UI is updated as well.
    /// If students win, load the appropriate scene
    /// </summary>
    /// <param name="propertiesThatChanged"></param>
    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        TaskList.GetRoomProperty();
        UpdateText();
        if (TaskList.AllComplete()) {
            SceneManager.LoadScene("Students Win");
        }
    }

    /// <summary>
    /// Initialise the Text component
    /// </summary>
    private void Awake()
    {
        this.textBox = GetComponentInChildren<Text>();
    }
    
    /// <summary>
    /// Update the text on the display initially
    /// </summary>
    private void Start()
    {
        UpdateText();
    }

    /// <summary>
    /// Update the text in the textbox using ToString of TaskList
    /// </summary>
    private void UpdateText()
    {
        textBox.text = TaskList.ToString();
    }
}