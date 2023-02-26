using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class playerMov : MonoBehaviour
{
	[Header("Movement")]

	public float movementSpeed;

	public float groundDrag;

	public float jumpForce;
	public float jumpCooldown;
	public float airMultiplier;
	bool readyToJump;

	[Header ("Keybinds")]

	public KeyCode jumpKey = KeyCode.Space;



	[Header("Ground Check")]

	public float playerHeight;
	public LayerMask whatIsGround;
	bool grounded;

	public Transform orientation;

	float horizontalInput;
	float verticalInput;

	Vector3 movementDirection;

	Rigidbody rb;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		rb.freezeRotation = true;

		horizontalInput = 0.0f;
		verticalInput = 0.0f;
		readyToJump = true;
	}

	private void Update()
	{
		basicInput();

		groundCheck();
		SpeedControl();

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
