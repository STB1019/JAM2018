using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharpUtilities;
using System;
using Assets.Scripts.Game.Commons;

namespace Scripts.Game.Controls {




    public enum GroundCollisionDetectionStrategy
    {
        BYTAG,
        BYNAME
    }
    
   

    ///This script allows us to check if the player is grounded using a RayCast downwards. It can be used for NPCs and Players.
    ///It uses a Layer to ignore the Player so we need to change the layer to "8-Ground" on every jumpable object.
    ///To properly use the script attach it to an empty GameObject located in the center of the player collider.
    ///Then reference to its "isGounded" variable in the PlayerController script to allow the jump.
    ///The variable height should be set to the Player's collider height



    public class JumpScript : MonoBehaviour
    {
        public bool isGrounded {get; set;}
        public int jumpForce;
        [HideInInspector] //Hides layerMask
        public int layerMask = 1 << 8; //This layerMask includes every object except those whose layer is 8
        public int height;
        
        void Start()
        {
            isGrounded = false;
        }

        void Update()
        {
            layerMask = ~layerMask; //We reverse the layer excluding every object besides those whose layer is 8
            Rigidbody rb = GetComponent<Rigidbody>();
            Vector3 colliderOrigin = transform.position;
            Debug.Log(colliderOrigin);

            if (Physics.Raycast(colliderOrigin, Vector3.down, (height/2) + 0.5f, layerMask))
            {
                isGrounded = true;
            }
        }
    }
}
