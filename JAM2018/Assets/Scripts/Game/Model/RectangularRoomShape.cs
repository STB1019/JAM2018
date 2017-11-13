using System;

namespace Scripts.Game.Model
{
	/// <summary>
	/// A specification of the IRoomShape interface.
	/// It contains every needed data to describe a rectangular-based room.
	/// </summary>
	public class RectangularRoomShape : IRoomShape
	{
		float [] IRoomShape.BoxDimension { get; set; }

		public RectangularRoomShape()
		{
			/*
			 * this.x = boxdimension.x
			 * this.y = boxdimension.y
			 * this.z = boxdimension.z
			 */
		}
	}
}



