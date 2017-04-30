using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConcurrentPriorityQueue;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// A class implementing the Search method via BFS algorithm.
    /// </summary>
    /// <typeparam name="T">the type of the objects in the searching problem </typeparam>
    public class BFS<T> : Searcher<T>
    {
        /// <summary>
        /// implmentation of the Search method.
        /// </summary>
        /// <param name="searchable"> the searching problem to search in. </param>
        /// <returns> Solution object that includes the nodes of the route from the initial node to the goal.</returns>
        public override Solution<T> Search(ISearchable<T> searchable)
        {
            ConcurrentPriorityQueue<State<T>, double> open = new ConcurrentPriorityQueue<State<T>, double>();
            closed = new HashSet<State<T>>();

            State<T> initialState = searchable.GetInitialState();
            open.Enqueue(initialState, 0);
            while (open.Count != 0)
            {
                State<T> node = open.Dequeue();
                evaluatedNodes++;
                if (node.Equals(searchable.GetGoalState()))
                    return new Solution<T>(BackTrace(searchable), GetNumberOfNodesEvaluated());

                closed.Add(node);
                foreach (State<T> adjacent in searchable.GetAllPossibleStates(node))
                {
                    if (!closed.Contains(adjacent) && !open.Contains(adjacent))
                    {
                        adjacent.CameFrom = node;
                        adjacent.TotalCost = node.TotalCost + adjacent.Cost;
                        open.Enqueue(adjacent, adjacent.TotalCost);
                    }
                    else
                    {
                        if ((node.TotalCost + adjacent.Cost) < adjacent.TotalCost)
                        {
                            adjacent.CameFrom = node;
                            adjacent.TotalCost = node.TotalCost + adjacent.Cost;

                            if (open.Contains(adjacent))
                            {
                                open.UpdatePriority(adjacent, adjacent.TotalCost);
                            }
                            else
                            {
                                closed.Remove(adjacent);
                                open.Enqueue(adjacent, adjacent.TotalCost);
                            }
                        }
                    }
                    
                }
            }

            return null;
        }
    }
}
