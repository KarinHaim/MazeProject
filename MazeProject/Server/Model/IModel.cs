using MazeLib;
using SearchAlgorithmsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model
{
    public interface IModel
    {
        Maze GenerateMaze(string name, int rows, int cols);
        Solution<Position> SolveMazeByDFS(Maze maze);
        Solution<Position> SolveMazeByBFS(Maze maze);
        string CreateDirectionStringOfSolution(Solution<Position> solution);
    }
}
