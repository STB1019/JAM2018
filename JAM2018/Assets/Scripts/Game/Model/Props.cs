using System;
using System.Collections.Generic;

namespace Scripts.Game.Model
{

	/// <summary>
	/// Represents a static class containing all the props you can hacve in the maze
	/// </summary>
	public static class Props
	{
		public static readonly IList<IPropType> ALL = new List<IPropType>{
			new DefaultPropType ("Fountain"),
			new DefaultPropType ("Chest")
		};
	}
}

