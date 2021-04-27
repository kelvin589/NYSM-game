using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/// <summary>
/// Manages the functionalities available through the in-game options menu.
/// </summary>
public class OptionMenuManager : MonoBehaviour
{
    // Reference to main audio mixer
    public AudioMixer gameAudioMixer;
    // Reference to volume slider in options menu
    public Slider slider;
    // Reference to fullscreen toggle in options menu
    public Toggle fullscreenToggle;

    /// <summary>
    /// Called before the first frame update.
    /// Sets up the volume slider in the options menu based on the player preference.
    /// </summary>
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("Game Volume", 0.75f);
    }

    /// <summary>
    /// Changes the volume level based on the slider value in the options menu.
    /// </summary>
    /// <param name="volumeLevel"></param>
    public void SetVolumeLevel (float volumeLevel)
    {
        // UI slider and mixer increase/decrease on different scales. This is to ensure they increase on the same scale
        gameAudioMixer.SetFloat("GameVolume", Mathf.Log10(volumeLevel)*20);
        // Saves slider value when changed
        PlayerPrefs.SetFloat("Game Volume", volumeLevel);
    }

    /// <summary>
    /// Allows a player to switch between fullscreen and normal view.
    /// </summary>
    public void ToggleFullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

}
