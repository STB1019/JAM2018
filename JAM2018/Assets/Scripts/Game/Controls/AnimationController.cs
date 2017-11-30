using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour 
{
	private Rigidbody rb;
	private Animator anim;
	private float distance;
    public float viewDistance = 5;
    public float attackDistance = 1.2f;
    Transform player;    

	void Start () 
	{
		rb = GetComponent<Rigidbody>();
		anim = GetComponent<Animator>();
	}
	
	void Update () 
	{
		Animator();
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		distance = Vector3.Distance(player.position, transform.position);
	}

	void Animator()
	{
		if (distance < viewDistance && distance > attackDistance)
		{
			anim.SetBool ("isWalking", true);
        	anim.SetBool ("isIdle", false);
		}

		else if (distance > viewDistance || distance < attackDistance)
		{
			Debug.Log("I'm stopping");
			anim.SetBool("isWalking", false);
            anim.SetBool("isIdle", true);  
		}

		if (distance < attackDistance)
		{
			Debug.Log("Attack");
			anim.SetTrigger("attack");
		}
	}
}
