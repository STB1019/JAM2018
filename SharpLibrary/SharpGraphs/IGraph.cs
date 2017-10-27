using SharpUtilities;
using System;
using System.Collections;
using System.Collections.Generic;

namespace SharpGraph

{
	/// <summary>
	/// Represents a generic graph you an use whenever you want. The graph is encoded via a node hastable contanining, given a particular node,
	/// the lists of all its successors.
	/// Useful if you have sparse graphs or graphs you don't care about predecessors
	/// </summary>
	/// <typeparam name="NODE">A class representing what is the associated paylaod type of each node inside this graph</typeparam>
	/// <typeparam name="EDGE">A class representing what is the associated payload type of each edge inside this graph</typeparam>
    public interface IGraph<NODE, EDGE> 
    {

		/// <summary>
		/// The name of the graph. Allows you to identify this graph when you have lots of them
		/// </summary>
		string Name
		{
			get;
			set;
		}

		/// <summary>
		/// The number of nodes inside the graph
		/// </summary>
		long Size
		{
			get;
		}

		/// <summary>
		/// true if the graph has no nodes at all; false otherwise
		/// </summary>
		bool IsEmpty
		{
			get;
		}

		/// <summary>
		/// Adds a new node inside the graph
		/// </summary>
		/// <param name="payload">the node payload to associate to that node</param>
		/// <returns>true if the node has been added, false otherwise</returns>
		/// <exception cref="GraphException">if the node id you're adding already exist or something goes wrong</exception>
		bool AddNode(long id, NODE payload);

		/// <summary>
		/// Check if there is a node inside this graph with a particular id
		/// </summary>
		/// <param name="id">the id to check</param>
		/// <returns>true if the node is inside the graph, false otherwise</returns>
		bool ContainsNode(long id);

		/// <summary>
		/// Retrieve a particular node
		/// </summary>
		/// <param name="id">the id of the node to fetch</param>
		/// <returns>The node with the id you've requested</returns>
		/// <exception cref="GraphException">If the map does not have any nodes with that id </exception>
		NODE GetNode(long id);

		/// <summary>
		/// Remove a nodes inside the graph. If no nodes has that particular id, the method
		/// does nothing
		/// </summary>
		/// <param name="id">id of the node to remove</param>
		/// <returns>true if we have removed the node, false otherwise</returns>
		bool RemoveNode(long id);

		/// <summary>
		/// Aslias for GetNode
		/// </summary>
		/// <param name="id">the id of the node to fetch</param>
		/// <returns>The node with the id you've requested</returns>
		/// <exception cref="GraphException">If the map does not have any nodes with that id </exception>
		/// <see cref="GetNode(long)"/>
		NODE this[long id]
		{
			get;
		}

		/// <summary>
		/// If true, the graph has inheritly all the edges immediately available
		/// </summary>
		bool AreEdgesImplicitlyPresent
		{
			get;
		}

		/// <summary>
		/// Adds a new edge inside the graph.
		/// </summary>
		/// 
		/// 
		/// <param name="sourceId">the id of the node where the edge starts</param>
		/// <param name="sinkId">the id of the node where the edge ends</param>
		/// <param name="payload">the paylad associated to the new edge</param>
		/// <returns>true if the edge is added in the graph; false otherwise</returns>
		/// <exception cref="GraphException">if the graph is incompatible with edge overwriting if the edge is already present or one of the 2 nodes of the edge does not exist</exception>
		bool AddEdge(long sourceId, long sinkId, EDGE payload);

		/// <summary>
		/// Adds a new edge inside the graph. If the edge is already present, the method will overwrite the old value with the new one.
		/// </summary>
		/// <param name="sourceId">id of the source of the edge</param>
		/// <param name="sinkId">id of the sink of the edge</param>
		/// <param name="payload">the new payload to set to the edge</param>
		/// <returns>true if the edge was correctly updated</returns>
		/// <exception cref="GraphException">happens if sourceId or sinkId don't refer to a node in the graph</exception>
		bool AddOrUpdateEdge(long sourceId, long sinkId, EDGE payload);

