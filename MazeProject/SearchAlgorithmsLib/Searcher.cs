using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// abstract class that holds the functions and fields common to all search algorithms.
    /// </summary>
    /// <typeparam name="T">the type of the objects in the searching problem</typeparam>
    public abstract class Searcher<T> : ISearcher<T>
    {
        /// <summary>
        /// variable for holding the number of evaluted nodes
        /// </summary>
        protected int evaluatedNodes;
        /// <summary>
        /// set for holding the nodes "states" that the algorithm already passed.
        /// </summary>
        protected HashSet<State<T>> closed;

        /// <summary>
        /// the constructor.
        /// </summary>
        public Searcher()
        {
            evaluatedNodes = 0;
        }

        /// <summary>
        /// function for evaluating the number of nodes evaluated while the search
        /// </summary>
        /// <returns>number of nodes</returns>
        public int GetNumberOfNodesEvaluated()
        {
            return evaluatedNodes;
        }

        /// <summary>
        /// Method for generating the list of states that make up the solution of the search problem given
        /// the search problem
        /// </summary>
        /// <param name="searchable">the given search problem</param>
        /// <returns>list of states </returns>
        protected List<State<T>> BackTrace(ISearchable<T> searchable)
        {
            Stack<State<T>> stack = new Stack<State<T>>();
            State < T > current = searchable.GetGoalState();
            while (current != null) //insert all the states into a stack for reversing their order 
            {
                stack.Push(current);
                current = current.CameFrom;
            }

            List<State<T>> stateList = new List<State<T>>();
            while (stack.Count != 0)
            {
                stateList.Add(stack.Pop());
            }
            return stateList;
        }

        /// <summary>
        /// the method that makes the searches.
        /// </summary>
        /// <param name="searchable"> the given searching problem to search in</param>
        /// <returns>Solution object that includes the nodes of the route from the initial node to the goal.</returns>
        public abstract Solution<T> Search(ISearchable<T> searchable);
    }
}
