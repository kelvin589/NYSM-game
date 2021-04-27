using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using ExitGames.Client.Photon;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class ProgressBar : MonoBehaviourPunCallbacks
{
//     public Slider slider;
//     private float currentValue = 0f;
//     private bool _HasCollider;
//     public float CurrentValue;
//    // { 
//   //      get
//     //    {
//       //      return currentValue;
//        // }
// //        set
//      //   {
// //            currentValue = value;
// //            slider.value = currentValue;
//        // }
//     //}

//     // Use this for initialization
//     void Start()
//     {
//         CurrentValue = 0f;
//     }

//     // Update is called once per frame
//     void Update()
//     {
//     if (Input.GetKey(KeyCode.E)) 
//         {
//             CurrentValue += 0.0043f;
//         }
//         if (currentValue >= 10f) {
//             currentValue = 0;
//         }
//         //Debug.Log(currentValue);
//     }

//     void OnTriggerEnter2D(Collider2D other)
//     {
//         _HasCollider = true;
//         //textBox.text = "Press E to pickup";
//     }

//     void OnTriggerExit2D(Collider2D other)
//     {
//         _HasCollider = false;
//         //textBox.text = "No item in range";
//     }


    private Text progressPercentage;

    /// <summary>
    /// Initialise the Text component
    /// </summary>
    void Awake()
    {
        this.progressPercentage = GetComponentInChildren<Text>();
    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        double percentage = (((double)TaskList.GetCompleteSize()) / TaskList.GetSize()) * 100;
        progressPercentage.text = ((int)percentage).ToString() + "%";
    }
}
