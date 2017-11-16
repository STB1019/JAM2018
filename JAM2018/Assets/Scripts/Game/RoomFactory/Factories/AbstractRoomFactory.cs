using System;
using Scripts.Game.Model;
using Scripts.Game.RoomFactory.Visualizers;

namespace Scripts.Game.RoomFactory.Factories
{
	/// <summary>
	/// Implements the Abstract Factory class into the "Abstract Factory" design pattern.
	/// It supplies two methods (for now) to create a room based on its shape.
	/// </summary>
	public class AbstractRoomFactory
	{
		/// <summary>
		/// The default room used as "model" in the proces of instantiation.
		/// </summary>
		protected IRoom defaultRoom;

		/// <summary>
		/// The coordinates system.
		/// It's the same for all the project rooms, until it's changed.
		/// </summary>
		protected RoomCoordinatesSystem coordinatesSystem = RoomCoordinatesSystem.BaseCentered;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="_defaultRoom">Default room.</param>
		protected AbstractRoomFactory (IRoom _defaultRoom)
		{
			this.defaultRoom = _defaultRoom;
		}

		/// <summary>
		/// Factory method which returns a RoomScript object created with the informations in the default room.
		/// It can be specified in different classes basing on the room shape.
		/// </summary>
		/// <returns>The room.</returns>
		public abstract RoomScript makeRoom() {}
	}
}

