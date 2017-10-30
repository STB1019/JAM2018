using System;
using System.Collections.Generic;
using System.Text;

namespace SharpUtilities
{
    public struct Triple<XTYPE,YTYPE,ZTYPE>
    {
		public XTYPE X
		{
			get;
			private set;
		}
		public YTYPE Y
		{
			get;
			private set;
		}

		public ZTYPE Z
		{
			get;
			private set;
		}

		public Triple(XTYPE x, YTYPE y, ZTYPE z)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
		}

	}
}
