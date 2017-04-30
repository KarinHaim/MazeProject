using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class ClientHandler: IClientHandler, IView
    {
        public IController Controller;

        public ClientHandler(IController controller = null)
        {
            this.Controller = controller;
        }

        public void HandleClient(TcpClient client)
        {
            new Task(() =>
            {
                try
                {
                    ClientConnectionInfo connectionInfo = new ClientConnectionInfo(client);
                    while (connectionInfo.ShouldKeepConnection)
                    {
                        string command = connectionInfo.Reader.ReadLine();
                        if (command != "" && command != null)
                            Controller.HandleCommand(command, connectionInfo);
                    }
                    System.Threading.Thread.Sleep(1000);
                    connectionInfo.CloseConnection();
                }
                catch (Exception e)
                {
                    Exception e1 = e.InnerException;
                }
            }).Start();

            }

        public void SendReply(string reply, ClientConnectionInfo clientInfo)
        {
            new Task(() =>
            {
                try
                {
                    clientInfo.Writer.WriteLine(reply);
                }
                catch
                {

                }
            }).Start();
        }

       

    }
}
