using System;
using System.Collections.Generic;

namespace Scripts.Game.Model
{
	public static class QualitasTypes
	{
		public static IList<IQualitasType> ALL { get; private set; }

		static QualitasTypes() {
			ALL = new List<IPropType> ();
		}

		public void Add(string name) {
			ALL.Add (new DefaultQualitasType (name));
		}

		public bool Contains(IQualitasType type) {
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

