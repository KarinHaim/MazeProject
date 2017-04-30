using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// class that holds the list of states that make up solution to the search problem and the number
    /// of evaluated nodes.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Solution<T>
    {
        /// <summary>
        /// the list of states variable
        /// </summary>
        public List<State<T>> StateList;
        /// <summary>
        /// the number of evaluted nodes variable
        /// </summary>
        public int NumberOfEvaluatedNodes;

        /// <summary>
        /// a constructor.
        /// </summary>
        /// <param name="states"> the list of states </param>
        /// <param name="evaluatedNodes">the number of evaluated nodes</param>
        public Solution(List<State<T>> states, int evaluatedNodes)
        {
            this.StateList = states;
            this.NumberOfEvaluatedNodes = evaluatedNodes;
        }
    }
}
