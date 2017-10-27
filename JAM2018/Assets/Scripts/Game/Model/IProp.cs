using System;
using System.Collections.Generic;

namespace Scripts.Game.Model
{

	/// <summary>
	/// A prop instance is whatever object in the room which can have qualitas attached to it.
	/// Example of prop instance may be: the fountain you can see in room #3.
	/// </summary>
	public interface IProp
	{
		/// <summary>
		/// The prop connected to this element
		/// </summary>
		/// <value>The class.</value>
		IPropType Class { get; }

		/// <summary>
		/// The list of qualitas owned by this instance of prop
		/// </summary>
		/// <value>The qualitates.</value>
		IList<IQualitas> Qualitates { get; set; }

		/// <summary>
		/// An ID that uniquely identify the prop throughout the runtime. 2 prop with the same class has different id
		/// </summary>
		int Id { get; }

	}

}

