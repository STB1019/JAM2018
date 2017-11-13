using System;

namespace Scripts.Game.Model
{
	/// <summary>
	/// The only (for now) concrete class which represents a room into the maze.
	/// </summary>
	public class DefaultRoom : IRoom
	{
		IRoomShape IRoom.Shape { get; set; }

		public DefaultRoom ()
		{
		}
	}
}

