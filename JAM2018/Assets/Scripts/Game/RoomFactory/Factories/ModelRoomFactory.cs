using System;
using Scripts.Game.Model;
using Scripts.Game.RoomFactory.Visualizers;

namespace Scripts.Game.RoomFactory.Factories
{
	/// <summary>
	/// Factory class for the IRoom interface.
	/// It supplies a method to get a DefaultRoom instance.
	/// It follows the singleton pattern.
	/// </summary>
	public class ModelRoomFactory
	{
		/// <summary>
		/// The singleton instance of the factory.
		/// </summary>
		private static ModelRoomFactory singletonFactory = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="Scripts.Game.RoomFactory.Factories.ModelRoomFactory"/> class.
		/// </summary>
		private ModelRoomFactory () {}

		/// <summary>
		/// Gets the singleton factory.
		/// </summary>
		/// <returns>The only ModelRoomFactory in the program.</returns>
		public static ModelRoomFactory getFactory ()
		{
			if (ModelRoomFactory.singletonFactory == null) {
				ModelRoomFactory.singletonFactory = new ModelRoomFactory ();
			}
			return ModelRoomFactory.singletonFactory;
		}

		/// <summary>
		/// The coordinates system.
		/// It's the same for all the project rooms, until it's changed.
		/// </summary>
		protected RoomCoordinatesSystem coordinatesSystem = RoomCoordinatesSystem.BaseCentered;

		/// <summary>
		/// Factory method which returns an IRoom object created with the informations passed as parameters.
		/// </summary>
		/// <returns>The room.</returns>
		/// <param name="roomShape">
		/// 	The IRoomShape object of the room.
		/// 	It will be saved as reference into the DefaultRoom instance.
		/// </param>
		internal IRoom makeRoom (IRoomShape roomShape) {
			return new DefaultRoom (roomShape);
		}
	}
}

