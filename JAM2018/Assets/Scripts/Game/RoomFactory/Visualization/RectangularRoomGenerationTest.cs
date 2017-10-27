using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AssemblyCSharp;

public class RectangularRoomGenerationTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.GetComponent<RectangularRoomGenerator>()
			.GenerateRectangularRoom(new Vector3(5, 5, -10), 7, 5, 8, RoomCoordinatesSystem.BaseCentered);
	}
}
