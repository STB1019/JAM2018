using log4net;
using SharpGraphs;
using SharpUtilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpGraphs
{
	/// <summary>
	/// Represents a graph implemented via both predecessors lists and successors lists.
	/// Thanks to those list, you can retrieve both the successors and the predecessors of a node
	/// </summary>
	/// <typeparam name="NODE"></typeparam>
	/// <typeparam name="EDGE"></typeparam>
	public class PSLGraph<NODE, EDGE> : IGraph<NODE, EDGE>
	{
		private static readonly ILog LOG = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		/// <summary>
		/// A container of all the nodes available within the graph
		/// </summary>
		private Dictionary<long, NODE> nodes;
		/// <summary>
		/// Represents the successors of each node.
		/// The keys of the dictionaries are the ID of the nodes inside the graph.
		/// This each key identifies a node. Each node has then several successors.
		/// Each successor is identifiable via a ID node as well. The second dictionary has as keys the ID of the sink of the edge
		/// while the value is the edge itself.
		/// 
		/// Node successors is also used to check graph sanity. The implementation avoid all the tests on the predecessors list since it relies on the
		/// tests done on the successors list
		/// </summary>
		private Dictionary<long, Dictionary<long, EDGE>> nodeSuccessors;

		/// <summary>
		/// Represents the precedessors of each node.
		/// The keys of the dictionaries are the ID of the nodes inside the graph.
		/// This each key identifies a node. Each node has then several predecessors.
		/// Each predecessor is identifiable via a ID node as well. The second dictionary has as keys the ID of the source of the edge
		/// while the value is the edge itself.
		/// </summary>
		private Dictionary<long, Dictionary<long, EDGE>> nodePredecessors;


		public PSLGraph()
		{
			this.nodes = new Dictionary<long, NODE>();
			this.nodeSuccessors = new Dictionary<long, Dictionary<long, EDGE>>();
			this.nodePredecessors = new Dictionary<long, Dictionary<long, EDGE>>();
		}

		/// <inheritdoc/>
		public NODE this[long id]
		{
			get
			{
				return this.GetNode(id);
			}
		}

		/// <inheritdoc/>
		public string Name { get; set; }

		/// <inheritdoc/>
		public long Size
		{
			get
			{
				return this.nodes.Count;
			}
		}

		/// <inheritdoc/>
		public bool IsEmpty
		{
			get
			{
				return this.nodes.Count == 0;
			}
		}

		/// <inheritdoc/>
		public EDGE this[long sourceId, long sinkId]
		{
			get
			{
				return GetEdge(sourceId, sinkId);
			}
			set
			{
				AddOrUpdateEdge(sourceId, sinkId, value);
			}
		}

		/// <inheritdoc/>
		public bool AreEdgesImplicitlyPresent
		{
			get { return false; }
		}

		/// <inheritdoc/>
		public bool AddEdge(long sourceId, long sinkId, EDGE payload)
		{
			if (!this.ContainsNode(sourceId))
			{
				throw new GraphException(String.Format("Can't find the node with id {0}", sourceId));
			}
			if (!this.ContainsNode(sinkId))
			{
				throw new GraphException(String.Format("Can't find the node with id {0}", sinkId));
			}

			if (!this.nodeSuccessors.ContainsKey(sourceId))
			{
				//there are no edges coming out from node. We create the list of them
				this.nodeSuccessors[sourceId] = new Dictionary<long, EDGE>();
			}

			if (this.nodeSuccessors[sourceId].ContainsKey(sinkId))
			{
				//there is already an edge from source->sink. We throw an error
				throw new GraphException(String.Format("Edge {0}->{1} is already present", sourceId, sinkId));
			}

			this.nodeSuccessors[sourceId].Add(sinkId, payload);

			if (!this.nodePredecessors.ContainsKey(sinkId))
			{
				this.nodePredecessors[sinkId] = new Dictionary<long, EDGE>();
			}
			nodePredecessors[sinkId].Add(sourceId, payload);
			return true;
		}

		/// <inheritdoc/>
		public bool AddOrUpdateEdge(long sourceId, long sinkId, EDGE payload)
		{
			if (!this.ContainsNode(sourceId))
			{
				throw new GraphException(String.Format("Can't find the node with id {0}", sourceId));
			}
			if (!this.ContainsNode(sinkId))
			{
				throw new GraphException(String.Format("Can't find the node with id {0}", sinkId));
			}

			if (!this.nodeSuccessors.ContainsKey(sourceId))
			{
				//there are no edges coming out from node. We create the list of them
				this.nodeSuccessors[sourceId] = new Dictionary<long, EDGE>();
			}

			if (this.nodeSuccessors[sourceId].ContainsKey(sinkId))
			{
				this.nodeSuccessors[sourceId][sinkId] = payload;
			}
			else
			{
				this.nodeSuccessors[sourceId].Add(sinkId, payload);
			}

			if (!this.nodePredecessors.ContainsKey(sinkId))
			{
				this.nodePredecessors[sinkId] = new Dictionary<long, EDGE>();
			}
			if (this.nodePredecessors[sinkId].ContainsKey(sourceId))
			{
				this.nodePredecessors[sinkId][sourceId] = payload;
			} else
			{
				this.nodePredecessors[sinkId].Add(sourceId, payload);
			}
			
			return true;
		}

		/// <inheritdoc/>
		public bool AddNode(long id, NODE n)
		{
			if (this.nodes.ContainsKey(id))
			{
				throw new GraphException(string.Format("the node with id {0} already exists", id));
			}
			this.nodes.Add(id, n);
			return true;
		}

		/// <inheritdoc/>
		public bool ContainsEdge(long sourceId, long sinkId)
		{
			if (!this.ContainsNode(sourceId))
			{
				return false;
			}
			if (!this.ContainsNode(sinkId))
			{
				return false;
			}

			if (!this.nodeSuccessors.ContainsKey(sourceId))
			{
				//there are no edges coming out from source
				return false;
			}

			return this.nodeSuccessors[sourceId].ContainsKey(sinkId);
		}

		/// <inheritdoc/>
		public bool ContainsNode(long id)
		{
			return this.nodes.ContainsKey(id);
		}

		/// <inheritdoc/>
		public NODE GetNode(long id)
		{

			try
			{
				//KeyNotFoundException is thrown by [] operator
				return this.nodes[id];
			}
			catch (KeyNotFoundException e)
			{
				throw new GraphException(e);
			}

		}

		/// <inheritdoc/>
		public EDGE GetEdge(long sourceId, long sinkId)
		{
			try
			{
				return this.nodeSuccessors[sourceId][sinkId];
			}
			catch (KeyNotFoundException e)
			{
				throw new GraphException(string.Format("Can't access to {0}->{1} edge", sourceId, sinkId), e);
			}

		}

		/// <inheritdoc/>
		public bool RemoveNode(long id)
		{
			return this.nodes.Remove(id);
		}

		/// <inheritdoc/>
		public bool RemoveEdge(long sourceId, long sinkId)
		{
			if (!this.ContainsNode(sourceId))
			{
				throw new GraphException(String.Format("Can't find the node with id {0}", sourceId));
			}
			if (!this.ContainsNode(sinkId))
			{
				throw new GraphException(String.Format("Can't find the node with id {0}", sinkId));
			}

			if (!this.nodeSuccessors.ContainsKey(sourceId))
			{
				throw new GraphException(String.Format("Node {0} doesn't have any successors!", sourceId));
			}

			if (!this.nodeSuccessors[sourceId].ContainsKey(sinkId))
			{
				throw new GraphException(String.Format("Node {0} does not have a successors to node {1}!", sourceId, sinkId));
			}

			this.nodeSuccessors[sourceId].Remove(sinkId);
			if (this.nodeSuccessors[sourceId].Count == 0)
			{
				this.nodeSuccessors.Remove(sourceId);
			}

			this.nodePredecessors[sinkId].Remove(sourceId);
			if (this.nodePredecessors[sinkId].Count == 0)
			{
				this.nodePredecessors.Remove(sinkId);
			}

			return true;
		}

		/// <inheritdoc/>
		public bool HasEdge(long sourceId, long sinkId, EDGE payload)
		{
			EDGE actual = this.GetEdge(sourceId, sinkId);
			if (actual == null)
			{
				return payload == null;
			}
			return actual.Equals(payload);
		}

		/// <inheritdoc/>
		public IEnumerable<Pair<long, NODE>> GetNodesEnumerable()
		{
			foreach (long nId in this.nodes.Keys)
			{
				yield return new Pair<long, NODE>(nId, nodes[nId]);
			}
		}

		/// <inheritdoc/>
		public IEnumerable<Triple<long, long, EDGE>> GetEdgesEnumerable()
		{
			foreach (long sourceId in this.nodes.Keys)
			{
				if (!this.nodeSuccessors.ContainsKey(sourceId))
				{
					continue;
				}

				foreach (long sinkId in this.nodeSuccessors[sourceId].Keys)
				{
					yield return new Triple<long, long, EDGE>(sourceId, sinkId, this[sourceId, sinkId]);
				}
			}
		}

		/// <inheritdoc/>
		public IEnumerable<Pair<long, NODE>> GetSuccessorsOfNode(long sourceId)
		{
			if (!this.nodeSuccessors.ContainsKey(sourceId))
			{
				yield break;
			}
			foreach (long sinkId in this.nodeSuccessors[sourceId].Keys)
			{
				yield return new Pair<long, NODE>(sinkId, this[sinkId]);
			}
		}

		/// <inheritdoc/>
		public IEnumerable<Pair<long, NODE>> GetPredecessorsOfNode(long sinkId)
		{
			if (!this.nodePredecessors.ContainsKey(sinkId))
			{
				yield break;
			}
			foreach (long sourceId in this.nodePredecessors[sinkId].Keys)
			{
				yield return new Pair<long, NODE>(sourceId, this[sourceId]);
			}
		}

		/// <inheritdoc/>
		public string DrawGraph(string format, params object[] list)
		{
			return GraphsCommons.DrawGraph<NODE, EDGE>(this, format, list);
		}
	}
}
