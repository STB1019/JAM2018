using SharpGraph;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpGraphs
{
	/// <summary>
	/// Factory useful to get instances of graphs
	/// </summary>
	public class GraphFactory
	{
		private static readonly GraphFactory instance = new GraphFactory();

		private GraphFactory()
		{

		}

		/// <summary>
		/// Retrieve an instance of GraphFactory
		/// </summary>
		/// <returns>a valid instance of GraphFactory</returns>
		public static GraphFactory get()
		{
			return instance;
		}

		/// <summary>
		/// Get a general purpose graph that will work in probably all scenarios
		/// </summary>
		/// <typeparam name="NODE">the payload type related to each node</typeparam>
		/// <typeparam name="EDGE">the payload type related to each edge</typeparam>
		/// <returns>an empty grapph</returns>
		public IGraph<NODE, EDGE> getGraph<NODE, EDGE>()
		{
			return new NLGraph<NODE, EDGE>();
		}

		/// <summary>
		/// Get a graph that is best suitable for representing sparse graphs
		/// </summary>
		/// <param name="supportForPredecessors">True if you need support to quickly fetch the predecessors of a node, false otherwise</param>
		/// <typeparam name="NODE">the payload type related to each node</typeparam>
		/// <typeparam name="EDGE">the payload type related to each edge</typeparam>
		/// <returns></returns>
		public IGraph<NODE, EDGE> getSparseFocusGraph<NODE, EDGE>(bool supportForPredecessors)
		{
			if (supportForPredecessors)
			{
				return new PSLGraph<NODE, EDGE>();
			} else
			{
				return new NLGraph<NODE, EDGE>();
			}
		}

		/// <summary>
		/// Get a graph that is most suitable in representing complete graphs. You need to give all the nodes inside the graph before hand
		/// </summary>
		/// <typeparam name="NODE">the payload type related to each node</typeparam>
		/// <typeparam name="EDGE">the payload type related to each edge</typeparam>
		/// <param name="defaultEdgeValue">the value every edge in the graph has</param>
		/// <param name="nodes">a list of nodes in the graph. the n-th node in the variadic argument will have id set to n</param>
		/// <returns></returns>
		public IGraph<NODE, EDGE> getCompleteFocusGraph<NODE, EDGE>(EDGE defaultEdgeValue, params NODE[] nodes)
		{
			IGraph<NODE, EDGE> retVal = new MatrixGraph<NODE, EDGE>(nodes.Length, defaultEdgeValue);

			for (int i=0; i<nodes.Length; i++)
			{
				retVal.AddNode(i, nodes[i]);
			}

			return retVal;
		}


	}
}
