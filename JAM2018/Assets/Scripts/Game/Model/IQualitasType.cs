using System;

namespace Scripts.Game.Model
{

	/// <summary>
	/// A qualitas is something which adds behaviour to a prop.
	/// Not all qualitas can be added to every prop: in order to be attached, the prop needs to have a certain minimal interface; such
	/// interface is used by the implementation of the qualitas to concretely implement the expected behaviour.
	/// A class implementing this interface will be able to become a possible qualitas a prop might have. For example it may be "DoSpawn" or "DoClone"
	/// </summary>
	public interface IQualitasType
	{
		string Name { get; set; }

		/// <summary>
		/// Serves as a hash function for a <see cref="IQualitas"/> object.
		/// </summary>
		/// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
		int GetHashCode ();
	}

}
