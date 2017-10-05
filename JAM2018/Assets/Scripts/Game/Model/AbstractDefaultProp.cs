using System;
using System.Collections.Generic;

namespace Scripts.Game.Model
{
	public abstract class AbstractDefaultProp : IProp
	{

		#region FIELDS
		public IPropType Class { get; private set;}
		public IList<IQualitas> Qualitates { get; set; }
		public int Id {get; private set;}
		/// <summary>
		/// The next identifier for the field ID
		/// </summary>
		private static int NextId {
			get;
			set;
		}
		#endregion

		#region CONSTRUCTORS
		protected AbstractDefaultProp(IPropType type) {
			this.Id = this.getNextAssignableIdAndIncrement();
			this.Class = type;
			this.Qualitates = new List<IQualitas> ();
		}

		protected AbstractDefaultProp(Type t) : this(PropTypes.INSTANCE[t]) {
		}

		#endregion

		#region PRIVATE METHODS
		/// <summary>
		/// Gets the next assignable identifier.
		/// </summary>
		/// <returns>The next assignable identifier.</returns>
		private int getNextAssignableIdAndIncrement() {
			var retVal = NextId;
			NextId += 1;
			return retVal;
		}
		#endregion
	}
}

