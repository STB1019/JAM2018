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
		GameObject [][] WallMatrix { get; set; }

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
		GameObject [][] FloorMatrix { get; set; }

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
		GameObject [][] CeilingMatrix { get; set; }

		/// <summary>
		/// Gets or sets the coordinates system.
		/// See RoomCoordinatesSystem enumeration for further informations.
		/// </summary>
		/// <value>The coordinates system.</value>
		public RoomCoordinatesSystem CoordinatesSystem { get; set; }

		//
		private Vector3 origin;

		/// <summary>
		/// Initializes a new instance of the <see cref="Scripts.Game.RoomFactory.Visualizers.RectangularRoomVisualizer"/> class.
		/// </summary>
		public RectangularRoomVisualizer ()
		{
			
		}
	}
}

