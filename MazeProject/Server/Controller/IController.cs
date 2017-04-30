using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    /// <summary>
    /// defines the inteface for the controller
    /// </summary>
    public interface IController
    {
        /// <summary>
        /// function for handling received command
        /// </summary>
        /// <param name="command">received command</param>
        /// <param name="clientInfo"> info of client the sent the command</param>
        void HandleCommand(string command, ClientConnectionInfo clientInfo);
    }
}
