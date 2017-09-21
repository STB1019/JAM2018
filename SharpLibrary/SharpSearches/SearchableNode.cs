using System;
using System.Collections.Generic;
using System.Text;

namespace SharpSearches
{
	/// <summary>
	/// A node that can be used inside a local search algorithm, like A*
	/// </summary>
	public interface SearchableNode
	{

		/// <summary>
		/// If true, this means that the state has not been visited yet.
		/// Local search algorithms may use this information to speed up the search.
		/// However, this is just an hint
		/// </summary>
		bool IsOpen { get; set; }

		/// <summary>
		/// If true, this means that the state has already been dimmed as "fully analyzed"
		/// Local search algorithms may use this information to speed up the search.
		/// However, this is just an hint
		/// </summary>
		bool IsClosed { get; set; }

		/// <summary>
		/// Returns the list of state that are the successors of the current one
		/// </summary>
		IEnumerable<SearchableNode> Children { get; }

		SearchableNode Parent { get; set; }
	}
}
