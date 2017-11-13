using System;

namespace Scripts.Game.Model
{
	/// <summary>
	/// A Room Shape is an object that indicates what shape a room has, and memorizes
	/// all the variables needed to describe exhaustively that particular shape.
	/// </summary>
	public interface IRoomShape
	{
	/*
		/// <summary>
		/// Gets or sets the three box dimensions.
		/// The box is the minimum parallelepiped (whit parallel faces to the axis)
		/// which contains all the room.
		/// It can be used to easily check collisions and intersections with adjacent rooms.
		/// </summary>
		/// <value>The three box dimensions.</value>
		Tuple<float, float, float> BoxDimension { get; set; }
		// TODO fix Tuple's problem.
	*/

		float [] BoxDimension { get; set; }
		
	}
}

