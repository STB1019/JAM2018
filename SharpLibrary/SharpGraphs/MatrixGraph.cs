using SharpGraphs;
using System;
using System.Collections.Generic;
using System.Text;
using SharpUtilities;

namespace SharpGraphs
{
	/// <summary>
	/// A graph implemented with a adjacent matrix
	/// </summary>
	/// <typeparam name="NODE">the payload type associated to every node</typeparam>
	/// <typeparam name="EDGE">the payload type associated to every edge</typeparam>
	public partial class MatrixGraph<NODE, EDGE> : IGraph<NODE, EDGE>
	{
		private Dictionary<long, NODE> nodes;
		private EDGE[,] graph;

		public EDGE DefaultEdgeLabel
		{
			get;
			private set;
		}

		/// <summary>
		/// Creates a new matrix graph
		/// </summary>
		/// <param name="capacity">the maximum number of nodes you can put inside this graph</param>
		/// <param name="defaultValue">the default value each edge has</param>
		public MatrixGraph(int capacity, EDGE defaultValue)
		{
			this.nodes = new Dictionary<long, NODE>();
			this.graph = new EDGE[capacity, capacity];
			this.DefaultEdgeLabel = defaultValue;

			for (int y=0; y< capacity; y++)
			{
				for (int x=0; x< capacity; x++)
				{
					this.graph[y, x] = defaultValue;
				}
			}
		}

		public NODE this[long id] {
			get
			{
				return this.GetNode(id);
			}
		}

		public EDGE this[long sourceId, long sinkId] {
			get
			{
				return this.GetEdge(sourceId, sinkId);
			}
			set
			{
				this.AddOrUpdateEdge(sourceId, sinkId, value);
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

		public bool AreEdgesImplicitlyPresent
		{
			get { return true; }
		}

		public bool AddEdge(long sourceId, long sinkId, EDGE payload)
		{
			return this.AddOrUpdateEdge(sourceId, sinkId, payload);
		}

		public bool AddNode(long id, NODE payload)
		{
			if (this.nodes.ContainsKey(id))
			{
				throw new GraphException(string.Format("the node with id {0} already exists!", id));
			}
			if (id >= this.graph.GetLength(0))
			{
				throw new GraphException(string.Format("the id of the node you're trying to add ({0}) is too big for the matrix graph (acceptable ids up to {1})!", id, this.graph.Length));
			}

			this.nodes.Add(id, payload);
			return true;
		}

		public bool AddOrUpdateEdge(long sourceId, long sinkId, EDGE payload)
		{
			if (!this.ContainsNode(sourceId))
			{
				throw new GraphException(string.Format("graph has no source node {0}", sourceId));
			}

			if (!this.ContainsNode(sinkId))
			{
				throw new GraphException(string.Format("graph has no sink node {0}", sinkId));
			}

			this.graph[sourceId, sinkId] = payload;
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
			return true;
		}

		public bool ContainsNode(long id)
		{
			return this.nodes.ContainsKey(id);
		}

		public string DrawGraph(string format, params object[] list)
		{
			return GraphsCommons.DrawGraph<NODE, EDGE>(this, format, list);
		}

		public EDGE GetEdge(long sourceId, long sinkId)
		{
			if (!this.ContainsNode(sourceId))
			{
				throw new GraphException(string.Format("graph has no source node {0}", sourceId));
			}

			if (!this.ContainsNode(sinkId))
			{
				throw new GraphException(string.Format("graph has no sink node {0}", sinkId));
			}

			return this.graph[sourceId, sinkId];
		}

		public IEnumerable<Triple<long, long, EDGE>> GetEdgesEnumerable()
		{
			foreach (Pair<long, NODE> source in this.GetNodesEnumerable())
			{
				foreach (Pair<long, NODE> sink in this.GetNodesEnumerable())
				{
					yield return new Triple<long, long, EDGE>(source.X, sink.X, this[source.X, sink.X]);
				}
			}
		}

		public NODE GetNode(long id)
		{
			try {
				return this.nodes[id];
			} catch (KeyNotFoundException e)
			{
				throw new GraphException(e);
			}
			
		}

		public IEnumerable<Pair<long, NODE>> GetNodesEnumerable()
		{
			foreach (long nId in this.nodes.Keys)
			{
				yield return new Pair<long, NODE>(nId, this[nId]);
			}
		}

		public IEnumerable<Pair<long, NODE>> GetSuccessorsOfNode(long sourceId)
		{
			for (int x=0; x<this.graph.GetLength(1); x++)
			{
				if (!this.nodes.ContainsKey(x))
				{
					continue;
				}
				yield return new Pair<long, NODE>(x, this[x]);
			}
		}

		public IEnumerable<Pair<long, NODE>> GetPredecessorsOfNode(long sinkId)
		{
			for (int y = 0; y < this.graph.GetLength(0); y++)
			{
				if (!this.nodes.ContainsKey(y))
				{
					continue;
				}
				yield return new Pair<long, NODE>(y, this[y]);
			}
		}

		public bool HasEdge(long sourceId, long sinkId, EDGE payload)
		{
			return payload == null 
				? this[sourceId, sinkId] == null 
				: payload.Equals(this[sourceId, sinkId]);
		}

		public bool RemoveEdge(long sourceId, long sinkId)
		{
			this.AddOrUpdateEdge(sourceId, sinkId, this.DefaultEdgeLabel);
			return true;
		}

		public bool RemoveNode(long id)
		{
			//remove all the edges on the row of the node
			for (int x = 0; x < this.graph.GetLength(1); x++)
			{
				this[id, x] = this.DefaultEdgeLabel;
			}

			//remove all the edges on the column of the node
			for (int y = 0; y < this.graph.GetLength(0); y++)
			{
				this[y, id] = this.DefaultEdgeLabel;
			}
			this.nodes.Remove(id);
			return true;
		}

		/// <summary>
		/// Check if the payload given is equal to the default edge. This methood differs from a simple Equals call because it handle the case
		/// where the default edge label is null
		/// </summary>
		/// <param name="payload">the edge payload to check</param>
		/// <returns>True if the payload and the default edge label are the same, false otherwise</returns>
		private bool isEdgeEqualToDefaultOne(EDGE payload)
		{
			if (this.DefaultEdgeLabel == null)
			{
				return payload == null;
			}

			return this.DefaultEdgeLabel.Equals(payload);
		}
	}
}
