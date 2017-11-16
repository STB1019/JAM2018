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
	///in line 29 to associate it with the proper JumpTrigger (see JumpScript.cs for explanation)


	public class PlayerController : MonoBehaviour
	{
		public int jumpForce;

    
		void Update()
		{

			var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
			var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

			transform.Rotate(0, x, 0);
			transform.Translate(0, 0, z);

			GameObject jumpTrigger = GameObject.Find("JumpTrigger");
			JumpScript jumpScript = jumpTrigger.GetComponent<JumpScript>(); 
			Rigidbody rb = GetComponent<Rigidbody>();

			if (Input.GetKeyDown(KeyCode.S) && jumpScript.isGrounded==true)
			{
				Debug.Log("I'm Jumping");
				Vector3 jforce = new Vector3(0, jumpForce, 0);
				rb.AddForce(jforce, ForceMode.Impulse);
				jumpScript.isGrounded=false;
			}
		}
	}
}
