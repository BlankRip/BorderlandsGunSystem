using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class playerMov : MonoBehaviour
{
	[Header("Movement")]

	private float movementSpeed;
	public float walkSpeed;
	public float sprintSpeed;

	public float groundDrag;

	public float jumpForce;
	public float jumpCooldown;
	public float airMultiplier;
	bool readyToJump = true;

	[Header("Crouch")]

	public float crouchSpeed;
	public float crouchYScale;
	private float notCrouchYScale;

	[Header ("Keybinds")]

	public KeyCode jumpKey = KeyCode.Space;
	public KeyCode sprintKey = KeyCode.LeftShift;
	public KeyCode crouchKey = KeyCode.LeftControl;


	[Header("Ground Check")]

	public float playerHeight;
	public LayerMask whatIsGround;
	bool grounded;

	public Transform orientation;

	float horizontalInput;
	float verticalInput;

	Vector3 movementDirection;

	Rigidbody rb;

	public MovementStates currentMovementState;

	public enum MovementStates
	{
		walking,
		sprinting,
		crouching,
		air
	}

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		rb.freezeRotation = true;

		notCrouchYScale = transform.localScale.y;

	}

	private void Update()
	{
		basicInput();

		groundCheck();
		SpeedControl();
		movementStateHandler();

	}
	private void FixedUpdate()
	{
		MovePlayer();
	}

	void basicInput()
	{
		horizontalInput = Input.GetAxisRaw("Horizontal");
		verticalInput = Input.GetAxisRaw("Vertical");

		//jump
		if(Input.GetKey(jumpKey) && readyToJump && grounded)
		{
			readyToJump = false;

			playerJump();

			Invoke(nameof(resetJump), jumpCooldown);
		}

		//Crouch

		if(Input.GetKeyDown(crouchKey))
		{
			transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
			rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
		}
		if(Input.GetKeyUp(crouchKey))
		{
			transform.localScale = new Vector3(transform.localScale.x, notCrouchYScale, transform.localScale.z);
		}

	}


	private void movementStateHandler()
	{
		//if(Input.GetKey(crouchKey) && grounded)
		//{
		//	currentMovementState = MovementStates.crouching;
		//	movementSpeed = crouchSpeed;
		//	Debug.Log(currentMovementState);
		//}

		if(grounded && Input.GetKey(sprintKey))
		{
			currentMovementState = MovementStates.sprinting;
			movementSpeed = sprintSpeed;
		}
		else if(Input.GetKey(crouchKey) && grounded)
		{
			currentMovementState = MovementStates.crouching;
			movementSpeed = crouchSpeed;
		}
		else if(grounded)
		{
			currentMovementState = MovementStates.walking;
			movementSpeed = walkSpeed;
		}
		else
		{
			currentMovementState = MovementStates.air;
		}
	}


	void MovePlayer()
	{
		movementDirection = (orientation.forward * verticalInput) + (orientation.right * horizontalInput);

		if(grounded)
		{
			rb.AddForce(movementDirection.normalized * movementSpeed * 10f, ForceMode.Force);
		}
		else if(!grounded)
		{
			rb.AddForce(movementDirection.normalized * movementSpeed * 10f * airMultiplier, ForceMode.Force);
		}
		

	}

	void groundCheck()
	{
		grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

		if(grounded)
		{
			rb.drag = groundDrag;
		}
		else
		{
			rb.drag = 0;
		}
	}

	void SpeedControl()
	{

		Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);


		if(flatVel.magnitude > movementSpeed)
		{
			Vector3 limitedVel = flatVel.normalized * movementSpeed;
			rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
		}

	}

	void playerJump()
	{
		rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

		rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
	}

	void resetJump()
	{
		readyToJump = true;
	}

}
