using System;
using System.Collections.Generic;
using System.Text;

namespace SharpUtilities
{
    public struct Pair<XTYPE,YTYPE>
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

		public Pair(XTYPE x, YTYPE y)
		{
			this.X = x;
			this.Y = y;
		}
    }
}
