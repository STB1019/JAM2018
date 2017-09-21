using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpGraph;
using System.Collections.Generic;
using SharpUtilities;
using System;
using SharpGraphs;

namespace Test
{
	[TestClass]
	public class TestMatrixGraph : AbstractGraphTest
	{
		[TestInitialize()]
		public override void SetupTest()
		{
			G = new NLGraph<MyNode, int>();
		}

		[TestCleanup]
		public override void TeardownTest()
		{
			G = null;
		}
	}
}
