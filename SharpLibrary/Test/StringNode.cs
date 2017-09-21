using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
	internal class StringNode
	{
		public string S
		{
			get;
			set;
		}

		public StringNode(string s)
		{
			this.S = s;
		}

		public override string ToString()
		{
			return this.S;
		}
	}
}
