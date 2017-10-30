using Microsoft.VisualStudio.TestTools.UnitTesting;
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
