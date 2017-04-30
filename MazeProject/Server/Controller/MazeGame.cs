using MazeLib;
using Newtonsoft.Json.Linq;
using Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.Controller
{
    /// <summary>
    /// a class the describes multiplayer game between two players.
    /// </summary>
    public class MazeGame
    {
        /// <summary>
        /// info of first player
        /// </summary>
        public ClientConnectionInfo Player1;
        /// <summary>
        /// info of second player
        /// </summary>
        public ClientConnectionInfo Player2;
        /// <summary>
        /// the maze game the players play
        /// </summary>
        private Maze maze;
        /// <summary>
        /// the view for interacting with the clients
        /// </summary>
        private IView view;
        /// <summary>
        /// the model for the buisness logic of the game
        /// </summary>
        private IModel model;

        /// <summary>
        /// the constructor.
        /// </summary>
        /// <param name="maze">the game</param>
        /// <param name="player1">the first player</param>
        /// <param name="player2">the second player</param>
        /// <param name="view">the view</param>
        /// <param name="model">the model</param>
        public MazeGame(Maze maze, ClientConnectionInfo player1, ClientConnectionInfo player2, IView view, IModel model) 
        {
            this.maze = maze;
            this.Player1 = player1;
            this.Player2 = player2;
            this.view = view;
            this.model = model;

            SendGameStartedMessage();
        }

        /// <summary>
        /// in charge of sending the clients message about the game beggining
        /// </summary>
        private void SendGameStartedMessage()
        {
            view.SendReply(maze.ToJSON(), Player1);
            view.SendReply(maze.ToJSON(), Player2);
        }

        /// <summary>
        /// in charge of sending the clients message about a movement in the game.
        /// </summary>
        /// <param name="move">the move</param>
        /// <param name="clientInfo">the client info who moved</param>
        public void NotifyOfMove(string move, ClientConnectionInfo clientInfo)
        {
            Array directions = Enum.GetValues(typeof(Direction));
            int i;
            for (i = 0; i < directions.Length; i++)
            {
                if (directions.GetValue(i).ToString().Equals(move))
                    break;
            }
            if (i == 4) // there was not found appropriate value in the Direction enum
            {
                JObject errorMessage = new JObject();
                errorMessage["Error"] = "invalid direction key word";
                view.SendReply(errorMessage.ToString(), clientInfo);
                return;
            }

            JObject movement = new JObject();
            movement["Name"] = maze.Name;
            movement["Direction"] = move;

            if (clientInfo.Equals(Player1))
                view.SendReply(movement.ToString(), Player2);
            else
                view.SendReply(movement.ToString(), Player1);
        }

        /// <summary>
        /// in charge of sending the clients messages about the finish of the game.
        /// </summary>
        /// <param name="client">the reuqesting to close game client info</param>
        public void NotifyGameFinish(ClientConnectionInfo client)
        {
            JObject finishGame = new JObject();
            if (client.Equals(Player1))
                view.SendReply(finishGame.ToString(), Player2);
            else
                view.SendReply(finishGame.ToString(), Player1);

            Player1.ShouldKeepConnection = false;
            Player2.ShouldKeepConnection = false;
        }
    }
}
