using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using SearchAlgorithmsLib;
using Server.Model;
using System.Net.Sockets;
using Server.Controller;

namespace Server
{
    public class ServerController: IController
    {
        public Dictionary<string, Maze> SinglePlayerMazes;
        public Dictionary<string, Maze> MultiplePlayerMazes;
        public Dictionary<string, Solution<Position>> MazesSolutions;
        public Dictionary<string, ClientConnectionInfo> CreatedGamesPlayers;
        public Dictionary<ClientConnectionInfo, MazeGame> PlayersGamesBelonging;
        private Dictionary<string, ICommand> commands;
        public IModel Model;
        public IView View;

        public ServerController(IModel model = null, IView view = null)
        {
            SinglePlayerMazes = new Dictionary<string, Maze>();
            MultiplePlayerMazes = new Dictionary<string, Maze>();
            MazesSolutions = new Dictionary<string, Solution<Position>>();
            CreatedGamesPlayers = new Dictionary<string, ClientConnectionInfo>();
            PlayersGamesBelonging = new Dictionary<ClientConnectionInfo, MazeGame>();
            this.Model = model;
            this.View = view;

            commands = new Dictionary<string, ICommand>();
            commands.Add("generate", new GenerateMazeCommand(this));
            commands.Add("solve", new SolveMazeCommand(this));
            commands.Add("start", new StartGameCommand(this));
            commands.Add("list", new JoinableGamesListCommand(this));
            commands.Add("join", new JoinGameCommand(this));
            commands.Add("play", new PlayGameCommand(this));
            commands.Add("close", new CloseGameCommand(this));
        }

        public void HandleCommand(string commandLine, ClientConnectionInfo clientInfo)
        {
            string[] arr = commandLine.Split(' ');
            string command = arr[0];
            ICommand commandExecution;
            if (!commands.TryGetValue(command, out commandExecution))
            {
                //to do
            }
            string[] args = arr.Skip(1).ToArray();
            commandExecution.Execute(args, clientInfo);
        }

        

    }
}
