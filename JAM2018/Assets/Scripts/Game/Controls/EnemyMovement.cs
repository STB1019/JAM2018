using UnityEngine;
using System.Collections;

namespace Scripts.Game.Controls
{
    public class EnemyMovement : MonoBehaviour
    {
        private Transform player;
        private UnityEngine.AI.NavMeshAgent nav;
        private float distance;
        public float viewDistance = 5;
        public float attackDistance = 1.5f;

        void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        }

        void Update()
        {
            Movement();
        }

        /// <summary>
        /// This method updates the NavMesh Agent thus allowing the enemy to move.
        /// If the player is within the enemy's range then the enemy will move towards
        /// the player. Otherwhise he will stop. If the player is within melee range
        /// then the enemy will always face the player.
        /// </summary>
        void Movement()
        {
            distance = Vector3.Distance(player.position, transform.position);
            if (distance < viewDistance && distance > attackDistance)
            {
                Debug.Log("Player is in range and the enemy is following him");
                nav.SetDestination(player.position);
                nav.Resume();
            }

            else if (distance > viewDistance)
            {
                Debug.Log("The enemy is too far to see the player");
                nav.Stop(true);
            }

            else if (distance < attackDistance)
            {
                Debug.Log("The enemy is close enough to attack");
                nav.Stop(true);
                transform.LookAt(player.transform);
            }
            else
            {
                Debug.Log("There's an error");
            }
        }
    }
}