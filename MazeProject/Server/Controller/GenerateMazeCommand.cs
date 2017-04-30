using MazeLib;
using Newtonsoft.Json.Linq;
using Server.Controller;
using Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    /// <summary>
    /// class that defines a generate maze command.
    /// </summary>
    public class GenerateMazeCommand : Command
    {
        /// <summary>
        /// a constructor.
        /// </summary>
        /// <param name="controller"> A controller which is repsonsible for implementing the command.</param>
        public GenerateMazeCommand(ServerController controller) : base(controller)
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
            int rows, cols;
            try
            {
                name = args[0];
                rows = int.Parse(args[1]);
                cols = int.Parse(args[2]);
            }
            catch // in case there is overflow in the index in the array
            {
                JObject errorMessage = new JObject();
                errorMessage["Error"] = "missing arguments";
                controller.View.SendReply(errorMessage.ToString(), clientInfo);
                return;
            }
            if (controller.SinglePlayerMazes.ContainsKey(name))
            {
                controller.SinglePlayerMazes.Remove(name);
                if (controller.MazesSolutions.ContainsKey(name))
                    controller.MazesSolutions.Remove(name);
            }
            Maze maze = controller.Model.GenerateMaze(name, rows, cols);
            controller.SinglePlayerMazes.Add(name, maze);
            string reply = maze.ToJSON();
            controller.View.SendReply(reply, clientInfo);
            clientInfo.ShouldKeepConnection = false;
        }
    }
}
