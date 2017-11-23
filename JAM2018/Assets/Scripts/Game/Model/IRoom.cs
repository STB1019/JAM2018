using System;

namespace Scripts.Game.Model
{
	/// <summary>
	/// General interface for a room object.
	/// For now, it contains an "IRoomShape" object to describe the shape of the room.
	/// In the future, it can contains different values to specify room's attribuites (bioms?).
	/// </summary>
	public interface IRoom
	{
		/// <summary>
		/// Gets or sets the shape of the room, as an IShapeRoom instance.
		/// </summary>
		/// <value>The shape.</value>
		IRoomShape Shape { get; set; }		// Note: it is public by default
	}
}

