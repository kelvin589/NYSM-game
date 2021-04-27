using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

/// <summary>
/// Contains features specific to all palyers. This includes getting
/// all the colliders in range using a overlapcirclecast
/// and get the role of the player
/// </summary>
public abstract class PlayerController : MonoBehaviourPun
{
    // Which player am I currently targeting
    protected PlayerController _currentTarget;
    // The colliders in range of us
    protected Collider2D[] _collidingWith;
    [SerializeField]
    private Text _nameText;
    private LineRenderer _lineRenderer;
    
    /// <summary>
    /// Initialise the camera movement target to this object
    /// </summary>
    virtual protected void Awake()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected) { return; }
        CameraMovement.target = this.transform;
        _collidingWith = new Collider2D[1];
    }

    /// <summary>
    /// Initialise the action button to our player
    /// Set our nickname
    /// </summary>
    virtual protected void Start()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected) { return; }
        UIActionButton.instance.SetCurrentTarget(this);
        UIStuckButton.instance.SetCurrentTarget(this);
        // if (photonView.IsMine)
        // {
        //     _nameText.text = PhotonNetwork.NickName;
        // }
        // if (!photonView.IsMine)
        // {
        //     _nameText.text = photonView.Owner.NickName;
        // }
    }

    virtual protected void Update()
    {

    }

    /// <summary>
    /// Detect the players/game objects in range of us
    /// </summary>
    virtual protected void FixedUpdate()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected) { return; }  
        DetectPlayers();
    }

    /// <summary>
    /// Unstick the player (teleport to spawn) when stuck button is pressed
    /// </summary>
    public void UnStick()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected) { return; }
        this.transform.position = new Vector3(-33, -8, 0);
    }

    /// <summary>
    /// Detect the players in range and set a protected variable with the array of these Collider2D
    /// </summary>
    private void DetectPlayers()
    {
        //LayerMask.GetMask("Character");
        Vector3 circleCentre = transform.position;
        float radius = 2f;
        this._collidingWith = Physics2D.OverlapCircleAll(circleCentre, radius);
        
        // stuff on other players ghost/student/virus if you want to find specific game object tag type
        // foreach (Collider2D t in hits) {
        //     // you need to get really close if you enable the below since there may be wall
        //     //if (t.transform.gameObject.tag == "Collision") {return;};
        //     if (t.transform.gameObject.tag == "Player" && t.transform.gameObject != this.gameObject) 
        //     {
        //         //Debug.Log(t.gameObject.name);
        //         currentTarget = t.GetComponent<PlayerController>();
        //         //Debug.Log(currentTarget.gameObject.name);
        //         UIActionButton.instance.HasTarget=true;
        //         return;
        //     }
        // }
        // UIActionButton.instance.HasTarget = false;
    }

    /// <summary>
    /// Get what role this player is
    /// </summary>
    /// <returns>Role the player is</returns>
    public abstract string GetRole();

    /// <summary>
    /// Perform some action specific to the player type
    /// This is called by the action button
    /// </summary>
    public abstract void PerformAction();
}
