using System;
using System.Collections.Generic;
using System.Text;

namespace SharpSearches
{
	/// <summary>
	/// Implementing this class means that the class is an heuristic.
	/// An heurstic allows you to evaluate a node
	/// </summary>
	public interface HeuristicEvaluator<NODE> where NODE : SearchableNode
	{
		int EvaluateToGoal(NODE n, int parentEvaluation);

		int EvaluateToGoal(NODE n, NODE goal, int parentEvaluation);
	}
}
