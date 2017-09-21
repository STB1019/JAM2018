using System;
using System.Collections.Generic;
using System.Text;

namespace SharpSearches
{
	/// <summary>
	/// AStar algorithm states while searching for the goal.
	/// </summary>
	public enum State
	{
		/// <summary>
		/// The search algorithm is still searching for the goal.
		/// </summary>
		Searching,

		/// <summary>
		/// The search algorithm has found the goal.
		/// </summary>
		GoalFound,

		/// <summary>
		/// The search algorithm has failed to find a solution.
		/// </summary>
		Failed
	}

	interface SearchAlgorithm
	{
		State StartSearch(SearchableNode start, SearchableNode goal);

	}
}
