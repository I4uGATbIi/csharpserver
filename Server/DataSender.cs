using System;
using System.Collections.Generic;
using System.Text;
using GeneralLib;

namespace Server
{
    public enum ServerPackets
    {
        SWelcomeMessage = 1,
    }
    static class DataSender
    {
        public static void SendWelcomeMessage(int connectionID)
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteInteger((int)ServerPackets.SWelcomeMessage);
            buffer.WriteString("HELLO NEW CLIENT!PLEASE SUFFER!");
            ClientManager.SendDataTo(connectionID, buffer.ToArray());
            buffer.Dispose();
        }
    }
}
