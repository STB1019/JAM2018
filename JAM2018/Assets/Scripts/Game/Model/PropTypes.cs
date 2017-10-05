using System;
using System.Collections.Generic;

namespace Scripts.Game.Model
{

	/// <summary>
	/// Represents a static class containing all the props you can hacve in the maze
	/// </summary>
	public class PropTypes
	{
		public static IList<IPropType> ALL { get; private set; }

		static PropTypes() {
			ALL = new List<IPropType> ();
		}

		public void Add(string name) {
			ALL.Add (new DefaultPropType (name));
		}

		public void Add(Type t) {
			Add (t.Name);
		}

		public bool Contains(IPropType type) {
			foreach (var t in ALL) {
				if (t.Equals(type)) {
					return true;
				}
			}
		}

		public bool Contains(string type) {
			foreach (var t in ALL) {
				if (t.Name.Equals(type)) {
					return true;
				}
			}
		}

	}
}

