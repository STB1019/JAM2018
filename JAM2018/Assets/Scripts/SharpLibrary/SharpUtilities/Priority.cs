using System;
using System.Collections.Generic;
using System.Text;

namespace SharpUtilities
{
	/// <summary>
	/// A mapping representing default priorities used through out all the proejct.
	/// If some structure is priority based, this methods can be used to set the priority.
	/// </summary>
	public enum Priority
	{
		/// <summary>
		/// Highest possible priority
		/// </summary>
		HIGHEST = 1,
		/// <summary>
		/// higher priority
		/// </summary>
		HIGHER = 10,
		/// <summary>
		/// High priority
		/// </summary>
		HIGH = 20,
		/// <summary>
		/// priority just more important than <see cref="Priority.MEDIUM"/>
		/// </summary>
		ABOVE_AVERAGE= 30,
		/// <summary>
		/// medium priority
		/// </summary>
		MEDIUM = 40,
		/// <summary>
		/// priority just less important than <see cref="Priority.MEDIUM"/>
		/// </summary>
		BELOW_AVERAGE = 50,
		/// <summary>
		/// low priority
		/// </summary>
		LOW = 60,
		/// <summary>
		/// lower priority
		/// </summary>
		LOWER = 70,
		/// <summary>
		/// lowest possible priority
		/// </summary>
		LOWEST = 80
	}
}
