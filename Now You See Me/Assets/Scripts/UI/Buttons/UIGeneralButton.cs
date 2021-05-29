using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents a general button on the UI which can be enabled or disabled
/// </summary>
public abstract class UIGeneralButton : MonoBehaviour
{
    // The button we are attached to
    protected Button button;
    // If we have a target or not to enable/disable the button
    private bool _HasTarget;

    /// <summary>
    /// Activate or deactivate the button
    /// </summary>
    /// <param name="active">True if active, else false</param>
    public void SetActive(bool active)
    {
        this.button.gameObject.SetActive(active);
    }

    /// <summary>
    /// Set if this button has a target or not to enable/disable the button
    /// </summary>
    /// <param name="HasTarget">We have a target or not</param>
    public void SetHasTarget(bool HasTarget)
    {
        this._HasTarget = HasTarget;
    }

    /// <summary>
    /// Subclasses should implement their own behaviour
    /// when the button is pressed on the GUI
    /// </summary>
    public abstract void ButtonPressed();

    /// <summary>
    /// Set the colour of the button
    /// </summary>
    /// <param name="colour">The new colour of the button</param>
    protected void SetColour(Color colour)
    {
        this.button.GetComponent<Image>().color = colour;
    }

    /// <summary>
    /// Set the text in the button
    /// </summary>
    /// <param name="text">The new text to set</param>
    protected void SetText(string text)
    {
        this.button.GetComponentInChildren<Text>().text = text;
    }

    /// <summary>
    /// Initiailise <c>button</c>
    /// </summary>
    protected virtual void Awake()
    {
        this.button = GetComponent<Button>();
    }

    /// <summary>
    /// Every frame we need to check if we have a target 
    /// to enable or disable the interactability of the button
    /// </summary>
    private void Update()
    {
        this.button.interactable = this._HasTarget;
    }
}