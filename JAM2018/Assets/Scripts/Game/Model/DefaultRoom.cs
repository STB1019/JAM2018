using System;

namespace Scripts.Game.Model
{
	/// <summary>
	/// The only (for now) concrete class which represents a room into the maze.
	/// </summary>
	public class DefaultRoom : IRoom
	{
		/// <summary>
		/// Gets or sets the shape of the room, as an IShapeRoom instance.
		/// It implements the interface "IRoom"'s property.
		/// </summary>
		/// <value>The shape.</value>
		public IRoomShape Shape { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Scripts.Game.Model.DefaultRoom"/> class.
		/// </summary>
		public DefaultRoom ()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Scripts.Game.Model.DefaultRoom"/> class.
		/// </summary>
		/// <param name="roomShape">An IRoomShape object defining the shape of the room.</param>
		public DefaultRoom (IRoomShape roomShape)
		{
			this.Shape = roomShape;
		}
	}
}

