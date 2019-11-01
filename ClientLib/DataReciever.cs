using GeneralLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientLib
{
    public enum ServerPackets
    {
        SWelcomeMsg = 1,
    }

    static class DataReciever
    {
        public static void HandleWelcomeMsg(byte[] data)
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteBytes(data);
            int packetID = buffer.ReadInteger();
            string msg = buffer.ReadString();
            buffer.Dispose();

            //RETURN MSG SOMEWHERE
            DataSender.SendHelloServer();
        }
    }
}
