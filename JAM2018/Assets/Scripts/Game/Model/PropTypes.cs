using System;
using System.Collections.Generic;

namespace Scripts.Game.Model
{

	/// <summary>
	/// Represents a static class containing all the props you can hacve in the maze
	/// </summary>
	public class PropTypes : AbstractTypes<IPropType>
	{
		public static readonly PropTypes INSTANCE = new PropTypes();

		internal PropTypes() : base() {
		}

		protected override IPropType BuildType(string key) {
			return new DefaultPropType (key);
		}

	}
}

