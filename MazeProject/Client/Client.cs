using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    /// <summary>
    /// A class that represents a Client of the app.
    /// </summary>
    class Client
    {
        /// <summary>
        /// ip address variable
        /// </summary>
        private IPEndPoint ep;
        /// <summary>
        /// tcp endpoint variable
        /// </summary>
        private TcpClient client;
        /// <summary>
        /// stream of the socket variable
        /// </summary>
        private NetworkStream stream;
        /// <summary>
        /// variable for reading from stream
        /// </summary>
        private StreamReader reader;
        /// <summary>
        /// variable for writing to stream
        /// </summary>
        private StreamWriter writer;

        /// <summary>
        /// the class constructor
        /// </summary>
        public Client()
        {
            string ipAddress = ConfigurationManager.AppSettings["ip"];
            ep = new IPEndPoint(IPAddress.Parse(ipAddress), Convert.ToInt32(ConfigurationManager.AppSettings["port"]));
            EstablishConnection();
        }

        /// <summary>
        /// a function that creates the socket and the stream for communication.
        /// </summary>
        private void EstablishConnection()
        {
            try
            {
                this.client = new TcpClient();
                this.client.Connect(this.ep);
                this.client.NoDelay = true;
                this.stream = client.GetStream();
                this.reader = new StreamReader(this.stream);
                this.writer = new StreamWriter(this.stream);
                this.writer.AutoFlush = true;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// a method that creates two threads, one for receiving commands from the user and send them and one for receiving
        /// output via socket and display it to the user.
        /// </summary>
        public void Start()
        {
             Thread sendCommandsThread = new Thread(() =>
            {
                 while (true)
                 {
                    string command = Console.ReadLine();
                    try
                    {
                        writer.WriteLine(""); // send an empty message for checking if the stream is closed
                        writer.WriteLine(command);
                    }
                    catch(IOException exception) // in case the stream is closed, reconnect to the server
                    {
                        EstablishConnection();
                        writer.WriteLine(command);
                    }
                 }

              });
            sendCommandsThread.Start();

            Thread receiveResultThread = new Thread(() =>
                {
                    while (true)
                    {
                        try
                        {
                            if (!reader.EndOfStream) // read all the output lines from the socket
                                while (!reader.EndOfStream)
                                {
                                    Console.WriteLine(reader.ReadLine());
                                }
                        }
                        catch (IOException exception) 
                        {
                            // in case the stream is closed wait for the second thread to realize it and generate new socket
                            System.Threading.Thread.Sleep(1000);
                        }    
                    }

                });
            receiveResultThread.Start();
                
            //}
        }

        /// <summary>
        /// The entry point of the client.
        /// </summary>
        /// <param name="args"> A list of command line arguments </param>
        static void Main(string[] args)
        {
            Client client = new Client();
            client.Start();
        }





    }
}
