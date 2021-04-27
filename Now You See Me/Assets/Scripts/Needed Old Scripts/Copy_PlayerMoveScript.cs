using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//for testing purposes
public class Copy_PlayerMoveScript : MonoBehaviour
{
	public Rigidbody2D rb;
	public float moveSpeed = 3;
	private Vector2 MoveDirection;
	private Animator animator;

	public void Start()
	{
		animator = GetComponent<Animator>();
		Debug.Log("test");
	}

	public void Update() 
	{
		ProcessInputs();
	}

	private void FixedUpdate()
	{
		Move();
	//Physics Calculations
	}

	void ProcessInputs()
	{
		
		float moveX = Input.GetAxisRaw("Horizontal");
		float moveY = Input.GetAxisRaw("Vertical");

		//process animation
		Vector3 change = new Vector3(0,0,0);
		change.x = moveX;
		change.y = moveY;
		if(change != Vector3.zero)
		{
			animator.SetFloat("moveX", change.x);
			animator.SetFloat("moveY", change.y);
			animator.SetBool("moving", true);
		} else {
			animator.SetBool("moving", false);
		}

		MoveDirection = new Vector2(moveX, moveY).normalized;

	}

	void Move()
	{
		rb.velocity = new Vector2(MoveDirection.x * moveSpeed, MoveDirection.y * moveSpeed);
	}

}
