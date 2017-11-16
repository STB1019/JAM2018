using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharpUtilities;
using System;
using Assets.Scripts.Game.Commons;

namespace Scripts.Game.Controls 
{

	///This is the basic Player Controller script.
	///Attach it to the Player to get him moving. To allow the jump you should change the parameters of GameObject.Find
	///in line 30 to associate it with the proper JumpTrigger (see JumpScript.cs for explanation)


	public class PlayerController : MonoBehaviour
	{
		public int jumpForce;

    
		void FixedUpdate()
		{

			var xMov = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
			var zMov = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

			transform.Rotate(0, xMov, 0);
			transform.Translate(0, 0, zMov);

			GameObject jumpTrigger = GameObject.Find("JumpTrigger"); //Remember to associate the right object here
			JumpScript jumpScript = jumpTrigger.GetComponent<JumpScript>(); 
			Rigidbody rb = GetComponent<Rigidbody>();

			if (Input.GetKeyDown(KeyCode.J) && jumpScript.isGrounded==true)
			{
				Debug.Log("I'm Jumping");
				Vector3 jforce = new Vector3(0, jumpForce, 0);
				rb.AddForce(jforce, ForceMode.Impulse);
				jumpScript.isGrounded=false;
			}
		}
	}
}
