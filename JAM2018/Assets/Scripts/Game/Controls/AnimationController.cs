using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Game.Controls
{
    public class AnimationController : MonoBehaviour
    {
        private Animator anim;
        private float distance;
        private Transform player;
        public float viewDistance = 5;
        public float attackDistance = 1.2f;

        void Start()
        {
            anim = GetComponent<Animator>();
        }

        void Update()
        {
            Animator();
            player = GameObject.FindGameObjectWithTag("Player").transform;
            distance = Vector3.Distance(player.position, transform.position);
        }

        /// <summary>
        /// This method is used to animate the enemy. If the player is too far away then the
        /// enemy will stay in idle animation. As soon as the player enters his vision range
        /// the enemy will start the walking animation. If the enemy enters the enemy's melee
        /// range then the attack animation will start.
        /// </summary>
    	void Animator()
        {
            if (distance < viewDistance && distance > attackDistance)
            {
                anim.SetBool("isWalking", true);
                anim.SetBool("isIdle", false);
                anim.SetBool("isAttacking", false);
            }

            else if (distance > viewDistance || distance < attackDistance)
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isIdle", true);
                anim.SetBool("isAttacking", false);
            }

            if (distance < attackDistance)
            {
                anim.SetBool("isAttacking", true);
                anim.SetBool("isIdle", false);
                anim.SetBool("isWalking", false);
            }
        }
    }
}
