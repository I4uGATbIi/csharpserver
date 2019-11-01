using System;
using System.Collections.Generic;
using System.Text;

namespace ClientLib
{
    static class General
    {
        public static void InitializeClient()
        {
            ClientHandleData.InitializePackets();
            ClientTCP.InitializingNetworking();
        }
    }
}
