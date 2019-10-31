using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    public enum ClientPackets
    {
        CHelloServer = 1,
    }

    class DataReciever
    {
        public static void HandleHelloServer(int connectionID, byte[] data)
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteBytes(data);
            int packetID = buffer.ReadInteger();
            string msg = buffer.ReadString();
            buffer.Dispose();
            Console.WriteLine(msg);
        }
    }
}
