using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the opening and closing of menus.
/// </summary>
public class Menu : MonoBehaviour
{
	public string menuName;
	public bool open; //is the menu open?

	/// <summary>
    /// Opens a menu.
    /// </summary>
	public void Open()
	{

			open = true;
			gameObject.SetActive(true); //display the menu
	}

	/// <summary>
    /// Closes a menu.
    /// </summary>
	public void Close()
	{
		open = false;
		gameObject.SetActive(false); //hide the menu
	}
}