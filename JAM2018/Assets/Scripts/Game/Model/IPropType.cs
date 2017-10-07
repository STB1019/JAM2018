using System;
using System.Collections.Generic;

namespace Scripts.Game.Model
{

	/// <summary>
	/// A class implementing this interface is a type of prop.
	/// A prop is something that can be displayed inside a room.
	/// There is a difference between a "prop type" and a "prop": the latter is a concrete, viewable object inside a room that
	/// has its own state (for example an open chest) while the former is just the class of props: for example there is the prop type "fountain" but we may have several fountains:
	/// the one in room 1, the one in room 3, the one bigger of factor 1.5; hell we may even say that 2 foutains have different models! 
	/// A prop is a generic term for any viewable object. Example of Props may be: foutain, chest, key and so on
	/// </summary>
	/// <seealso cref="IPropInstance"/>
	public interface IPropType
	{
		/// <summary>
		/// The name of this prop
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Serves as a hash function for a <see cref="IProp"/> object.
		/// </summary>
		/// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
		int GetHashCode ();
	}

}

