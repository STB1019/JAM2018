/*
 * This software has been heavily inspired by Josh Baldwin's one.
 * 
 * astar-1.0.cs may be freely distributed under the MIT license.
 * 
 * Copyright (c) 2013 Josh Baldwin https://github.com/jbaldwin/astar.cs
 * 
 * Permission is hereby granted, free of charge, to any person
 * obtaining a copy of this software and associated documentation 
 * files (the "Software"), to deal in the Software without 
 * restriction, including without limitation the rights to use, 
 * copy, modify, merge, publish, distribute, sublicense, and/or sell 
 * copies of the Software, and to permit persons to whom the 
 * Software is furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be 
 * included in all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, 
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES 
 * OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND 
 * NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT 
 * HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, 
 * WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR 
 * OTHER DEALINGS IN THE SOFTWARE.
 */

using SharpUtilities;
using System;
using System.Collections.Generic;

namespace SharpSearches
{

	/// <summary>
	/// System.Collections.Generic.SortedList by default does not allow duplicate items.
	/// Since items are keyed by TotalCost there can be duplicate entries per key.
	/// </summary>
	internal class DuplicateComparer : IComparer<int>
	{
		public int Compare(int x, int y)
		{
			return (x <= y) ? -1 : 1;
		}
	}

	/// <summary>
	/// How A* will compute the total cost of a node, given the movement cost and the heuristic cost
	/// </summary>
	/// 
	/// <example>
	/// See project TestAstar for a complete example
	/// </example>
	/// 
	/// <param name="cost">the cost from going from state "start" till the "current" one</param>
	/// <param name="heuristic">the estimate cost from going from state "current" to the "goal" one</param>
	/// <returns>The total cost</returns>
	public delegate int BiFunction(int cost, int heuristic);

	/// <summary>
	/// Interface to setup and run the AStar algorithm.
	/// </summary>
	public class AStar<NODE> : SearchAlgorithm where NODE : SearchableNode
	{
		/// <summary>
		/// The open list.
		/// </summary>
		private SortedList<int, Triple<NODE, int, int>> openList;

		/// <summary>
		/// The closed list.
		/// </summary>
		private SortedList<int, Triple<NODE, int, int>> closedList;

		/// <summary>
		/// The current node.
		/// </summary>
		private NODE current;

		/// <summary>
		/// The node where we have started our search
		/// </summary>
		public NODE Start { get; private set; }

		/// <summary>
		/// The goal node.
		/// </summary>
		public NODE Goal { get; private set; }

		/// <summary>
		/// Gets the current amount of steps that the algorithm has performed.
		/// </summary>
		public int Steps { get; private set; }

		/// <summary>
		/// Gets the current node that the AStar algorithm is at.
		/// </summary>
		public NODE CurrentNode { get { return current; } }

		public CostEvaluator<NODE> CostFunction { get; private set; }

		public HeuristicEvaluator<NODE> Heuristic { get; private set; }

		public BiFunction TotalCostComputer { get; private set; }

		private static readonly BiFunction SIMPLE_A_STAR_TOTAL_COST = delegate (int g, int h) { return g + h; };

		/// <summary>
		/// Creates a new AStar algorithm instance with the provided start and goal nodes.
		/// </summary>
		/// <param name="g">a function that, given a particular node, computes what is the cost for reaching such node</param>
		/// <param name="h">a function that, given a particular node, computes an estimate for reaching the goal state</param>
		/// <param name="totalCostComputer">A function that, given the output of both "g" and "h", retrieve the total cost of the state</param>
		public AStar(CostEvaluator<NODE> g, HeuristicEvaluator<NODE> h, BiFunction totalCostComputer)
		{
			var duplicateComparer = new DuplicateComparer();

			this.Heuristic = h;
			this.CostFunction = g;
			this.TotalCostComputer = totalCostComputer;
			openList = new SortedList<int, Triple<NODE, int, int>>(duplicateComparer);
			closedList = new SortedList<int, Triple<NODE, int, int>>(duplicateComparer);
		}

		public AStar(CostEvaluator<NODE> g, HeuristicEvaluator<NODE> h) : this(g, h, SIMPLE_A_STAR_TOTAL_COST)
		{

		}

