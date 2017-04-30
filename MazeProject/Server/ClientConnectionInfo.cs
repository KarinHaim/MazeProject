using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class ClientConnectionInfo
    {
        public TcpClient TcpClient;
        public NetworkStream Stream;
        public StreamReader Reader;
        public StreamWriter Writer;
        public bool ShouldKeepConnection;

       /* public ClientConnectionInfo(TcpClient tcpClient, NetworkStream stream, StreamReader reader, StreamWriter writer)
        {
            this.TcpClient = tcpClient;
            this.Stream = stream;
            this.Reader = reader;
            this.Writer = writer;
        }*/

        public ClientConnectionInfo(TcpClient tcpClient)
        {
            this.TcpClient = tcpClient;
            this.Stream = this.TcpClient.GetStream();
            this.Reader = new StreamReader(this.Stream);
            this.Writer = new StreamWriter(this.Stream);
            this.Writer.AutoFlush = true;
            this.ShouldKeepConnection = true;
        }

        public void CloseConnection()
        {
            this.Reader.Dispose();
            this.Writer.Dispose();
            this.Stream.Dispose();
            this.TcpClient.Close();
        }
    }
}
