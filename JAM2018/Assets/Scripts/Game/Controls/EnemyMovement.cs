using UnityEngine;
using System.Collections;

namespace Scripts.Game.Controls
{
    public class EnemyMovement : MonoBehaviour
    {
        private Transform player;
        private RaycastHit hitInfo;
        private UnityEngine.AI.NavMeshAgent nav;
        private float distance;
        private Vector3 playerPos;
        private Vector3 playerDir;
        public float viewDistance = 5;
        public float attackDistance = 1.5f;
        public int viewLenght = 5;
        private CapsuleCollider collider;

        void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            collider = GetComponent<CapsuleCollider>();
            nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        }

        void Update()
        {
            Movement();
            Debug.DrawRay(collider.transform.TransformPoint(collider.center), playerDir);
            playerPos = player.position;
            playerDir = playerPos - transform.position;
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
            if (Physics.Raycast(collider.transform.TransformPoint(collider.center), playerDir, out hitInfo, viewLenght) && hitInfo.transform.tag == "Player" && distance > attackDistance)
            {

                Debug.Log("Player is in range and the enemy is following him");
                nav.SetDestination(player.position);
                nav.Resume();
            }

            else if (distance < attackDistance)
            {
                Debug.Log("The enemy is close enough to attack");
                nav.Stop(true);
                transform.LookAt(player.transform);
            }

            else if (distance > viewDistance)
            {
                nav.Stop(true);
            }
        }
    }
}