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
		IRoomShape Shape { get; set; }
	}
}

