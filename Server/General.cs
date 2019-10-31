using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    static class General
    {
        public static void InitializeServer()
        {
            ServerTCP.InitializeNetwork();
            Console.WriteLine("Server Started!");
        }
    }
}
