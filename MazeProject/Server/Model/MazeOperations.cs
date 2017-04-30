using MazeGeneratorLib;
using MazeLib;
using SearchAlgorithmsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model
{
    class MazeOperations: IModel
    {
        public Maze GenerateMaze(string name, int rows, int cols)
        {
            DFSMazeGenerator generator = new DFSMazeGenerator();
            Maze maze = generator.Generate(rows, cols);
            maze.Name = name;
            return maze;
        }

        public Solution<Position> SolveMazeByBFS(Maze maze)
        {
            BFS<Position> searcher = new BFS<Position>();
            return searcher.Search(new MazeSearchProblem(maze));
        }

        public Solution<Position> SolveMazeByDFS(Maze maze)
        {
            DFS<Position> searcher = new DFS<Position>();
            return searcher.Search(new MazeSearchProblem(maze));
        }

        public string CreateDirectionStringOfSolution(Solution<Position> solution)
        {
            string solutionSteps = "";
            List<State<Position>> states = solution.StateList;
            for (int i = 1; i < solution.StateList.Count; i++) // 1 because 
            {
                if (states[i].Situation.Row > states[i - 1].Situation.Row)
                    solutionSteps = solutionSteps + (int)Direction.Down;
                else
                {
                    if (states[i].Situation.Row < states[i - 1].Situation.Row)
                        solutionSteps = solutionSteps + (int)Direction.Up;
                    else
                    {
                        if (states[i].Situation.Col < states[i - 1].Situation.Col)
                            solutionSteps = solutionSteps + (int)Direction.Left;
                        else
                            solutionSteps = solutionSteps + (int)Direction.Right;
                    }
                }
            }
            return solutionSteps;
        }
    }
}
