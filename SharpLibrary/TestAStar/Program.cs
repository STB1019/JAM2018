using SharpSearches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstarVisualizer
{
	internal class GridCostEvaluator : CostEvaluator<GridNode>
	{
		public int EvaluateFromStart(GridNode n, int parentCost)
		{
			return parentCost + 1;
		}

		public int EvaluateFromStart(GridNode start, GridNode n, int parentCost)
		{
			return parentCost + 1;
		}
	}

	internal class GridHeuristicEvaluator : HeuristicEvaluator<GridNode>
	{
		public int EvaluateToGoal(GridNode n, int parentEvaluation)
		{
			throw new NotImplementedException();
		}

		public int EvaluateToGoal(GridNode n, GridNode goal, int parentEvaluation)
		{
			//Manhattan distance
			return Math.Abs(n.X - goal.X) + Math.Abs(n.Y - goal.Y);
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			var grid = new Grid2D(40, 40, 15, 0, 0, 39, 39);
			var astar = new AStar<GridNode>(new GridCostEvaluator(), new GridHeuristicEvaluator());

			var result = astar.StartSearch(grid.Start, grid.Goal);

			Console.WriteLine("We have " + result);

			var output = grid.Print(astar.GetPath());

			Console.WriteLine(output);
			Console.ReadLine();
		}
	}
}
