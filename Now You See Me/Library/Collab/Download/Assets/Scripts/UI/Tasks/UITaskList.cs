using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using ExitGames.Client.Photon;

/// <summary>
/// Controls the task list on the UI to display the list in TaskList
/// </summary>
[DisallowMultipleComponent]
public class UITaskList : MonoBehaviourPunCallbacks
{
    // What we are using to display the task list
    private Text textBox;

    /// <summary>
    /// Initialise the Text component
    /// </summary>
    void Awake()
    {
        this.textBox = GetComponentInChildren<Text>();
    }
    
    /// <summary>
    /// Update the text on the display when we first start
    /// </summary>
    void Start()
    {
        UpdateText();


    }

    /// <summary>
    /// Updates the text on the textbox using the ToString of TaskList
    /// </summary>
    void UpdateText()
    {
        textBox.text = TaskList.ToString();
    }

    // mark a task as complete
    // will be removed and added to task item. this is for testing
    // This is on the green button in the text list ui
    public void CompleteTask(int index)
    {
        TaskList.Complete(index);
    }

    /// <summary>
    /// A PUN call back
    /// When the room properties update, we need to call TaskList to get the changes
    /// and then update ourselves so the list text on the UI is updated as well.
    /// </summary>
    /// <param name="propertiesThatChanged"></param>
    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        TaskList.GetRoomProperty();
        UpdateText();
        Debug.Log("updated properties");
    }
}
