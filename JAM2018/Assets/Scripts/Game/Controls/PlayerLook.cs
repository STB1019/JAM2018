using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Game.Controls
{
	///<summary>
	///This method allows the player to look around using a mouse. It gathers mouse input on the X and Y axis and then
	///rotates the camera according to the X value and rotates the player according to the Y value.
	///It also clamps the camera so player camera movement is limited and doesn't behave strangely
	///</summary>
	public class PlayerLook : MonoBehaviour
	{
		public Transform playerBody;
		public float mouseSensitivity;

		private float xAxisClamp = 0.0f;
		private float mouseX;
		private float mouseY;
		private float rotAmountY;
		private float rotAmountX;

		void Update()
		{
			RotateCamera();       
		}

		void RotateCamera()
		{
			mouseX = Input.GetAxis("Mouse X");
			mouseY = Input.GetAxis("Mouse Y");

			rotAmountX = mouseX * mouseSensitivity;
			rotAmountY = mouseY * mouseSensitivity;

			xAxisClamp -= rotAmountY;

			Vector3 targetRotCam = transform.rotation.eulerAngles;
			Vector3 targetRotBody = playerBody.rotation.eulerAngles;

			targetRotCam.x -= rotAmountY;
			targetRotCam.z = 0;
			targetRotBody.y += rotAmountX;

			if(xAxisClamp > 89)
			{
				xAxisClamp = 89;
				targetRotCam.x = 89;
			}
			else if(xAxisClamp < -89)
			{
				xAxisClamp = -89;
				targetRotCam.x = 269;
			}
			
			transform.rotation = Quaternion.Euler(targetRotCam);
			playerBody.rotation = Quaternion.Euler(targetRotBody);
		}
	}
}
