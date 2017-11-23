using System;
using Scripts.Game.Model;

namespace Scripts.Game.RoomFactory.Factories
{
	/// <summary>
	/// Creates an IRoom object with CylindricalRoomShape as IRoomShape.
	/// </summary>
	public class CylindricalRoomFactory
	{
		/// <summary>
		/// The singleton instance of the factory.
		/// </summary>
		private static CylindricalRoomFactory singletonFactory = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="Scripts.Game.RoomFactory.Factories.CylindricalRoomFactory"/> class.
		/// </summary>
		private CylindricalRoomFactory () {}

		/// <summary>
		/// Gets the singleton factory.
		/// </summary>
		/// <returns>The only CylindricalRoomFactory in the program.</returns>
		public static CylindricalRoomFactory getFactory ()
		{
			if (CylindricalRoomFactory.singletonFactory == null) {
				CylindricalRoomFactory.singletonFactory = new CylindricalRoomFactory ();
			}
			return CylindricalRoomFactory.singletonFactory;
		}
	}
}