		/// <summary>
		/// Check if an edge is present inside the graph
		/// </summary>
		/// <param name="sourceId">the node id where the edge starts</param>
		/// <param name="sinkId">the node id where the edge ends</param>
		/// <returns>true if there is an edge going from sourceId and ending in sinkId; false if either sourceo r sink doesn't exist or the edge doesn't exist</returns>
		bool ContainsEdge(long sourceId, long sinkId);

		/// <summary>
		/// Removes an edge from the graph.
		/// </summary>
		/// <param name="sourceId">the source of the edge to remove</param>
		/// <param name="sinkId">the sink of the edge to remove</param>
		/// <returns>true if the edge is removed, false otherwise</returns>
		/// <exception cref="GraphException">If there was no edge to begin with in the location specified</exception>
		bool RemoveEdge(long sourceId, long sinkId);

		/// <summary>
		/// Retrieve the payload of an edge
		/// </summary>
		/// <param name="sourceId">the id of the source of the edge</param>
		/// <param name="sinkId">the id of the sink of the edge</param>
		/// <returns>the label on the edge fetched</returns>
		/// <exception cref="GraphException">If the either the source, the sink or the edge was not present inside the graph</exception>
		EDGE GetEdge(long sourceId, long sinkId);

		/// <summary>
		/// Indexes operator allowing you to manipulate the graph as it was a matrix.
		/// The getter is an alias for <see cref="GetEdge(long, long)"/> while the setter is an alias for <see cref="AddOrUpdateEdge(long, long, EDGE)"/>
		/// </summary>
		/// <param name="sourceId">the id of the source of the edge</param>
		/// <param name="sinkId">the id of the sink of the edge</param>
		/// <exception cref="GraphException">if either one of the indexes don't represent a valid node</exception>
		/// <returns></returns>
		EDGE this[long sourceId, long sinkId]
		{
			get;
			set;
		}

		/// <summary>
		/// Check if a particular edge has a particular value
		/// </summary>
		/// <param name="sourceId">id of source of the edge</param>
		/// <param name="sinkId">id of the sink of the edge</param>
		/// <param name="payload">label of the edge</param>
		/// <returns>true if the edge specified has the given label, false otherwise</returns>
		/// <exception cref="GraphException">if either the source, the sink or the edge does not exist</exception>
		bool HasEdge(long sourceId, long sinkId, EDGE payload);

		/// <summary>
		/// Returns a enumerable allowing you to scroll over all the nodes in the graph
		/// </summary>
		/// <returns>an enumerable iterating over all the nodes id in the graph. Nothing is said about the order of the nodes</returns>
		IEnumerable<Pair<long, NODE>> GetNodesEnumerable();

		/// <summary>
		/// Returns an enumerable iterating over all the aedges inside the graph
		/// </summary>
		/// <returns>an iterator over all the edges of the graph. Nothing is said about the order of the edges</returns>
		IEnumerable<Triple<long, long, EDGE>> GetEdgesEnumerable();

		/// <summary>
		/// Lists every successor of the given node
		/// </summary>
		/// <param name="sourceId">the node to handle</param>
		/// <returns>a iterable of all the successors of a given node in the current graph</returns>
		IEnumerable<Pair<long, NODE>> GetSuccessorsOfNode(long sourceId);

		/// <summary>
		/// Lists every predecessor of the given node
		/// </summary>
		/// <param name="sourceId">the node to handle</param>
		/// <returns>a iterable of all the predecessors of a given node in the current graph</returns>
		IEnumerable<Pair<long, NODE>> GetPredecessorsOfNode(long sinkId);

		/// <summary>
		/// Create an PNG of the graph involved.
		/// This method rely on the fact that you system has the program "dot.exe" in your $PATH
		/// </summary>
		/// <param name="format">the name of the image. For example "hello" or someother thing. You can put printf commands inside it. You can avoid putting the extension</param>
		/// <param name="list">a list of object useful to fill format variable. For example i format variable is "hello_{0}", you can put in the varaidic argument the value "42" to obtain the name "hello_42"</param>
		/// <returns>the name of the image file created</returns>
		string DrawGraph(string format, params object[] list);
	}


}
