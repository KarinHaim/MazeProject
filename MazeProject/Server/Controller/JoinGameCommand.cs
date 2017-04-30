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
    /// class that defines a join game command.
    /// </summary>
    public class JoinGameCommand : Command 
    {
        /// <summary>
        /// a constructor.
        /// </summary>
        /// <param name="controller"> A controller which is repsonsible for implementing the command.</param>
        public JoinGameCommand(ServerController controller) : base(controller)
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

            try
            {
                name = args[0];
            }
            catch // in case there is overflow in the index in the array
            {
                JObject errorMessage = new JObject();
                errorMessage["Error"] = "missing arguments";
                controller.View.SendReply(errorMessage.ToString(), clientInfo);
                return;
            }
            if (!controller.MultiplePlayerMazes.ContainsKey(name))
            {
                JObject errorMessage = new JObject();
                errorMessage["Error"] = String.Format("game {0} doesn't exist", name);
                controller.View.SendReply(errorMessage.ToString(), clientInfo);
                return;
            }
            ClientConnectionInfo startedGameClient = controller.CreatedGamesPlayers[name];
            MazeGame mazeGame = new MazeGame(controller.MultiplePlayerMazes[name], startedGameClient, clientInfo, controller.View, controller.Model);
            controller.CreatedGamesPlayers.Remove(name);
            controller.PlayersGamesBelonging[startedGameClient] = mazeGame;
            controller.PlayersGamesBelonging[clientInfo] = mazeGame;
        }
    }
}
