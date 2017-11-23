using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharpUtilities;
using System;
using Assets.Scripts.Game.Commons;

namespace Scripts.Game.Controls
{
    ///<summary>
    ///This script allows us to check if the player is grounded using a RayCast downwards. It can be used for NPCs and Players.
    ///It uses a Layer to ignore the Player so we need to change the layer to "8-Ground" on every jumpable object.
    ///Attach this script to the object we want to be jumping and recall the RequestJump() method
    ///The variable height should be set to the object's collider height
    ///</summary>

    public class JumpScript : MonoBehaviour
    {
        public bool IsGrounded {get; set;}
        [HideInInspector] //Hides layerMask
        public int layerMask = 1 << 8; //This layerMask includes every object except those whose layer is 8
        public float height;
        private float halfHeight {get; set;}
        public Rigidbody rb;
        
        void Start()
        {
            IsGrounded = Physics.Raycast(transform.position, Vector3.down, halfHeight+0.01f, layerMask);
            halfHeight = height/2;
        }

        void RequestJump(GameObject go, float jumpforce)
        {
            if (IsGrounded)
            {
                go.GetComponent<Rigidbody>().AddForce(transform.up * jumpforce, ForceMode.Impulse);
            }
        }
    }
}




