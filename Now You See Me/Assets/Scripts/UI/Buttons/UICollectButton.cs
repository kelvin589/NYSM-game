using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Controls the button on screen for collecting an item or initiating a game task
/// </summary>
public class UICollectButton : UIGeneralButton
{
    public static UICollectButton instance;
    public bool IsPressed;
    // The current task that this button has reference to
    private GeneralTaskMono _currentTask;

    /// <summary>
    /// When this button is pressed call <see cref="TaskAction"/> of <see cref="TaskMono"/>
    /// which is the <c>currentTask</c>. Which essentially completes the task.
    /// </summary>
    public override void ButtonPressed()
    {   
        if (this._currentTask == null) { return; } 
        this._currentTask.TaskAction();
    }

    /// <summary>
    /// Set the current task we have reference to
    /// </summary>
    /// <param name="task">The current task</param>
    public void SetCurrentTask(GeneralTaskMono task)
    {
        this._currentTask = task;
    }

    /// <summary>
    /// <c>IsPressed</c> is true if the button is held down
    /// </summary>
    public void OnPDown()
    {
        this.IsPressed = true;
    }

    /// <summary>
    /// <c>IsPressed</c> is false if the button is lifted up
    /// </summary>
    public void OnPUp()
    {
        this.IsPressed = false;
    }

    /// <summary>
    /// Initialise instance and set the colour and text to something appropriate
    /// We also disable it as not everyone should have this button
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        instance = this;
        SetColour(new Color(0.98f, 0.839f, 0.675f));
        SetText("Collect");  
        SetActive(false);
    }
}