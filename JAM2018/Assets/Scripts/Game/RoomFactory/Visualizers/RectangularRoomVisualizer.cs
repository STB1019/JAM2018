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
		GameObject [][] wallMatrix;

		public RectangularRoomVisualizer ()
		{
			
		}
	}
}

