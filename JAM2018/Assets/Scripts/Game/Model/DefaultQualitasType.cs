using System;

namespace Scripts.Game.Model
{
	/// <summary>
	/// A naive implementation of <see cref="IQualitas"/>
	/// </summary>
	public class DefaultQualitasType : IQualitasType
	{

		public string Name { get; private set; }

		public DefaultQualitasType (string name)
		{
			this.Name = Name;
		}

		/// <summary>
		/// Serves as a hash function for a <see cref="IQualitas"/> object.
		/// </summary>
		/// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
		public override int GetHashCode() {
			return this.Name.GetHashCode ();
		}
	}
}

