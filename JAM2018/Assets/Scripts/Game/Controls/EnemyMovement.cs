using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Transform player;               
    UnityEngine.AI.NavMeshAgent nav;         
    private float distance;
    public float viewDistance = 5;
    public float attackDistance = 1.5f;
          



    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player").transform;
        nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
    }


    void Update ()
    {
        distance = Vector3.Distance(player.position, transform.position);
        if (distance < viewDistance && distance > attackDistance)
        {
            Debug.Log("Activating mesh");
            nav.SetDestination (player.position);
            nav.Resume();
        }

        else if (distance > viewDistance)
        {
            Debug.Log("Far away");
            nav.Stop(true);
        }

        else if (distance < attackDistance)
        {
            Debug.Log("I'm Close"); 
            nav.Stop(true);
        }
        else
        {
            Debug.Log("Other");
        }
    } 
}