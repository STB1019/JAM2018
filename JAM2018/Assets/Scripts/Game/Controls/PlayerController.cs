using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Game.Controls
{
    ///<summary>
    ///This script allows the player to move on the x and z axis and to jump on the y axis. Movement is done by changing
    ///the velocity of the rigidbody component. Jumping is allowed only when the player is touching the ground, this 
    ///check is made via a raycast down from the origin of the collider. 
    ///</summary>
    public class PlayerController : MonoBehaviour
    {
        [System.Serializable]
        public class MoveSettings
        {
            public float speed = 5; //The speed at which the player moves
            public float jumpVel = 5f; //The velocity applied to the jump
            public float height; //The height of the player collider 
            public LayerMask ground; //Layer mask that specifies what is ground
            public float maxVel = 10; //The maximum velocity the player can reach
            public float runSpeed = 1.2f; //The run speed 
        }

        [System.Serializable]
        public class PhysSettings
        {
            public float downAccel = 0.75f; //Accelleration downwards. Basically a gravity force
        }

        [System.Serializable]
        public class InputSettings
        {
            public float inputDelay = 0.1f; //Dead zone for the player input
            public string FORWARD_AXIS = "Vertical";
            public string STRAFE_AXIS = "Horizontal";
            public string JUMP_AXIS = "Jump";
        }

        public MoveSettings moveSetting = new MoveSettings();
        public PhysSettings physSetting = new PhysSettings();
        public InputSettings inputSetting = new InputSettings();

        private float forwardInput;
        private float strafeInput;
        private float jumpInput;
        private float currentVelX; 
        private float currentVelZ;
        public float distToGround;
        private Vector3 velocity = Vector3.zero;
        private Rigidbody rb;
        private Animator anim;
        private CapsuleCollider collider;


        void Start()
        {
            distToGround = (moveSetting.height / 2) + 0.1f;
            rb = GetComponent<Rigidbody>();
            collider = GetComponent<CapsuleCollider>();
            forwardInput = strafeInput = jumpInput = 0;
            anim = GetComponent<Animator>();
        }

        ///<summary>
        ///This method checks if the player is grounded using a raycast
        ///</summary>
        bool IsGrounded()
        {
            return Physics.Raycast(collider.transform.TransformPoint(collider.center), Vector3.down, distToGround, moveSetting.ground);
        }

        ///<summary>
        ///This method gathers the input from the player and stores it in a variable. jumpInput is a raw value that
        ///can only be 0 or 1
        ///</summary>
        void GetInput()
        {
            forwardInput = Input.GetAxis(inputSetting.FORWARD_AXIS);
            strafeInput = Input.GetAxis(inputSetting.STRAFE_AXIS);
            jumpInput = Input.GetAxisRaw(inputSetting.JUMP_AXIS);
        }

        void Update()
        {
            GetInput();
            Debug.Log(IsGrounded());
            Debug.DrawRay(collider.transform.TransformPoint(collider.center), Vector3.down * distToGround); 
            Animate();
        }

        ///<summary>
        ///This method is used to change the x and z components of the velocity vector accordingly to the player input
        ///If the input is greater than the input delay then the velocity is calculated and it's clamped between 
        ///the maximum speed and -(maximum speed).
        ///If the player is not moving then the velocity on the x and z axis is set to zero.
        ///</summary>
        void Move()
        {
            if (((Mathf.Abs(forwardInput) > inputSetting.inputDelay || (Mathf.Abs(strafeInput)) > inputSetting.inputDelay)) && IsGrounded())
            {
                velocity.z = Mathf.Clamp(forwardInput * moveSetting.speed, -moveSetting.maxVel, moveSetting.maxVel);
                velocity.x = Mathf.Clamp(strafeInput * moveSetting.speed, -moveSetting.maxVel, moveSetting.maxVel);
            }
            else
            {
                velocity.x = 0;
                velocity.z = 0;
            }

            if(RequestsRun())
            {
                velocity.z *= moveSetting.runSpeed;
                velocity.x *= moveSetting.runSpeed;
            }
        }

        ///<summary>
        ///This method allows the player to jump. If the player is grounded and he's not jumping then the current x and z
        ///velocity is stored in a temp variable. If the player is grounded and jumps then a velocity is applied upwards
        ///and if he's not grounded then a velocity downwards is applied but the x and z velocities stay constant.
        ///</summary>
        void Jump()
        {
            if (jumpInput > 0 && IsGrounded())
            {
                velocity.y = moveSetting.jumpVel;
            }
            else if (jumpInput == 0 && IsGrounded())
            {
                velocity.y = 0;
                currentVelX = velocity.x;
                currentVelZ = velocity.z;
            }
            else if(!IsGrounded())
            {
                velocity.z = currentVelZ;
                velocity.x = currentVelX;
                velocity.y -= physSetting.downAccel;
            }
        }

        void FixedUpdate()
        {
            Move();
            Jump();
            rb.velocity = transform.TransformDirection(velocity);
        }

        bool IsMoving()
        {
            if (Mathf.Abs(velocity.x)>0 || Mathf.Abs(velocity.z)>0)
            {
                return true;
            }
            else return false;
        }

        bool RequestsRun()
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                return true;
            }
            else return false;
        }

        bool IsIdle()
        {
            if(Mathf.Abs(velocity.x)==0 && Mathf.Abs(velocity.z)==0)
            {
                return true;
            }
            else return false;
        }

        void Animate()
        {
            if(IsIdle())
            {
                anim.SetBool ("isWalking", false);
                anim.SetBool ("isIdle", true);
            }
            else if(IsMoving())
            {
                anim.SetBool("isWalking", true);
                anim.SetBool("isIdle", false);  
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                anim.SetTrigger("attack");
            }
        }
    }
}