using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpGraphs;
using System.Collections.Generic;
using SharpUtilities;
using System;
using SharpGraphs;

namespace Test
{
	[TestClass]
	public class TestPSLGraph : AbstractGraphTest
	{

		[TestInitialize()]
		public override  void SetupTest()
		{
			G = new PSLGraph<MyNode, int>();
		}

		[TestCleanup]
		public override void TeardownTest()
		{
			G = null;
		}
	}
}
