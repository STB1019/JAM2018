using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpGraphs;

namespace Test
{
	public abstract class AbstractGraphTest
	{
		internal IGraph<MyNode, int> G { get; set; }

		public abstract void SetupTest();

		public virtual void TeardownTest()
		{
			this.G = null;
		}


		[TestMethod]
		public void TestAddNode()
		{
			G.AddNode(0, new MyNode());
			Assert.IsTrue(G.ContainsNode(0));
			Assert.IsTrue(!G.ContainsNode(1));
		}

		[TestMethod]
		public void TestCount()
		{
			Assert.IsTrue(G.Size == 0);
			for (int i = 0; i < 10; i++)
			{
				G.AddNode(i, new MyNode());
			}
			Assert.IsTrue(G.Size == 10);

		}

		[TestMethod]
		public void testGetNode()
		{
			Assert.IsTrue(G.Size == 0);
			MyNode[] nodes = new MyNode[10];
			for (int i = 0; i < 10; i++)
			{
				nodes[i] = new MyNode();
				G.AddNode(i, nodes[i]);
			}
			Assert.IsTrue(G.GetNode(5) == nodes[5]);
			Assert.ThrowsException<GraphException>(delegate ()
			{
				G.GetNode(15);
			});

			//check the same with the indexer

			Assert.IsTrue(G[5] == nodes[5]);
			Assert.ThrowsException<GraphException>(delegate ()
			{
				MyNode tmp = G[15];
			});
		}

		[TestMethod]
		public void TestRemove()
		{
			for (int i = 0; i < 10; i++)
			{
				G.AddNode(i, new MyNode());
			}

			if (!G.AreEdgesImplicitlyPresent)
			{
				Assert.IsFalse(G.RemoveNode(15));
			}
			Assert.IsTrue(G.Size == 10);
			Assert.IsTrue(G.RemoveNode(7));
			Assert.IsTrue(G.Size == 9);
		}

		[TestMethod]
		public void TestAddEdge()
		{
			for (int i = 0; i < 10; i++)
			{
				G.AddNode(i, new MyNode());
			}

			if (!G.AreEdgesImplicitlyPresent)
			{
				Assert.IsFalse(G.ContainsEdge(0, 3));
			}

			Assert.IsTrue(G.AddEdge(0, 3, 5));
			Assert.IsTrue(G.ContainsEdge(0, 3));

			if (G.AreEdgesImplicitlyPresent)
			{
				Assert.IsTrue(G.AddEdge(0, 3, 10));
			}
			else
			{
				Assert.ThrowsException<GraphException>(delegate ()
				{
					G.AddEdge(0, 3, 10);
				});
			}

			Assert.ThrowsException<GraphException>(delegate ()
			{
				G.AddEdge(11, 3, 10);
			});


			Assert.ThrowsException<GraphException>(delegate ()
			{
				G.AddEdge(0, 11, 10);
			});
		}

		[TestMethod]
		public void TestRemoveEdge()
		{
			for (int i = 0; i < 10; i++)
			{
				G.AddNode(i, new MyNode());
			}

			Assert.IsTrue(G.AddEdge(0, 1, 10));
			Assert.IsTrue(G.AddEdge(1, 2, 15));
			Assert.IsTrue(G.AddEdge(2, 3, 20));
			Assert.IsTrue(G.AddEdge(3, 4, 25));
			Assert.IsTrue(G.AddEdge(4, 5, 30));
			Assert.IsTrue(G.AddEdge(5, 0, 35));

			Assert.IsTrue(G.ContainsEdge(0, 1));
			Assert.IsTrue(G.ContainsEdge(1, 2));
			Assert.IsTrue(G.ContainsEdge(2, 3));
			Assert.IsTrue(G.ContainsEdge(3, 4));
			Assert.IsTrue(G.ContainsEdge(4, 5));
			Assert.IsTrue(G.ContainsEdge(5, 0));

			G.RemoveEdge(2, 3);
			if (!G.AreEdgesImplicitlyPresent)
			{
				Assert.IsFalse(G.ContainsEdge(2, 3));
			}
			Assert.IsTrue(G.AddEdge(2, 3, 20));
			Assert.IsTrue(G.ContainsEdge(2, 3));

			Assert.ThrowsException<GraphException>(delegate ()
			{
				G.RemoveEdge(11, 5);
			});

			Assert.ThrowsException<GraphException>(delegate ()
			{
				G.RemoveEdge(5, 11);
			});


			if (!G.AreEdgesImplicitlyPresent)
			{
				Assert.ThrowsException<GraphException>(delegate ()
				{
					G.RemoveEdge(0, 2);
				});
			}
			else
			{
				Assert.IsTrue(G.RemoveEdge(0, 2));
			}

		}

