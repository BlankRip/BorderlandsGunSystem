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
	public float wallRunSpeed;

	[Header("Crouch")]

	public float crouchSpeed;
	public float crouchYScale;
	private float notCrouchYScale;

	[Header("Slope movement")]

	public float maxSlopeAngle;
	public RaycastHit slopeHit;
	private bool exitSlope;

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
		wallRunning,
		air
	}

	public bool wallrunning;

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
		if(wallrunning)
		{
			currentMovementState = MovementStates.wallRunning;
			movementSpeed = wallRunSpeed;                                   //change later when slide implemented
		}

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

		//On slope movement

		if(OnSlope() && !exitSlope)
		{
			rb.AddForce(getSlopeMoveDirection() * movementSpeed * 20f, ForceMode.Force);

			// To remove the wierd upward force when going up a slope
			if(rb.velocity.y > 0)
			{
				rb.AddForce(Vector3.down * 80f, ForceMode.Force);
			}
		}

		if(grounded)
		{
			rb.AddForce(movementDirection.normalized * movementSpeed * 10f, ForceMode.Force);
		}
		else if(!grounded)
		{
			rb.AddForce(movementDirection.normalized * movementSpeed * 10f * airMultiplier, ForceMode.Force);
		}

		rb.useGravity = !OnSlope();
		

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
		//speed control on slope  -> makes you unable to jump, so use exit slope bool in playerJump fucntion

		if (OnSlope() && !exitSlope)
		{
			if (rb.velocity.magnitude > movementSpeed)
			{
				rb.velocity = rb.velocity.normalized * movementSpeed;
			}
		}
		else
		{
			Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);


			if (flatVel.magnitude > movementSpeed)
			{
				Vector3 limitedVel = flatVel.normalized * movementSpeed;
				rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
			}
		}

	}

	void playerJump()
	{
		exitSlope = true;

		rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

		rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
	}

	void resetJump()
	{
		exitSlope = false;
		readyToJump = true;
	}

	private bool OnSlope()
	{
		if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
		{
			float steepAngle = Vector3.Angle(Vector3.up, slopeHit.normal);
			return steepAngle < maxSlopeAngle && steepAngle != 0;
		}
		return false;
	}

	//Finding direction relative of slope

	private Vector3 getSlopeMoveDirection()
	{
		return Vector3.ProjectOnPlane(movementDirection, slopeHit.normal).normalized;      //ProjectOnPlane: Projects a vector onto a plane defined by a normal orthogonal to the plane

	}

}
