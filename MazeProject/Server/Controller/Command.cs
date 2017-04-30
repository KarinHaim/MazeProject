using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.Controller
{
    /// <summary>
    /// an abstract class that contains a field that is common to all Commands.
    /// </summary>
    public abstract class Command : ICommand
    {
        /// <summary>
        /// A controller which is repsonsible for implementing the command.
        /// </summary>
        protected ServerController controller;

        /// <summary>
        /// a constrcutor.
        /// </summary>
        /// <param name="controller">A controller which is repsonsible for implementing the command.</param>
        public Command(ServerController controller)
        {
            this.controller = controller;
        }

        /// <summary>
        /// defines the execution of the command.
        /// </summary>
        /// <param name="args"> arguments for the command</param>
        /// <param name="clientInfo"> info of client the sent the command</param>
        public abstract void Execute(string[] args, ClientConnectionInfo clientInfo);
    }
}