		[TestMethod]
		public void TestGetSuccessorsOfNode()
		{
			for (int i = 0; i < 10; i++)
			{
				G.AddNode(i, new MyNode());
			}

			Assert.IsTrue(G.AddEdge(0, 1, 10));
			Assert.IsTrue(G.AddEdge(1, 2, 15));
			Assert.IsTrue(G.AddEdge(2, 3, 20));
			Assert.IsTrue(G.AddEdge(3, 4, 25));
			Assert.IsTrue(G.AddEdge(4, 5, 30));
			Assert.IsTrue(G.AddEdge(5, 0, 35));

			Assert.IsTrue(G.AddEdge(3, 2, 1));
			Assert.IsTrue(G.AddEdge(3, 1, 2));

			int sum = 0;
			foreach (var successor in G.GetSuccessorsOfNode(3))
			{
				sum += G.GetEdge(3, successor.X);
			}
			Assert.IsTrue(sum == 28);
		}

		[TestMethod]
		public void TestGetPredecessorsOfNode()
		{
			for (int i = 0; i < 10; i++)
			{
				G.AddNode(i, new MyNode());
			}

			Assert.IsTrue(G.AddEdge(0, 1, 10));
			Assert.IsTrue(G.AddEdge(1, 2, 15));
			Assert.IsTrue(G.AddEdge(2, 3, 20));
			Assert.IsTrue(G.AddEdge(3, 4, 25));
			Assert.IsTrue(G.AddEdge(4, 5, 30));
			Assert.IsTrue(G.AddEdge(5, 0, 35));

			Assert.IsTrue(G.AddEdge(3, 2, 1));
			Assert.IsTrue(G.AddEdge(3, 1, 2));

			int sum = 0;
			foreach (var predecessor in G.GetPredecessorsOfNode(2))
			{
				sum += G.GetEdge(predecessor.X, 2);
			}
			Assert.IsTrue(sum == 16);
		}

