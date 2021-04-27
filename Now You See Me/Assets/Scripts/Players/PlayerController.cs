using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

/// <summary>
/// Abstract class containing features specific to all players. This includes
/// Get all the colliders in some range
/// Get the role of the palyer
/// Perform some action
/// The percentage of time it takes to complete tasks
/// Unstick the player
/// </summary>
public abstract class PlayerController : MonoBehaviourPun
{
    // The currently referenced player to target
    protected PlayerController _currentTarget;
    // The colliders in range
    protected Collider2D[] _collidingWith;
    protected float taskTimePercent = 1.0f;
    [SerializeField]
    private Text _nameText;
    private const float _RangeRadius = 2f;
    // The maximum number of colliders in _collidingWith
    private const int _MaxColliders = 1;
    
    /// <summary>
    /// Get this player's role
    /// </summary>
    /// <returns>The role of the player</returns>
    public abstract string GetRole();

    /// <summary>
    /// Perform some action specific to the player type
    /// Called by the action button
    /// </summary>
    public abstract void PerformAction();

    /// <summary>
    /// Get the percentage of time required to complete a task
    /// </summary>
    /// <returns></returns>
    public float GetTaskTimePercent()
    {
        return this.taskTimePercent;
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
    /// Initialise the camera movement target to this gameObject
    /// </summary>
    virtual protected void Awake()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected) { return; }
        CameraMovement.target = this.transform;
        _collidingWith = new Collider2D[_MaxColliders];
    }

    /// <summary>
    /// Initialise the action and unstick button to our player
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
        if (!photonView.IsMine && PhotonNetwork.IsConnected) { return; }
    }

    /// <summary>
    /// Detect the the gameObjects in range
    /// </summary>
    virtual protected void FixedUpdate()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected) { return; }  
        DetectObjects();
    }

    /// <summary>
    /// Detect the gameObjects in range and update <c>_collidingWith</c>
    /// </summary>
    private void DetectObjects()
    {
        Vector3 circleCentre = transform.position;
        this._collidingWith = Physics2D.OverlapCircleAll(circleCentre, _RangeRadius);
    }
}
