using System;

namespace Scripts.Game.Model
{
	/// <summary>
	/// A specification of the IRoomShape interface.
	/// It contains every needed data to describe a cylindrical room.
	/// </summary>
	public class CylindricalRoomShape : IRoomShape
	{
		float [] IRoomShape.BoxDimension { get; set; }

		public CylindricalRoomShape()
		{
			/*
			 * // BASE (elliptic?)
			 * this.a_radius = boxdimension.x / 2;
			 * this.b_radius = boxdimension.z / 2;
			 * // HEIGHT
			 * this.height = boxdimension.y;
			 */
		}
	}
}

