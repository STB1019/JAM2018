using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Scripts.Game.RoomFactory.Visualizers;
using Scripts.Game.Model;

namespace Scripts.Game.RoomFactory.Factories
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
	public class RectangularRoomFactory {

		/// <summary>
		/// The singleton instance of the factory.
		/// </summary>
		private static RectangularRoomFactory singletonFactory = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="Scripts.Game.RoomFactory.Factories.RectangularRoomFactory"/> class.
		/// </summary>
		private RectangularRoomFactory () {}

		/// <summary>
		/// Gets the singleton factory.
		/// </summary>
		/// <returns>The only RectangularRoomFactory in the program.</returns>
		public static RectangularRoomFactory getFactory ()
		{
			if (RectangularRoomFactory.singletonFactory == null) {
				RectangularRoomFactory.singletonFactory = new RectangularRoomFactory ();
			}
			return RectangularRoomFactory.singletonFactory;
		}

		/// <summary>
		/// Room tiles models.
		/// </summary>
		public static Transform tileFloor = 			(Transform) AssetDatabase.LoadAssetAtPath ("Assets/Prefabs/RoomTiles/tileFloor", 			typeof (Transform));
		public static Transform tileCeiling = 			(Transform) AssetDatabase.LoadAssetAtPath ("Assets/Prefabs/RoomTiles/tileCeiling", 			typeof (Transform));
		public static Transform tileTopWall = 			(Transform) AssetDatabase.LoadAssetAtPath ("Assets/Prefabs/RoomTiles/tileTopWall", 			typeof (Transform));
		public static Transform tileMiddleWall = 		(Transform) AssetDatabase.LoadAssetAtPath ("Assets/Prefabs/RoomTiles/tileMiddleWall",	 	typeof (Transform));
		public static Transform tileBottomWallPlain = 	(Transform) AssetDatabase.LoadAssetAtPath ("Assets/Prefabs/RoomTiles/tileBottomWallPlain", 	typeof (Transform));
		public static Transform tileBottomWallDoor = 	(Transform) AssetDatabase.LoadAssetAtPath ("Assets/Prefabs/RoomTiles/tileBottomWallDoor", 	typeof (Transform));

		public RoomScript makeRoom (Triple<float, float, float> dimensions, Vector3 position) 
		{
			RoomScript roomScript = new RoomScript ();

			// Creating room model
			ModelRoomFactory modelRoomFactory = ModelRoomFactory.getFactory ();
			IRoomShape roomShape = new RectangularRoomShape (dimensions);
			roomScript.ConcreteRoom = modelRoomFactory.makeRoom (roomShape);

			// Creating room visualizer
			roomScript.ConcreteRoomVisualizer = this.makeRectangularRoomVisualizer (position, roomShape);
		}

		/// <summary>
		/// Instantiates the container object and creates the room tiles.
		/// </summary>
		/// <returns>The reference to the empty Visualizer.</returns>
		/// <param name="position">Position.</param>
		/// <param name="roomSideX">Room side x.</param>
		/// <param name="roomSideY">Room side y.</param>
		/// <param name="roomSideZ">Room side z.</param>
		private RectangularRoomVisualizer makeRectangularRoomVisualizer (Vector3 position, IRoomShape roomShape) {
			// Instantiating the container for all the tiles
			RectangularRoomVisualizer visualizer = new RectangularRoomVisualizer ();

			// Room tiled-dimensions
			int roomSideX = (int) roomShape.BoxDimension.X;
			int roomSideY = (int) roomShape.BoxDimension.Y;
			int roomSideZ = (int) roomShape.BoxDimension.Z;

			// First, the side length of a tile is calculated. 
			float tileSize = tileFloor.GetComponent<Renderer> ().bounds.size.x;

			// Computing position based on RoomCoordinatesSystem
			Vector3 originPosition = position;
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
					Transform floor = Object.Instantiate (
						tileFloor, 
						new Vector3 (x, 0, z) * tileSize + originPosition, 
						Quaternion.Euler (-90, 0, 0)) as Transform;
					floor.SetParent (parent.transform);

					// Creating Ceiling tiles
					Transform ceiling = Object.Instantiate (
						tileCeiling, 
						new Vector3 (x, roomSideY, z) * tileSize + originPosition, 
						Quaternion.Euler (-90, 0, 0)) as Transform;
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
					Transform middle_wall_A = Object.Instantiate (
						current_wall,
						new Vector3 (0, y, z) * tileSize + originPosition, 
						Quaternion.Euler (-90, 90, 0));
					middle_wall_A.SetParent (parent.transform);

					// Creating (x = roomSideX - 1) tiles
					Transform middle_wall_B = Object.Instantiate (
						current_wall,
						new Vector3 (roomSideX - 1, y, z) * tileSize + originPosition, 
						Quaternion.Euler (-90, -90, 0));
					middle_wall_B.SetParent (parent.transform);
				}

				// Creating Z-facing walls.
				for (int x = 0; x < roomSideX; x++)
				{
					// Creating (z = 0) tiles
					Transform middle_wall_A = Object.Instantiate (
						current_wall,
						new Vector3 (x, y, 0) * tileSize + originPosition, 
						Quaternion.Euler (-90, 0, 0));
					middle_wall_A.SetParent (parent.transform);

					// Creating (z = roomSideZ - 1) tiles
					Transform middle_wall_B = Object.Instantiate (
						current_wall,
						new Vector3 (x, y, roomSideZ - 1) * tileSize + originPosition, 
						Quaternion.Euler (-90, 180, 0));
					middle_wall_B.SetParent (parent.transform);
				}
			}

			// Returning parent reference
			return visualizer;
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