using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// A class implementing the Search method via BFS algorithm.
    /// </summary>
    /// <typeparam name="T">the type of the objects in the searching problem </typeparam>
    public class DFS<T> : Searcher<T>
    {
        /// <summary>
        /// implmentation of the Search method.
        /// </summary>
        /// <param name="searchable"> the searching problem to search in. </param>
        /// <returns> Solution object that includes the nodes of the route from the initial node to the goal.</returns>
        public override Solution<T> Search(ISearchable<T> searchable)
        {
            Stack<State<T>> stack = new Stack<State<T>>();
            closed = new HashSet<State<T>>();
            stack.Push(searchable.GetInitialState());
            
            while(stack.Count != 0)
            {
                State<T> node = stack.Pop();
                evaluatedNodes++;
                if (node.Equals(searchable.GetGoalState()))
                    return new Solution<T>(BackTrace(searchable), GetNumberOfNodesEvaluated());
                if (!closed.Contains(node))
                {
                    closed.Add(node);
                    foreach (State<T> adjacent in searchable.GetAllPossibleStates(node))
                    {
                        if (!closed.Contains(adjacent))
                        {
                            adjacent.CameFrom = node;
                            stack.Push(adjacent);
                        }
                    }
                }
            }
            return null;
        }
    }
}
