using MazeLib;
using Newtonsoft.Json.Linq;
using SearchAlgorithmsLib;
using Server.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    /// <summary>
    /// class that defines a solve maze command.
    /// </summary>
    public class SolveMazeCommand : Command
    {
        /// <summary>
        /// a constructor.
        /// </summary>
        /// <param name="controller"> A controller which is repsonsible for implementing the command.</param>
        public SolveMazeCommand(ServerController controller) : base(controller)
        {
        }

        /// <summary>
        /// defines the execution of the command.
        /// </summary>
        /// <param name="args"> arguments for the command</param>
        /// <param name="clientInfo"> info of client the sent the command</param>
        public override void Execute(string[] args, ClientConnectionInfo clientInfo)
        {
            string name;
            int algorithm;

            try
            {
                name = args[0];
                algorithm = int.Parse(args[1]);
            }
            catch
            {
                JObject errorMessage = new JObject();
                errorMessage["Error"] = "missing arguments";
                controller.View.SendReply(errorMessage.ToString(), clientInfo);
                return;
            }

            Solution<Position> solution = null;
            if (controller.MazesSolutions.ContainsKey(name))
                solution = controller.MazesSolutions[name];
            else
            {
                if (!controller.SinglePlayerMazes.ContainsKey(name))
                {
                    JObject errorMessage = new JObject();
                    errorMessage["Error"] = String.Format("maze with name {0} doesn't exist", name);
                    controller.View.SendReply(errorMessage.ToString(), clientInfo);
                    return;
                }

                Maze maze = controller.SinglePlayerMazes[name];

                switch (algorithm)
                {
                    case 0:
                        solution = controller.Model.SolveMazeByBFS(maze);
                        break;
                    case 1:
                        solution = controller.Model.SolveMazeByDFS(maze);
                        break;
                }
                controller.MazesSolutions[name] = solution;
            }

            JObject entireSolution = new JObject();
            entireSolution["Name"] = name;
            entireSolution["Solution"] = controller.Model.CreateDirectionStringOfSolution(solution);
            entireSolution["NodesEvaluated"] = solution.NumberOfEvaluatedNodes;

            controller.View.SendReply(entireSolution.ToString(), clientInfo);
            clientInfo.ShouldKeepConnection = false;
        }
    }
}
