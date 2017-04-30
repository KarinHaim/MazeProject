using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// Class for holding the belonging a node in a search algorithm.
    /// </summary>
    /// <typeparam name="T"> the type of the objects in the searching problem </typeparam>
    public class State<T>
    {
        /// <summary>
        /// variable that holds the info of a node in a search problem
        /// </summary>
        public T Situation;
        /// <summary>
        /// the cost to reach to this state
        /// </summary>
        public double Cost;
        /// <summary>
        /// the "state" node that led to the current in the search algorithm
        /// </summary>
        public State<T> CameFrom;
        /// <summary>
        /// the total cost of states from the initial state until the current.
        /// </summary>
        public double TotalCost;

        /// <summary>
        /// a constructor.
        /// </summary>
        /// <param name="state"> the info of a node in a search problem</param>
        /// <param name="cost">the cost to reach to this state</param>
        private State(T state, double cost)
        {
            this.Situation = state;
            this.Cost = cost;
            this.CameFrom = null;
            this.TotalCost = 0;
        }

        /// <summary>
        /// a method for comparing other state to the current. The compare is done by comparing the Situations.
        /// </summary>
        /// <param name="s">other state</param>
        /// <returns>bool declaring if the states are equal</returns>
        public bool Equals(State<T> s)
        {
            return this.Situation.Equals(s.Situation);
        }

        /// <summary>
        /// override of the getHashCode method for defining the unique id for hashing of the state.
        /// </summary>
        /// <returns>hash code</returns>
        public override int GetHashCode()
        {
            return Situation.GetHashCode();
        }

        /// <summary>
        /// inner class that defines a pool of state and controlls getting states so there won't be
        /// duplicated states that contain the same info T.
        /// </summary>
        public static class StatePool
        {
            /// <summary>
            /// dictionary for mapping unique id to state
            /// </summary>
            private static Dictionary<int, State<T>> pool = new Dictionary<int, State<T>>();
            /// <summary>
            /// method for getting states.
            /// </summary>
            /// <param name="situation">the info of the state</param>
            /// <param name="cost">the cost to reach this state</param>
            /// <returns>a State </returns>
            public static State<T> GetState(T situation, double cost)
            {
                if (pool.ContainsKey(situation.GetHashCode()))
                    return pool[situation.GetHashCode()];  
                else
                    return new State<T>(situation, cost);
            }
        }
    }
}
