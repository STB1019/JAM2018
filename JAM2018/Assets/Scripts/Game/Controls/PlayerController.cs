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
            public float speed = 10;
            public float jumpVel = 10f;
            [HideInInspector]
            public float distToGrounded;
            public float height;
            public LayerMask ground;
            [HideInInspector]
            public float currentVelX;
            [HideInInspector]
            public float currentVelZ;
            public float maxVel = 5;
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

        Rigidbody rb;

        float forwardInput, turnInput, jumpInput;

        bool IsGrounded()
        {
            return Physics.Raycast(transform.position, Vector3.down, moveSetting.distToGrounded, moveSetting.ground);
        }

        void Start()
        {
            moveSetting.distToGrounded = (moveSetting.height / 2) + 0.1f;
            rb = GetComponent<Rigidbody>();
            forwardInput = turnInput = jumpInput = 0;
        }

        void GetInput()
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
            Jump();
            rb.velocity = transform.TransformDirection(velocity);
        }

        void Run()
        {
            if (((Mathf.Abs(forwardInput) > inputSetting.inputDelay) || (Mathf.Abs(turnInput) > inputSetting.inputDelay)) && IsGrounded())
            {
                velocity.z = Mathf.Clamp(forwardInput * moveSetting.speed, -moveSetting.maxVel, moveSetting.maxVel);
                velocity.x = Mathf.Clamp(turnInput * moveSetting.speed, -moveSetting.maxVel, moveSetting.maxVel);
            }
            else
            {
                velocity.x = 0;
                velocity.z = 0;
            }
        }

        void Jump()
        {
            if (jumpInput > 0 && IsGrounded())
            {
                velocity.y = moveSetting.jumpVel;
            }
            else if (jumpInput == 0 && IsGrounded())
            {
                velocity.y = 0;
                moveSetting.currentVelX = velocity.x;
                moveSetting.currentVelZ = velocity.z;
            }
            else if (!IsGrounded())
            {
                velocity.z = moveSetting.currentVelZ;
                velocity.x = moveSetting.currentVelX;
                velocity.y -= physSetting.downAccel;
            }
        }
    }
}