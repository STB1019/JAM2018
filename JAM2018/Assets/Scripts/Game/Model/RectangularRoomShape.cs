using System;

namespace Scripts.Game.Model
{
	/// <summary>
	/// A specification of the IRoomShape interface.
	/// It contains every needed data to describe a rectangular-based room.
	/// </summary>
	public class RectangularRoomShape : IRoomShape
	{
		public Triple<float, float, float> BoxDimension { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Scripts.Game.Model.RectangularRoomShape"/> class.
		/// </summary>
		/// <param name="xSide">Length of X side.</param>
		/// <param name="ySide">Length of Y side.</param>
		/// <param name="zSide">Length of Z side.</param>
		public RectangularRoomShape(float xSide, float ySide, float zSide)
		{
			this.BoxDimension.X = xSide;
			this.BoxDimension.Y = ySide;
			this.BoxDimension.Z = zSide;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Scripts.Game.Model.RectangularRoomShape"/> class.
		/// </summary>
		/// <param name="dimensions">Triple representing the length of the three sides.</param>
		public RectangularRoomShape(Triple<float, float, float> dimensions)
		{
			this.BoxDimension.X = dimensions.X;
			this.BoxDimension.Y = dimensions.Y;
			this.BoxDimension.Z = dimensions.Z;
		}
	}
}



