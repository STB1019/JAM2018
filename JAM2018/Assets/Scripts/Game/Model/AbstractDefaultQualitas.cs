using System;

namespace Scripts.Game.Model
{
	public class AbstractDefaultQualitas : IQualitas
	{
		public IQualitasType Class { get; private set;}
		public IProp Prop {get; set;}
		public int Id { get; private set;}

		/// <summary>
		/// The next identifier for the field ID
		/// </summary>
		private static int NextId {
			get;
			set;
		}

		public AbstractDefaultQualitas (IQualitasType type, IProp attachedProp)
		{
			this.Class = type;
			this.Prop = attachedProp;
			this.Id = this.getNextAssignableIdAndIncrement();
		}

		public AbstractDefaultQualitas (IQualitasType type) : this(type, null) {
		}

		/// <summary>
		/// Gets the next assignable identifier.
		/// </summary>
		/// <returns>The next assignable identifier.</returns>
		private int getNextAssignableIdAndIncrement() {
			var retVal = NextId;
			NextId += 1;
			return retVal;
		}
			

	}
}

