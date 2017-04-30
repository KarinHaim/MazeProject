using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// defines the interface of the searching problem.
    /// </summary>
    /// <typeparam name="T">the type of the objects in the searching problem</typeparam>
    public interface ISearchable<T>
    {
        /// <summary>
        /// function for receiving the initial state of the sarching problem.
        /// </summary>
        /// <returns> State of type of objects in the searching problem. </returns>
        State<T> GetInitialState();
        /// <summary>
        /// function for receiving the goal state of the sarching problem.
        /// </summary>
        /// <returns>State of type of objects in the searching problem.</returns>
        State<T> GetGoalState();
        /// <summary>
        /// function for receiving all the states the state given can go to.
        /// </summary>
        /// <param name="s"> the given state</param>
        /// <returns>list of possible states</returns>
        List<State<T>> GetAllPossibleStates(State<T> s);
    }
}
