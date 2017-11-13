using System;
using UnityEngine;
using Scripts.Game.Model;
using Scripts.Game.RoomFactory.Visualizers;

namespace Scripts.Game.RoomFactory
{
	/// <summary>
	/// MonoBehaviour script representing a single room.
	/// Every RoomScript has two properties:
	/// <list type="bullet">
	///		<item>
	/// 		<term>ConcreteRoom</term>
	///	 		<description>The concrete class used to describe every parameter and behaviour of the room.</description>
	/// 	</item>
	///		<item>
	/// 		<term>ConcreteRoomVisualizer</term>
	///	 		<description>The container for the graphic objects into the scene that belong to that room.</description>
	/// 	</item>
	/// </list>
	/// </summary>
	public class RoomScript : MonoBehaviour
	{
		IRoom ConcreteRoom { get; set; }
		IRoomVisualizer ConcreteRoomVisualizer { get; set; }

		public RoomScript ()
		{
		}
	}
}

