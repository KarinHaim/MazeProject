using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;
using MazeLib;

namespace Server
{
    class MazeSearchProblem : ISearchable<Position>
    {
        private Maze maze;
        private List<List<State<Position>>> states;

        public MazeSearchProblem(Maze maze)
        {
            this.maze = maze;
            this.states = new List<List<State<Position>>>();

            for (int i = 0; i < maze.Rows; i++)
            {
                List<State<Position>> rowStates = new List<State<Position>>();
                for (int j = 0; j < maze.Cols; j++)
                {
                    State<Position> state = State<Position>.StatePool.GetState(new Position(i, j), 1);
                    rowStates.Add(state);
                }
                states.Add(rowStates);
            }
        }

        public List<State<Position>> GetAllPossibleStates(State<Position> s)
        {
            List<State<Position>> validAdjacentStates = new List<State<Position>>();
            Position pos = s.Situation;
            if (pos.Row > 0)
                if (maze[pos.Row - 1, pos.Col] == CellType.Free)
                    validAdjacentStates.Add(states[pos.Row - 1][pos.Col]);
            if (pos.Col < this.maze.Cols - 1)
                if (maze[pos.Row, pos.Col + 1] == CellType.Free)
                    validAdjacentStates.Add(states[pos.Row][pos.Col + 1]);
            if (pos.Row < this.maze.Rows - 1)
                if (maze[pos.Row + 1, pos.Col] == CellType.Free)
                    validAdjacentStates.Add(states[pos.Row + 1][pos.Col]);
            if (pos.Col > 0)
                if (maze[pos.Row, pos.Col - 1] == CellType.Free)
                    validAdjacentStates.Add(states[pos.Row][pos.Col - 1]);
            return validAdjacentStates;
        }

        public State<Position> GetGoalState()
        {
            Position pos = maze.GoalPos;
            return states[pos.Row][pos.Col];
        }

        public State<Position> GetInitialState()
        {
            Position pos = maze.InitialPos;
            return states[pos.Row][pos.Col];
        }
    }
}
