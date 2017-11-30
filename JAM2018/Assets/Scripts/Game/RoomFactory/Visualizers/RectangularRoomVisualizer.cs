using System;
using UnityEngine;

namespace Scripts.Game.RoomFactory.Visualizers
{
	/// <summary>
	/// Concrete class that contains the GameObjects in the scene belonging to a specific RectangularRoom.
	/// </summary>
	public class RectangularRoomVisualizer : IRoomVisualizer
	{
		/// <summary>
		/// The whole matrix that represents all the walls of the rectangular room.
		/// The first index runs across the perimeter beginning from point (0,0) and going in clockwise direction
		/// (i.e. if you're standing in the center of the room, from left to right).
		/// 
		///     Second_index
		/// 	^    _______________________________________________
		/// 	^	|			 	 |		|			 	 |		|
		/// 	^	|	   ROOM		 |		|	   ROOM		 |		|
		/// 		|	   WALLS	 |		|	   WALLS	 |		|
		/// 		|________________|______|________________|______|		[Side view]
		/// 					>> First_index
		/// 
		/// 				>>
		/// 	(0,0)________________
		/// 		|				 |
		/// 	^	|	   ROOM		 | v
		/// 	^	|	   BASE		 | v
		/// 		|________________|		[Top view]
		/// 
		/// 			    <<
		/// 
		/// </summary>
		/// <value>The wall matrix</value>
		public GameObject [,] WallMatrix { get; internal protected set; }

		/// <summary>
		/// Gets or sets the floor matrix.
		/// That is a matrix representing and containing all the tiles belonging to the floor.
		/// The used coordinates system reflects the VertexCentered one.
		/// 
		/// 			> > Second_index
		/// 	(0,0)_________________
		/// 		|			 	  |
		/// 		|	   ROOM		  |
		/// 	v	|	   FLOOR	  |
		/// 	v	|_________________|		[Top view]
		/// 	First_index 
		/// 
		/// </summary>
		/// <value>The floor matrix.</value>
		public GameObject [,] FloorMatrix { get; internal protected set; }

		/// <summary>
		/// Gets or sets the ceiling matrix.
		/// That is a matrix representing and containing all the tiles belonging to the ceiling.
		/// The used coordinates system reflects the VertexCentered one.
		/// 
		/// 			> > Second_index
		/// 	(0,0)_________________
		/// 		|				  |
		/// 		|	  ROOM		  |
		/// 	v	|	  CEILING 	  |
		/// 	v	|_________________|		[Top view]
		/// 	First_index 
		/// 
		/// </summary>
		/// <value>The ceiling matrix.</value>
		public GameObject [,] CeilingMatrix { get; internal protected set; }

		/// <summary>
		/// Gets or sets the coordinates system.
		/// See RoomCoordinatesSystem enumeration for further informations.
		/// </summary>
		/// <value>The coordinates system.</value>
		public RoomCoordinatesSystem CoordinatesSystem { get; internal protected set; }

		/// <summary>
		/// The origin of the coordinates system.
		/// </summary>
		/*
		private Vector3 origin;
		*/

		/// <summary>
		/// Initializes a new instance of the <see cref="Scripts.Game.RoomFactory.Visualizers.RectangularRoomVisualizer"/> class.
		/// </summary>
		public RectangularRoomVisualizer (int roomSideX, int roomSideY, int roomSideZ)
		{
			this.CeilingMatrix = new GameObject [roomSideX, roomSideZ];
			this.FloorMatrix = new GameObject [roomSideX, roomSideZ];
			this.WallMatrix = new GameObject [2 * (roomSideX + roomSideZ), roomSideY];
		}
	}
}

