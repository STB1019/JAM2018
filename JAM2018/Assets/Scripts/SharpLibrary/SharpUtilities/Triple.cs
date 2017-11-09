using System;
using System.Collections.Generic;
using System.Text;

namespace SharpUtilities
{
	/// <summary>
	/// A structure representing a triple of indipendent values
	/// </summary>
	/// <typeparam name="XTYPE">the type of the first value</typeparam>
	/// <typeparam name="YTYPE">the type of the second value</typeparam>
	/// <typeparam name="ZTYPE">the type of the third value</typeparam>
    public struct Triple<XTYPE,YTYPE,ZTYPE>
    {
		private XTYPE x;
		/// <summary>
		/// The first value
		/// </summary>
		public XTYPE X
		{
			get { return x; }
			private set { this.x = value; }
		}

		private YTYPE y;
		/// <summary>
		/// the second value
		/// </summary>
		public YTYPE Y
		{
			get { return y; }
			private set { this.y = value; }
		}

		private ZTYPE z;
		/// <summary>
		/// the third value
		/// </summary>
		public ZTYPE Z
		{
			get { return z; }
			private set { this.z = value; }
		}

		/// <summary>
		/// Builds a new triple
		/// </summary>
		/// <param name="x">the first value</param>
		/// <param name="y">the second value</param>
		/// <param name="z">the third value</param>
		public Triple(XTYPE x, YTYPE y, ZTYPE z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		/// <summary>
		/// Allows you to build a <see cref="Triple{XTYPE, YTYPE, ZTYPE}"/> as value, not as reference
		/// </summary>
		/// <param name="x">the first value</param>
		/// <param name="y">the second value</param>
		/// <param name="z">the third value</param>
		/// <returns>the triple you have requested</returns>
		public static Triple<XTYPE,YTYPE,ZTYPE> Build(XTYPE x, YTYPE y, ZTYPE z)
		{
			Triple<XTYPE, YTYPE, ZTYPE> retVal;
			retVal.x = x;
			retVal.y = y;
			retVal.z = z;
			return retVal;
		}

	}
}
