using System;
using System.Collections.Generic;
using System.Text;

namespace SharpSearches
{
	public interface CostEvaluator<NODE> where NODE : SearchableNode
	{
		int EvaluateFromStart(NODE n, int parentCost);

		int EvaluateFromStart(NODE start, NODE n, int parentCost);
	}
}
