using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Game.Controls
{

	public class PlayerController : MonoBehaviour
	{
	    [System.Serializable]
	    public class MoveSettings
	    {
		public float speed = 12;
		public float rotateVel = 1;
		public float jumpVel = 25f;
		public float distToGrounded;
		public float height;
		public LayerMask ground;
		public float timeToStop;
		public float currentVelX;
		public float currentVelZ;
	    }

	    [System.Serializable]
	    public class PhysSettings
	    {
		public float downAccel = 0.75f;
	    }

	    [System.Serializable]
	    public class InputSettings
	    {
		public float inputDelay = 0.1f;
		public string FORWARD_AXIS = "Vertical";
		public string TURN_AXIS = "Horizontal";
		public string JUMP_AXIS = "Jump";
	    }

	    public MoveSettings moveSetting = new MoveSettings();
	    public PhysSettings physSetting = new PhysSettings();
	    public InputSettings inputSetting = new InputSettings();



	    Vector3 velocity = Vector3.zero;

	    Quaternion targetRotation;

	    Rigidbody rb;

	    float forwardInput, turnInput, jumpInput;

	    public Quaternion TargetRotation
	    {
		get { return targetRotation; }
	    }

	    bool IsGrounded()
	    {
		return Physics.Raycast(transform.position, Vector3.down, moveSetting.distToGrounded, moveSetting.ground);
	    }

	    void Start()
	    {
		moveSetting.timeToStop = 0;
		moveSetting.distToGrounded = (moveSetting.height / 2) + 0.1f;
		targetRotation = transform.rotation;
		rb = GetComponent<Rigidbody>();

		forwardInput = turnInput = jumpInput = 0;
	    }

	    void GetInput()using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class PlayerLook : MonoBehaviour
	{
	    public Transform playerBody;
	    public float mouseSensitivity;

	    float xAxisClamp = 0.0f;

	    void Awake()
	    {
		Cursor.lockState = CursorLockMode.Locked;
	    }

	    void Update()
	    {
		RotateCamera();       
	    }

	    void RotateCamera()
	    {
		float mouseX = Input.GetAxis("Mouse X");
		float mouseY = Input.GetAxis("Mouse Y");

		float rotAmountX = mouseX * mouseSensitivity;
		float rotAmountY = mouseY * mouseSensitivity;

		xAxisClamp -= rotAmountY;

		Vector3 targetRotCam = transform.rotation.eulerAngles;
		Vector3 targetRotBody = playerBody.rotation.eulerAngles;

		targetRotCam.x -= rotAmountY;
		targetRotCam.z = 0;
		targetRotBody.y += rotAmountX;

		if(xAxisClamp > 90)
		{
		    xAxisClamp = 90;
		    targetRotCam.x = 90;
		}
		else if(xAxisClamp < -90)
		{
		    xAxisClamp = -90;
		    targetRotCam.x = 270;
		}

		print(mouseY);


		transform.rotation = Quaternion.Euler(targetRotCam);
		playerBody.rotation = Quaternion.Euler(targetRotBody);


	    }



	}

	    {
		forwardInput = Input.GetAxis(inputSetting.FORWARD_AXIS);
		turnInput = Input.GetAxis(inputSetting.TURN_AXIS);
		jumpInput = Input.GetAxisRaw(inputSetting.JUMP_AXIS);
	    }

	    void Update()
	    {
		GetInput();
	    }

	    void FixedUpdate()
	    {
		Run();
		Debug.Log(Grounded());
		Jump();
		rb.velocity = transform.TransformDirection(velocity);
	    }

	    void Run()
	    {
		if (((Mathf.Abs(forwardInput) > inputSetting.inputDelay) || (Mathf.Abs(turnInput) > inputSetting.inputDelay)) && IsGrounded)
		{
		    velocity.z = forwardInput * moveSetting.speed;
		    velocity.x = turnInput * moveSetting.speed;
		    moveSetting.timeToStop = 0;
		}
		else
		{
		    velocity = Vector3.zero;
		}
	    }

	    void Jump()
	    {
		if (jumpInput > 0 && Grounded())
		{
		    velocity.y = moveSetting.jumpVel;
		    currentVelX = velocity.x;
		    currentVelY = velocity.y;
		}
		else if (jumpInput == 0 && Grounded())
		{
		    velocity.y = 0;
		}
		else if (!IsGrounded())
		{
		    velocity.y -= physSetting.downAccel;
		    velocity.x = currentVelX;
		    velocity.z = currentVelZ;
		}
	    }
	}
}
