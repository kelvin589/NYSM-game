using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

/// <summary>
/// Defines the behaviour that every player should have:
/// <list type="bullet">
/// <item><description><para> Display range detection using <see cref="LineRenderer"/></para></description></item>
/// <item><description><para> Execute something if in range</para></description></item>
/// </list>
/// </summary>
[RequireComponent(typeof(LineRenderer))]
[DisallowMultipleComponent]
public class Old_PlayerController : MonoBehaviourPun
{
    [SerializeField]
    [Tooltip("The detection range")]
    private float _range = 10.0f; 
    private LineRenderer _lineRenderer; 
    private PlayerController _currentTarget;
    public GameObject playerprefab;//prefab for player PHOTON // what is this??
    [SerializeField] private Text nameText;

    /// <summary>
    /// Set the <c>_lineRenderer</c> and the <see cref="CameraMovement.target"/> to this transform.
    /// </summary>
    void Awake()
    {

        if (!photonView.IsMine && PhotonNetwork.IsConnected) { return; }
        _lineRenderer = GetComponent<LineRenderer>();
        CameraMovement.target = this.transform;
    }

    /// <summary>
    /// Start the coroutine <see cref="SearchForPlayers"/> to determine players in range.
    /// Also set the button target to this player
    /// </summary>
    void Start()
    {
        // coroutine can have its action split through multiple 
        // frames minutes or other time frames
        // use to continuously detect target
        // better than using update (called every frame)
        // basically just wait a little so dont execute every frame

        if (!photonView.IsMine && PhotonNetwork.IsConnected) { return; }
        StartCoroutine(SearchForPlayers());
        //UIActionButton.instance.currentPlayer = this;

        if (photonView.IsMine)
        {
            nameText.text = PhotonNetwork.NickName;
        }
        if (!photonView.IsMine)
        {
            nameText.text = photonView.Owner.NickName;
        }

    }

    /// <summary>
    /// A coroutine to determine the players in range
    /// </summary>
    IEnumerator SearchForPlayers() 
    {
        while (true)
        {
            // The new target
            PlayerController newTarget = null;
            // Find all objects of type <c>PlayerController</c> and add to a list
            PlayerController[] playerList = FindObjectsOfType<PlayerController>();
            
            // Looping through each <c>PlayerController</c> in the list to perform some action
            foreach(PlayerController player in playerList) 
            {
                // Ignore this playercontroller
                if (player == this) 
                {
                    continue;
                }

                // Work out the distance from us to the other player
                float distance = Vector3.Distance(transform.position, player.transform.position);
                // If they are out of range, move on to next player
                if (distance > _range) 
                {
                    continue;
                }

                // A player has been found so set <c>newTarget</c> to it
                newTarget = player;
                break;
            }

            // Set the current target
            _currentTarget = newTarget;
            // Disable the button if we have no current target else enable it
            UIActionButton.instance.SetHasTarget(_currentTarget != null);
            // Pause execution for 0.25s
            yield return new WaitForSeconds(0.25f);  
        }
    }

    /// <summary>
    /// Use the line renderer to display a line to the current target (if there is one).
    /// </summary>
    void Update() 
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected) { return; }       
        if (_currentTarget != null) 
        {
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, _currentTarget.transform.position);
        } else
        {
            _lineRenderer.SetPosition(0, Vector3.zero);
            _lineRenderer.SetPosition(1, Vector3.zero);
        }
    }

    /// <summary>
    /// Perform some action on the current target depending on if I am a Ghost, Virus or Student
    /// </summary>
    public void DoSomethingTo() {
        GhostController ImGhost = this.gameObject.GetComponent<GhostController>();
        VirusController ImVirus = this.gameObject.GetComponent<VirusController>();
        StudentController ImStudent = this.gameObject.GetComponent<StudentController>();
        if (_currentTarget == null)
        {
            return;
        }
        //TODO: RPC for the functions below
        //run ghost function if i am a ghost
        if (ImGhost != null) 
        {
            //ImGhost.PushVirus(_currentTarget.gameObject);
        }
        //run virus function if i am a virus
        if (ImVirus != null) 
        {
            //ImVirus.kill(_currentTarget.gameObject);
        }
        //run student function if i am a student
        if (ImStudent != null) 
        {
            //ImStudent.action();
        }
        Debug.Log(this + " Do something to: " + _currentTarget);
    }
}