		[TestMethod]
		public void TestAddOrUpdateEdge()
		{
			for (int i = 0; i < 10; i++)
			{
				G.AddNode(i, new MyNode());
			}

			Assert.IsTrue(G.AddEdge(0, 1, 10));
			Assert.IsTrue(G.AddEdge(1, 2, 15));
			Assert.IsTrue(G.AddEdge(2, 3, 20));
			Assert.IsTrue(G.AddEdge(3, 4, 25));
			Assert.IsTrue(G.AddEdge(4, 5, 30));
			Assert.IsTrue(G.AddEdge(5, 0, 35));

			Assert.IsTrue(G.GetEdge(2, 3) == 20);

			//add edge wont' work on update
			if (!G.AreEdgesImplicitlyPresent)
			{
				Assert.ThrowsException<GraphException>(delegate ()
				{
					G.AddEdge(2, 3, 100);
				});
			}
			else
			{
				Assert.IsTrue(G.AddEdge(2, 3, 100));
			}


			//add or update will work
			G.AddOrUpdateEdge(2, 3, 100);
			Assert.IsTrue(G.GetEdge(2, 3) == 100);

			//create a new edge
			if (!G.AreEdgesImplicitlyPresent)
			{
				Assert.IsFalse(G.ContainsEdge(0, 3));
			}
			G.AddOrUpdateEdge(0, 3, 10);
			Assert.IsTrue(G.ContainsEdge(0, 3));
			Assert.IsTrue(G.GetEdge(0, 3) == 10);

			//we do the same with indexes
			Assert.IsTrue(G.ContainsEdge(2, 3));
			G[2, 3] = 50;
			Assert.IsTrue(G.ContainsEdge(2, 3));
			Assert.IsTrue(G.ContainsEdge(0, 1));
			G[0, 1] = 150;
			Assert.IsTrue(G.GetEdge(0, 1) == 150);

			Assert.IsTrue(G[0, 1] == 150);
			Assert.IsTrue(G[1, 2] == 15);
			Assert.IsTrue(G[2, 3] == 50);
			Assert.IsTrue(G[3, 4] == 25);
			Assert.IsTrue(G[4, 5] == 30);
			Assert.IsTrue(G[5, 0] == 35);

			Assert.ThrowsException<GraphException>(delegate ()
			{
				G[11, 5] = 4;
			});

			Assert.ThrowsException<GraphException>(delegate ()
			{
				G[5, 11] = 4;
			});
		}

		[TestMethod]
		public void TestHasEdge()
		{
			for (int i = 0; i < 10; i++)
			{
				G.AddNode(i, new MyNode());
			}

			Assert.IsTrue(G.AddEdge(0, 1, 10));
			Assert.IsTrue(G.AddEdge(1, 2, 15));
			Assert.IsTrue(G.AddEdge(2, 3, 20));
			Assert.IsTrue(G.AddEdge(3, 4, 25));
			Assert.IsTrue(G.AddEdge(4, 5, 30));
			Assert.IsTrue(G.AddEdge(5, 0, 35));

			Assert.IsTrue(G.HasEdge(0, 1, 10));
			Assert.IsFalse(G.HasEdge(0, 1, 9));

			//what about the null values?
			NLGraph<MyNode, MyNode> myGraph = new NLGraph<MyNode, MyNode>();

			myGraph.AddNode(0, new MyNode());
			myGraph.AddNode(1, new MyNode());
			myGraph.AddNode(2, new MyNode());
			MyNode e01 = new MyNode();
			myGraph.AddOrUpdateEdge(0, 1, e01);
			myGraph.AddOrUpdateEdge(1, 2, null);

			Assert.IsTrue(myGraph.HasEdge(0, 1, e01));
			Assert.IsFalse(myGraph.HasEdge(0, 1, null));

			Assert.IsFalse(myGraph.HasEdge(1, 2, e01));
			Assert.IsTrue(myGraph.HasEdge(1, 2, null));
		}

		[TestMethod]
		public void TestDrawGraph()
		{
			string s0 = "Fountain";
			string s1 = "Flames";
			string s2 = "Lava";
			string s3 = "Boss";
			string s4 = "Treasure";
			NLGraph<StringNode, double> example;

			example = new NLGraph<StringNode, double>();
			example.AddNode(0, new StringNode(s0));
			example.AddNode(1, new StringNode(s1));
			example.AddNode(2, new StringNode(s2));
			example.AddNode(3, new StringNode(s3));
			example.AddNode(4, new StringNode(s4));

			example.AddOrUpdateEdge(0, 1, 0.3);
			example.AddOrUpdateEdge(0, 2, 0.4);
			example.AddOrUpdateEdge(2, 4, 0.1);
			example.AddOrUpdateEdge(3, 2, 0.3);
			example.AddOrUpdateEdge(1, 4, 0.7);
			example.AddOrUpdateEdge(2, 1, 0.4);

			example.DrawGraph("test");

		}
	}
}

