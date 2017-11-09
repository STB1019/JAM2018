using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharpUtilities;
using System;
using Assets.Scripts.Game.Commons;

namespace Scripts.Game.Controls {

	/// <summary>
	/// If you want to listen to when a <see cref="JumpScript.doJump(float)"/> starts a jump
	/// you need to create a delegate with the following signature
	/// </summary>
	/// <param name="go">the gameobject containing the <see cref="JumpScript"/> which requested the jump</param>
	/// <param name="jumpForce">the force of this jump</param>
	public delegate void JumpHandler(GameObject go, float jumpForce);


	public enum GroundCollisionDetectionStrategy
	{
		BYTAG,
		BYNAME
	}

	/// <summary>
	/// This scripts allows you to invoke methods useful to let something jump.
	/// The script can be used by both player and NPC.
	/// </summary>
	/// <remarks>
	/// If you wish that something can jump, attach this script to the <b>desired gameobject</b>, then
	/// call <see cref="doJump(float)"/> method.
	/// 
	/// A jump is an <b>action</b> took by someone that can be applied only when such someone 
	/// is on the ground (namely colliding with the ground). The jump will propel the object in the <b>opposite direction</b>
	/// the gravity is going. The jump stops when the object return colliding to the ground.
	/// </remarks>
	[RequireComponent(typeof(Rigidbody))]
	public partial class JumpScript : MonoBehaviour {

		/// <summary>
		/// true if someone has requested to make a jump
		/// </summary>
		private bool HasJumpBeenRequested { get; set; }
		/// <summary>
		/// true if the object can request a jump.
		/// If false it can mean several things: it's flying, it's falling or it's jumping;
		/// however, we may never know which is the correct answer!
		/// If true, however, it's garantueed you can request a jump.
		/// </summary>
		public bool CanRequestJump { get; private set; }
		/// <summary>
		/// Property used to pass around the requested force of the jump
		/// </summary>
		private float JumpForce { get; set; }
		/// <summary>
		/// Property representing the fact that the attached game object is colliding with the terrain
		/// </summary>
		private bool IsCollidingWithTerrain { get; set; }
		/// <summary>
		/// If the velocity y is less (in absolute terms) of this threshold we can
		/// apply the jump
		/// </summary>
		[HideInInspector]
		public float VelocityYThreshHold = 0.5f;

		/// <summary>
		/// Listeners to alert when someone requests a jump
		/// </summary>
		[HideInInspector]
		public SortedEventList<JumpHandler> OnJumpStarted = new SortedEventList<JumpHandler>();
		/// <summary>
		/// Listeners to alert when someone concludes its jump
		/// </summary>
		[HideInInspector]
		public SortedEventList<JumpHandler> OnJumpStopped = new SortedEventList<JumpHandler>();

		/// <summary>
		/// The <see cref="Rigidbody"/> attached to the gameobject owning this script
		/// </summary>
		private Rigidbody Rigidbody { get
			{
				return this.GetComponent<Rigidbody>();		
			}
		}

		public void Awake()
		{
			this.IsCollidingWithTerrain = false;
			this.JumpForce = 0;
		}

		/// <summary>
		/// Requests a jump to be executed
		/// </summary>
		/// <param name="jumpForce">how much force you want to put on this jump</param>
		public void doJump(float jumpForce)
		{
			if (this.CanRequestJump)
			{
				this.IsCollidingWithTerrain = false;
				this.HasJumpBeenRequested = true;
				this.JumpForce = jumpForce;
				this.OnJumpStarted.FireEvents(this.gameObject, jumpForce);
			}
		}

		public void FixedUpdate()
		{
			//we can jump only if the y velocity is 0. see http://answers.unity3d.com/comments/736765/view.html
			this.CanRequestJump = this.computeRequestJump();
			if (this.HasJumpBeenRequested) {
				this.Rigidbody.AddForce(this.JumpForce * Vector3.up, ForceMode.Impulse);
				this.HasJumpBeenRequested = false;
			}
		}

		private bool computeRequestJump()
		{
			if (Math.Abs(this.Rigidbody.velocity.y) >= VelocityYThreshHold)
			{
				return false;
			}

			if (!IsCollidingWithTerrain)
			{
				return false;
			}

			return true;
		}

		void OnCollisionEnter(Collision col)
		{
			this.IsCollidingWithTerrain = (col.gameObject.ta == Tags.TERRAIN);
		}
	}


}