using System;
using System.Collections.Generic;
using System.IO;
using SharpUtilities;
using log4net;
using SharpGraphs;

namespace SharpGraph
{
	public partial class NLGraph<NODE, EDGE> : IGraph<NODE, EDGE> 
    {
		private static readonly ILog LOG = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		/// <summary>
		/// A container of all the nodes available within the graph
		/// </summary>
		private Dictionary<long, NODE> nodes;
		/// <summary>
		/// Represents the node list identifying the whole graph.
		/// The keys of the dictionaries are the ID of the nodes inside the graph.
		/// This each key identifies a node. Each node has then several successors.
		/// Each successor is identifiable via a ID node as well. The second dictionary has as keys the ID of the sink of the edge
		/// while the value is the edge itself.
		/// </summary>
		private Dictionary<long, Dictionary<long, EDGE>> nodeList;

		public NLGraph()
		{
			this.nodes = new Dictionary<long, NODE>();
			this.nodeList = new Dictionary<long, Dictionary<long, EDGE>>();
		}

		public NODE this[long id]
		{
			get
			{
				return this.GetNode(id);
			}
		}

		public string Name { get; set; }

		public long Size
		{
			get
			{
				return this.nodes.Count;
			}
		}

		public bool IsEmpty
		{
			get
			{
				return this.nodes.Count == 0;
			}
		}

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

		public bool AreEdgesImplicitlyPresent
		{
			get { return false; }
		}

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

			if (!this.nodeList.ContainsKey(sourceId))
			{
				//there are no edges coming out from node. We create the list of them
				this.nodeList[sourceId] = new Dictionary<long, EDGE>();
			}

			if (this.nodeList[sourceId].ContainsKey(sinkId))
			{
				//there is already an edge from source->sink. We throw an error
				throw new GraphException(String.Format("Edge {0}->{1} is already present", sourceId, sinkId));
			}

			this.nodeList[sourceId].Add(sinkId, payload);
			return true;
		}

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

			if (!this.nodeList.ContainsKey(sourceId))
			{
				//there are no edges coming out from node. We create the list of them
				this.nodeList[sourceId] = new Dictionary<long, EDGE>();
			}

			if (this.nodeList[sourceId].ContainsKey(sinkId))
			{
				this.nodeList[sourceId][sinkId] = payload;
			} else {
				this.nodeList[sourceId].Add(sinkId, payload);
			}

			return true;
		}

		public bool AddNode(long id, NODE n)
		{
			if (this.nodes.ContainsKey(id))
			{
				throw new GraphException(string.Format("the node with id {0} already exists", id));
			}
			this.nodes.Add(id, n);
			return true;
		}

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

			if (!this.nodeList.ContainsKey(sourceId))
			{
				//there are no edges coming out from source
				return false;
			}

			return this.nodeList[sourceId].ContainsKey(sinkId);
		}

		public bool ContainsNode(long id)
		{
			return this.nodes.ContainsKey(id);
		}

		public NODE GetNode(long id)
		{
			
			try
			{
				//KeyNotFoundException is thrown by [] operator
				return this.nodes[id];
			} catch (KeyNotFoundException e)
			{
				throw new GraphException(e);
			} 
			
		}

		public EDGE GetEdge(long sourceId, long sinkId)
		{
			try
			{
				return this.nodeList[sourceId][sinkId];
			} catch (KeyNotFoundException e)
			{
				throw new GraphException(string.Format("Can't access to {0}->{1} edge", sourceId, sinkId), e);
			}
			
		}

		public bool RemoveNode(long id)
		{
			return this.nodes.Remove(id);
		}

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

			if (!this.nodeList.ContainsKey(sourceId))
			{
				throw new GraphException(String.Format("Node {0} doesn't have any successors!", sourceId));
			}

			if (!this.nodeList[sourceId].ContainsKey(sinkId))
			{
				throw new GraphException(String.Format("Node {0} does not have a successors to node {1}!", sourceId, sinkId));
			}

			this.nodeList[sourceId].Remove(sinkId);

			if (this.nodeList[sourceId].Count == 0)
			{
				this.nodeList.Remove(sourceId);
			}

			return true;
		}

		public bool HasEdge(long sourceId, long sinkId, EDGE payload)
		{
			EDGE actual = this.GetEdge(sourceId, sinkId);
			if (actual == null)
			{
				return payload == null;
			}
			return actual.Equals(payload);
		}

		public IEnumerable<Pair<long, NODE>> GetNodesEnumerable()
		{
			foreach (long nId in this.nodes.Keys)
			{
				yield return new Pair<long, NODE>(nId, nodes[nId]);
			}
		}

		public IEnumerable<Triple<long, long, EDGE>> GetEdgesEnumerable()
		{
			foreach (long sourceId in this.nodes.Keys)
			{
				if (!this.nodeList.ContainsKey(sourceId))
				{
					continue;
				}

				foreach (long sinkId in this.nodeList[sourceId].Keys)
				{
					yield return new Triple<long, long, EDGE>(sourceId, sinkId, this[sourceId, sinkId]);
				}
			}
		}

		public IEnumerable<Pair<long, NODE>> GetSuccessorsOfNode(long sourceId)
		{
			if (!this.nodeList.ContainsKey(sourceId))
			{
				yield break;
			}
			foreach (long sinkId in this.nodeList[sourceId].Keys)
			{
				yield return new Pair<long, NODE>(sinkId, this[sinkId]);
			}
		}

		public IEnumerable<Pair<long, NODE>> GetPredecessorsOfNode(long sinkId)
		{
			foreach (long sourceId in this.nodes.Keys)
			{
				if (!this.nodeList.ContainsKey(sourceId))
				{
					continue;
				}
				if (!this.nodeList[sourceId].ContainsKey(sinkId))
				{
					continue;
				}
				yield return new Pair<long,NODE>(sourceId, this[sourceId]);
			}
		}

		public string DrawGraph(string format, params object[] list)
		{
			return GraphsCommons.DrawGraph<NODE, EDGE>(this, format, list);
		}
	}
}
