using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attach this script to the camera to make it follow a player (the <c>target</c>)
/// Credits to: Mister Taft Creates https://youtu.be/OWJa6lcFTXk
/// </summary>
public class CameraMovement : MonoBehaviour
{
    public static Transform target;
    [Tooltip("Roughly how quickly camera moves to target")]
    public float speed;
    [Tooltip("Max bound for the camera")]
    public Vector2 maxPosition;
    [Tooltip("Min bound for the camera")]
    public Vector2 minPosition;

    /// <summary>
    /// Check if the position of the camera is on the target.
    /// If not, Lerp it to the target position.
    /// </summary>
    private void LateUpdate()
    {
        if (transform.position != target.position) 
        {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            
            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);
            transform.position = Vector3.Lerp(transform.position, targetPosition, speed);
        }
    }
}