		/// <summary>
		/// Resets the AStar algorithm with the newly specified start node and goal node.
		/// In this way you can reuse this class if you're doing multiple searches
		/// </summary>
		/// <param name="start">The starting node for the AStar algorithm.</param>
		/// <param name="goal">The goal node for the AStar algorithm.</param>
		public void Reset(NODE start, NODE goal)
		{
			openList.Clear();
			closedList.Clear();
			current = start;
			this.Start = start;
			this.Goal = goal;

			int g = this.CostFunction.EvaluateFromStart(this.Start, current, 0);
			int h = this.Heuristic.EvaluateToGoal(current, this.Goal, int.MaxValue);
			openList.Add(this.TotalCostComputer(g, h), new Triple<NODE, int, int> (current, g, h));

			current.IsOpen = true;
		}

		/// <summary>
		/// Steps the AStar algorithm forward until it either fails or finds the goal node.
		/// </summary>
		/// <param name="start">The starting node for the AStar algorithm.</param>
		/// <param name="goal">The goal node for the AStar algorithm.</param>
		/// <returns>Returns the state the algorithm finished in, Failed or GoalFound.</returns>
		public State StartSearch(SearchableNode start, SearchableNode goal)
		{
			Reset((NODE)start, (NODE)goal);
			// Continue searching until either failure or the goal node has been found.
			while (true)
			{
				State s = AnalyzeNextState();
				if (s != State.Searching)
					return s;
			}
		}

		/// <summary>
		/// Moves the AStar algorithm forward one step.
		/// </summary>
		/// <returns>Returns the state the alorithm is in after the step, either Failed, GoalFound or still Searching.</returns>
		protected State AnalyzeNextState()
		{
			int currentG;
			int currentH;
			int childG;
			int childH;

			this.Steps++;
			while (true)
			{
				// There are no more nodes to search, return failure.
				if (openList.Count == 0)
				{
					return State.Failed;
				}

				// Check the next best node in the graph by TotalCost.
				//Pop from the open list the state with the best total cost
				Triple<NODE, int, int> triple = openList.Values[0];
				this.current = triple.X;
				currentG = triple.Y;
				currentH = triple.Z;
				openList.RemoveAt(0);

				// This node has already been searched, check the next one.
				if (current.IsClosed)
				{
					continue;
				}

				// An unsearched node has been found, search it.
				break;
			}

			// Remove from the open list and place on the closed list 
			// since this node is now being searched.
			current.IsOpen = false;

			this.closedList.Add(this.TotalCostComputer(currentG, currentH), new Triple<NODE, int, int>(current, currentG, currentH));

			current.IsClosed = true;

			// Found the goal, stop searching.
			if (current.Equals(this.Goal))
			{
				return State.GoalFound;
			}

			// Node was not the goal so add all children nodes to the open list.
			// Each child needs to have its movement cost set and estimated cost.
			foreach (NODE child in current.Children)
			{
				// If the child has already been searched (closed list) or is on
				// the open list to be searched then do not modify its movement cost
				// or estimated cost since they have already been set previously.
				if (child.IsOpen || child.IsClosed)
				{
					continue;
				}

				child.Parent = current;

				childG = this.CostFunction.EvaluateFromStart(this.Start, child, currentG);
				childH = this.Heuristic.EvaluateToGoal(child, this.Goal, currentH);

				openList.Add(this.TotalCostComputer(childG, childH), new Triple<NODE, int, int>(child, childG, childH));
				child.IsOpen = true;
			}

			// This step did not find the goal so return status of still searching.
			return State.Searching;
		}

		/// <summary>
		/// Gets the path of the last solution of the AStar algorithm.
		/// Will return a partial path if the algorithm has not finished yet.
		/// </summary>
		/// <returns>Returns null if the algorithm has never been run.</returns>
		public IEnumerable<NODE> GetPath()
		{
			if (current != null)
			{
				var next = current;
				var path = new List<NODE>();
				while (next != null)
				{
					path.Add(next);
					next = (NODE)next.Parent;
				}
				path.Reverse();
				return path.ToArray();
			}
			return null;
		}
	}
}