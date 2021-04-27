using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public bool No;
    public Menu[] menus; //creates array of Menus
    /// <summary>
    /// Called when the object is activated.
    /// </summary>
    void Awake()
        {
            Instance = this;
        }


    /// <summary>
    /// Allows a menu to be opened using its name in the form of a string.
    /// It will also close any other menus that are currently open.
    /// </summary>
    /// <param name="menuName"></param>
    public void OpenMenu(string menuName)
    {
        for (int i = 0; i < menus.Length; i++) //loops through the menus
        {
            if (menus[i].menuName == menuName)
            {
                menus[i].Open(); //open desired menu
            }
            else if (menus[i].open) 
            { 
                CloseMenu(menus[i]); //close menu
            }

            if (menuName.Contains("Hover"))
            {
                CloseMenu(menus[i]); //close menu
            }
        }
    }

/// <summary>
/// Allows a menu to be opened via a reference to the menu.
/// It will also close any other menus that are currently open.
/// </summary>
/// <param name="menu"></param>
public void OpenMenu(Menu menu) // Opens a Menu Object
    {

        for (int i = 0; i < menus.Length; i++) //loop through the menus
        {
            if (menus[i].open)
            {
                CloseMenu(menus[i]); //close menu
            }
        }
            menu.Open(); //open desired menu
    }

    /// <summary>
    /// Closes a menu.
    /// </summary>
    /// <param name="menu"></param>
    public void CloseMenu(Menu menu)
    {
        menu.Close();

    }


}
