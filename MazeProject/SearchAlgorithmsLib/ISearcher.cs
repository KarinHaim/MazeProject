using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// class for defining the interface of the searching algorithm.
    /// </summary>
    /// <typeparam name="T">the type of the objects in the searching problem</typeparam>
    public interface ISearcher<T>
    {
        /// <summary>
        /// the method that makes the searches.
        /// </summary>
        /// <param name="searchable"> the given searching problem to search in</param>
        /// <returns> Solution object that includes the nodes of the route from the initial node to the goal.</returns>
        Solution<T> Search(ISearchable<T> searchable);
        /// <summary>
        /// function for evaluating the number of nodes evaluated while the search
        /// </summary>
        /// <returns>number of nodes </returns>
        int GetNumberOfNodesEvaluated();
    }
}
