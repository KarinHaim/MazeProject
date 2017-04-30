using MazeLib;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.Controller
{
    /// <summary>
    /// class that defines a start game command.
    /// </summary>
    public class StartGameCommand : Command
    {
        /// <summary>
        /// a constructor.
        /// </summary>
        /// <param name="controller"> A controller which is repsonsible for implementing the command.</param>
        public StartGameCommand(ServerController controller): base(controller)
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
            catch
            {
                JObject errorMessage = new JObject();
                errorMessage["Error"] = "missing arguments";
                controller.View.SendReply(errorMessage.ToString(), clientInfo);
                return;
            }
            if (controller.MultiplePlayerMazes.ContainsKey(name))
            {
                JObject errorMessage = new JObject();
                errorMessage["Error"] = "game already started";
                controller.View.SendReply(errorMessage.ToString(), clientInfo);
                return;
            }
            Maze maze = controller.Model.GenerateMaze(name, rows, cols);
            controller.MultiplePlayerMazes.Add(name, maze);
            controller.CreatedGamesPlayers.Add(name, clientInfo);
        }
    }
}
