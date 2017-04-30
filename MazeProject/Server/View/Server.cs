using Server.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Server
    {
        private TcpListener listener;
        private IClientHandler ch;
        public IController Controller;

        public Server(IClientHandler ch)
        {
            this.ch = ch;
            string ipAddress = ConfigurationManager.AppSettings["ip"];
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ipAddress), Convert.ToInt32(ConfigurationManager.AppSettings["port"]));
            listener = new TcpListener(ep);
        }


        public void Start()
        {
            listener.Start();
            while (true) 
            {
                try
                {
                    TcpClient client = listener.AcceptTcpClient();
                    ch.HandleClient(client);
                }
                catch (SocketException)
                {

                }
            }
        }

        public static void Main(string[] args)
        {
            ClientHandler ch = new ClientHandler();
            ServerController sc = new ServerController();      
            MazeOperations mo = new MazeOperations();
            ch.Controller = sc;
            sc.View = ch;
            sc.Model = mo;
            Server server = new Server(ch);
            server.Start();
        }
    }
}
