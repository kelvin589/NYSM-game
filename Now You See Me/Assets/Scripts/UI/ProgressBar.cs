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
    private RectTransform progressColour;
    private int maxLeft;

    /// <summary>
    /// Initialise the Text component
    /// </summary>
    void Awake()
    {
        //0-142: 142 * percentage - 142
        //142 is the left (0%) to 0 (100%)
        this.progressPercentage = GetComponentInChildren<Text>();
        this.progressColour = this.transform.GetChild(1).gameObject.GetComponent<RectTransform>();
        maxLeft = 142;
        //Debug.Log(progressColour.offsetMax.x);
        //Debug.Log(progressColour.offsetMin.x);
        //progressColour.sizeDelta = new Vector2(60, 0);
        //progressColour.offsetMax = new Vector2(-142, 0);
        //progressColour.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 60);
    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        double percentage = (((double)TaskList.GetCompleteSize()) / TaskList.GetSize());
        progressPercentage.text = ((int)(percentage * 100)).ToString() + "%";
        progressColour.offsetMax = new Vector2((float)((maxLeft * percentage) - maxLeft), 0);
    }
}