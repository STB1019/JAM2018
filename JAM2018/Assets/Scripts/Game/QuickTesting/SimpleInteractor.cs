using Scripts.Game.Controls;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Game.QuickTesting
{
	/// <summary>
	/// The script allows you to press something in order to do something else.
	/// It's supposed to be highly changable, used when you need some quick test.
	/// For example suppose you have just created your awesome script but you need to call
	/// a method to start your script: you can call it here!
	/// </summary>
	/// <remarks>
	/// To clarify, I created this script because I needed to test my "jump script". I needed someone to actually
	/// call the method JumpScript.doJump. And that's why I've created it!
	/// </remarks>
	public class SimpleInteractor : MonoBehaviour
	{
		// Use this for initialization
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{
			if (Input.GetButtonDown("Fire1"))
			{
				//do something here!
				//Debug.Log("Started executing SimpleInteractor actions!!!");
				//this.gameObject.GetComponent<JumpScript>().doJump(5);
			}
		}
	}
}
