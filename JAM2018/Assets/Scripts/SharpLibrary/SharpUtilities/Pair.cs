using System;
using System.Collections.Generic;
using System.Text;

namespace SharpUtilities
{
	/// <summary>
	/// Represents a pair of 2 indipendent values
	/// </summary>
	/// <typeparam name="XTYPE">the type of the first value, namely "X"</typeparam>
	/// <typeparam name="YTYPE">the type of the second value, namely "Y"</typeparam>
    public struct Pair<XTYPE,YTYPE>
    {
		/// <summary>
		/// field representing the first value.
		/// </summary>
		/// <remarks><see href="https://stackoverflow.com/a/3943162/1887602">This SO question</see> explains why we need to introduce this field</remarks>
		private XTYPE x;
		/// <summary>
		/// property masking <see cref="x"/>
		/// </summary>
		public XTYPE X
		{
			get { return x; }
			private set { x = value; }
		}
		/// <summary>
		/// field representing the second value
		/// </summary>
		/// <remarks><see href="https://stackoverflow.com/a/3943162/1887602">This SO question</see> explains why we need to introduce this field</remarks>
		private YTYPE y;
		/// <summary>
		/// property masking <see cref="y"/>
		/// </summary>
		public YTYPE Y
		{
			get { return y; }
			private set { y = value; }
		}

		/// <summary>
		/// initialize a new pair
		/// </summary>
		/// <param name="x">the first value</param>
		/// <param name="y">the second value</param>
		public Pair(XTYPE x, YTYPE y)
		{
			this.x = x;
			this.y = y;
		}

		/// <summary>
		/// Creates a new pair without the "new" keyword
		/// </summary>
		/// <param name="x">the first value</param>
		/// <param name="y">the second value</param>
		/// <returns>the pair built</returns>
		public static Pair<XTYPE, YTYPE> Build(XTYPE x, YTYPE y)
		{
			Pair<XTYPE, YTYPE> retVal;
			retVal.x = x;
			retVal.y = y;
			return retVal;
		}
    }
}
