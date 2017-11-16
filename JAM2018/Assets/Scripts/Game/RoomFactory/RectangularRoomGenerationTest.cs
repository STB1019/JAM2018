﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Game.RoomFactory.Factories;
using Scripts.Game.RoomFactory.Visualizers;

namespace Scripts.Game.RoomFactory
{
	public class RectangularRoomGenerationTest : MonoBehaviour {

		/// <summary>
		/// How this small test has been conceived:
		/// 
		/// This script is attached to an empty GameObject (GO), which has also the "RoomGenerator" script.
		/// At starting, this GO will call "GenerateRectangularRoom" method on its proper script-component, instantiating an empty GO
		/// which will be the parent of all the tiles.
		/// Then, the RectangularRoomGenerator will begin to instantiate all the tiles according to the chosen system of coordinates
		/// and to the room dimensions. The instantiation is nothing else than the "cloning" of some prefab models that already have 
		/// their "MeshRenderer" component, allowing us to actually see the room into the scene.
		/// 
		/// </summary>
		/// 
		/// <author>Michele Dusi</author>
		void Start () {
			AbstractRoomFactory factory = new RectangularRoomFactory ();
			GameObject myRoom = factory.makeRoom ();
		}
	}
}
