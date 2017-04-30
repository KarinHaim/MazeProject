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
    /// class that defines a joinable games list command.
    /// </summary>
    public class JoinableGamesListCommand: Command
    {
        /// <summary>
        /// a constructor.
        /// </summary>
        /// <param name="controller"> A controller which is repsonsible for implementing the command.</param>
        public JoinableGamesListCommand(ServerController controller) : base(controller)
        {

        }

        /// <summary>
        /// defines the execution of the command.
        /// </summary>
        /// <param name="args"> arguments for the command</param>
        /// <param name="clientInfo"> info of client the sent the command</param>
        public override void Execute(string[] args, ClientConnectionInfo clientInfo)
        {
            JArray gamesList = new JArray();
            foreach (KeyValuePair<string, ClientConnectionInfo> game in controller.CreatedGamesPlayers)
            {
                gamesList.Add(game.Key);
            }

            controller.View.SendReply(gamesList.ToString(), clientInfo);
            clientInfo.ShouldKeepConnection = false;
        }
    }
}
