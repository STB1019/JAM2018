using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AssemblyCSharp
{
	/// <summary>
	/// This script creates the tiles that make the room visible, at the starting of the program.
	/// The room will be visualized in the position set by the first argument.
	///
	/// The room is visualized by instancing different pieces (called "tiles") in the scene.
	/// First, all the horizontal tiles are created, i.e. the floor and the ceiling.
	/// Then, it creates the vertical walls traversing the room from the ground floor to the last one.
	/// Pre-conditions: integer side inputs have to be strictly greater than 0. Otherwise,
	/// some walls will not be visualized. No controls are made on side inputs, since it's supposed 
	/// an external algorithm will set them properly.
	/// 
	/// </summary>
	/// <author>Michele Dusi</author>
	public class RectangularRoomGenerator : MonoBehaviour {

		// room model tiles
		public Transform tileFloor;
		public Transform tileCeiling;
		public Transform tileTopWall;
		public Transform tileMiddleWall;
		public Transform tileBottomWallPlain;
		public Transform tileBottomWallDoor;

		/// <summary>
		/// Instantiates the Empty Parent Object and creates the room tiles.
		/// </summary>
		/// <returns>The reference to the empty parent object.</returns>
		/// <param name="position">Position.</param>
		/// <param name="roomSideX">Room side x.</param>
		/// <param name="roomSideY">Room side y.</param>
		/// <param name="roomSideZ">Room side z.</param>
		public GameObject GenerateRectangularRoom (Vector3 position, int roomSideX, int roomSideY, int roomSideZ, RoomCoordinatesSystem coordinatesSystem) {
			// Instantiating empty parent object
			GameObject emptyParent = new GameObject ();
			// Setting its position as the defined position
			emptyParent.transform.position = position;
			// Creating room tiles
			GenerateRectangularRoomIn (emptyParent, roomSideX, roomSideY, roomSideZ, coordinatesSystem);
			// Returning parent reference
			return emptyParent;
		}

		/// <summary>
		/// Generates the room tiles as children of the GameObject passed as parameter.
		/// </summary>
		/// <param name="parent">Parent.</param>
		/// <param name="roomSideX">Room side x.</param>
		/// <param name="roomSideY">Room side y.</param>
		/// <param name="roomSideZ">Room side z.</param>
		public void GenerateRectangularRoomIn (GameObject parent, int roomSideX, int roomSideY, int roomSideZ, RoomCoordinatesSystem coordinatesSystem) {

			// First, the side length of a tile is calculated. 
			float tileSize = tileFloor.GetComponent<Renderer> ().bounds.size.x;

			// Computing position based on RoomCoordinatesSystem
			Vector3 originPosition = parent.transform.position;

			switch (coordinatesSystem) {
			case RoomCoordinatesSystem.VertexCentered:
				// No translations
				break;
			case RoomCoordinatesSystem.BaseCentered :
				originPosition += - new Vector3 (roomSideX / 2.0f, 0, roomSideZ / 2.0f) * tileSize;
				break;
			case RoomCoordinatesSystem.ShapeCentered :
				originPosition += - new Vector3 (roomSideX / 2.0f, roomSideY / 2.0f, roomSideZ / 2.0f) * tileSize;
				break;
			default:
				throw new SwitchCaseUnhandledException ("Unhandled case in RoomCoordinatesSystem switch.");
			}
			originPosition += new Vector3 (tileSize / 2, 0, tileSize / 2);

			// Creating "horizontal" tiles
			for (int x = 0; x < roomSideX; x++)
			{
				for (int z = 0; z < roomSideZ; z++)
				{
					// Creating floor tiles
					Transform floor = Instantiate (
						tileFloor, 
						new Vector3 (x, 0, z) * tileSize + originPosition, 
						Quaternion.Euler (-90, 0, 0));
					floor.SetParent (parent.transform);

					// Creating Ceiling tiles
					Transform ceiling = Instantiate (
						tileCeiling, 
						new Vector3 (x, roomSideY, z) * tileSize + originPosition, 
						Quaternion.Euler (-90, 0, 0));
					ceiling.SetParent (parent.transform);

					// TODO: Correct the problem with rotation (even if models were remade from zero, the problem is still there)
				}
			}

			// Creating vertical walls
			for (int y = 0; y <= roomSideY; y++)
			{
				// Selecting the vertical wall based on the "floor"
				// Exmp: Ground floor 	-> BottomWall
				// Exmp: Last floor 	-> TopWall
				Transform current_wall;
				if (y == 0) {
					//current_wall = (Random.value < 0.4) ? tileBottomWallDoor : tileBottomWallPlain;
					current_wall = tileBottomWallPlain;
				} else if (y == roomSideY) {
					current_wall = tileTopWall;
				} else {
					current_wall = tileMiddleWall;
				}

				// Creating X-facing walls
				for (int z = 0; z < roomSideZ; z++)
				{
					// Creating (x = 0) tiles
					Transform middle_wall_A = Instantiate (
						current_wall,
						new Vector3 (0, y, z) * tileSize + originPosition, 
						Quaternion.Euler (-90, -90, 0));
					middle_wall_A.SetParent (parent.transform);

					// Creating (x = roomSideX - 1) tiles
					Transform middle_wall_B = Instantiate (
						current_wall,
						new Vector3 (roomSideX - 1, y, z) * tileSize + originPosition, 
						Quaternion.Euler (-90, 90, 0));
					middle_wall_B.SetParent (parent.transform);
				}

				// Creating Z-facing walls.
				for (int x = 0; x < roomSideX; x++)
				{
					// Creating (z = 0) tiles
					Transform middle_wall_A = Instantiate (
						current_wall,
						new Vector3 (x, y, 0) * tileSize + originPosition, 
						Quaternion.Euler (-90, 180, 0));
					middle_wall_A.SetParent (parent.transform);

					// Creating (z = roomSideZ - 1) tiles
					Transform middle_wall_B = Instantiate (
						current_wall,
						new Vector3 (x, y, roomSideZ - 1) * tileSize + originPosition, 
						Quaternion.Euler (-90, 0, 0));
					middle_wall_B.SetParent (parent.transform);
				}
			}
		}

	}

	/*
	 * Notes (TODO LIST):
	 * - Decide where to put the door:
	 * 		- Create DoorPlacer issue
	 * 		- Create the door positioning system (CardinalDoorPlacement)
	 * 		- Establish the relation between visual units and logic business units
	 * - Map texture over the tiles.
	 * - Abstract parent class "AbstractRoomGenerator".
	 */
}