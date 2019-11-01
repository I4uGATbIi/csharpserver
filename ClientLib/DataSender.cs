using GeneralLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientLib
{
    public enum ClientPackets
    {
        CHelloServer = 1,
    }

    static class DataSender
    {
        public static void SendHelloServer()
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteInteger((int)ClientPackets.CHelloServer);
            buffer.WriteString("HI!I'M READY TO SUFFER");
            ClientTCP.SendData(buffer.ToArray());
            buffer.Dispose();
        }
    }
}
