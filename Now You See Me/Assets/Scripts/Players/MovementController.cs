using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/// <summary>
/// Attach to a rigidbody to:
/// Provide movement via horizontal and vertical Input.
/// Provide support for animation through the use of Animator parameters
/// moveX, moveY and moving.
/// Synchronise the state of rigidbody velocity across Photon
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[DisallowMultipleComponent]
public class MovementController : MonoBehaviourPun, IPunObservable
{
	[Tooltip("Set the move speed")]
    public float moveSpeed = 3;
    private float _moveX;
    private float _moveY;
	private Rigidbody2D _rigidbody;
	private Vector2 _moveDirection;
    private Animator _animator;
	private FacingDirection _facingDirection;
	string test;

	/// <summary>
	/// The state of direction of a gameObject
	/// </summary>
	public enum FacingDirection 
	{
		UP, DOWN, LEFT, RIGHT
	}

	/// <summary>
	/// Get the <see cref="_facingDirection"/>; the direction <c>_rigidbody</c> is currently facing.
	/// </summary>
	/// <returns>A FacingDirection</returns>
	public FacingDirection GetFacingDirection() 
	{
		return this._facingDirection;
	}

	/// <summary>
	/// Synchronise the velocities of this <c>_rigidbody</c>
	/// </summary>
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.IsWriting)
		{
			stream.SendNext(_rigidbody.velocity);
		}
		else
		{
			Vector2 newVelocity = (Vector2)stream.ReceiveNext();
			this._rigidbody.velocity = newVelocity;
		}
	}

	/// <summary>
	/// Initialise <c>_rigidbody</c>
	/// </summary>
	private void Awake()
	{
        this._rigidbody = GetComponent<Rigidbody2D>();
	}

	/// <summary>
	/// Initialise <c>_animator</c> and set initial <c>_facingDirection</c> to DOWN
	/// </summary>
    private void Start()
	{
		this._animator = GetComponent<Animator>();
		this._facingDirection = FacingDirection.DOWN;
	}

	/// <summary>
	/// Every frame call <see cref="ProcessFacingDirection()"/> to update the facing direction
	/// and call <see cref="ProcessInputs"/> to update the move direction depending on inputs
	/// </summary>
	private void Update() 
	{
		ProcessFacingDirection();
		if (!photonView.IsMine && PhotonNetwork.IsConnected) { return; }
		ProcessInputs();
	}

	/// <summary>
	/// Every fixed frame-rate frame we call the <see cref="Move"/> method to move
	/// </summary>
	private void FixedUpdate()
	{
		if (!photonView.IsMine && PhotonNetwork.IsConnected) { return; }
		Move();
	    //Physics Calculations
	}

	/// <summary>
	/// Set the velocity of the <c>_rigidbody</c>
	/// </summary>
	private void Move()
	{
		_rigidbody.velocity = new Vector2(_moveDirection.x * moveSpeed, _moveDirection.y * moveSpeed);
	}

	/// <summary>
	/// Determine facing direction using velocities
	/// </summary>
	private void ProcessFacingDirection() 
	{
		Vector2 velocity = _rigidbody.velocity;
		_moveX = velocity.x;
		_moveY = velocity.y;
		if (_moveY<0) 
		{
			_facingDirection = FacingDirection.DOWN;
			test = "Down";
			return;
		}
		if (_moveX<0) 
		{
			_facingDirection = FacingDirection.LEFT;
			return;
		}
		if (_moveX>0) 
		{
			_facingDirection = FacingDirection.RIGHT;
			return;
		}
		if (_moveY>0) 
		{
			_facingDirection = FacingDirection.UP;
			return;
		}
	}

	/// <summary>
	/// Set <c>_moveX</c> and <c>_moveY</c> based on the Input, and the <c>_moveDirection</c> based on this.
	/// Calls <see cref="ProcessAnimation"/>
	/// </summary>
	private void ProcessInputs()
	{
		_moveX = Input.GetAxisRaw("Horizontal");
		_moveY = Input.GetAxisRaw("Vertical");
        ProcessAnimation();
		_moveDirection = new Vector2(_moveX, _moveY).normalized;
	}

	/// <summary>
	/// Process animation by setting the parameters of the <c>_animator</c>
	/// If the <c>_moveDirection</c> is not zero, then we must be moving so set <c>moving</c>to true.
	/// </summary>
    private void ProcessAnimation()
    {
		if(_moveDirection != Vector2.zero)
		{
			_animator.SetFloat("moveX", _moveDirection.x);
			_animator.SetFloat("moveY", _moveDirection.y);
			_animator.SetBool("moving", true);
		} else 
		{
			_animator.SetBool("moving", false);
		}
    }
}
