using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AssemblyCSharp;

public class RectangularRoomGenerationTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject.Find ("RoomSeed").GetComponent<RectangularRoomGenerator>()
			.GenerateRectangularRoomIn(this.gameObject, 5, 2, 6, RoomCoordinatesSystem.BaseCentered);
	}
}
