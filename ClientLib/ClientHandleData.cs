using GeneralLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientLib
{
    static class ClientHandleData
    {
        private static ByteBuffer clientBuffer;
        public delegate void Packet(byte[] data);
        public static Dictionary<int, Packet> packets = new Dictionary<int, Packet>();

        public static void InitializePackets()
        {
            packets.Add((int)ServerPackets.SWelcomeMsg, DataReciever.HandleWelcomeMsg);
        }

        public static void HandleData(byte[] data)
        {
            byte[] buffer = (byte[])data.Clone();
            int pLength = 0;

            if (clientBuffer == null) clientBuffer = new ByteBuffer();

            clientBuffer.WriteBytes(buffer);
            if (clientBuffer.Count == 0)
            {
                clientBuffer.Clear();
                return;
            }

            if (clientBuffer.Length >= 4)
            {
                pLength = clientBuffer.ReadInteger();
                if (pLength <= 0)
                {
                    clientBuffer.Clear();
                    return;
                }
            }

            while (pLength > 0 & pLength <= clientBuffer.Length - 4)
            {
                if (pLength <= clientBuffer.Length - 4)
                {
                    clientBuffer.ReadInteger();
                    data = clientBuffer.ReadBytes(pLength);
                    HandleDataPackets(data);
                }

                pLength = 0;
                if (clientBuffer.Length >= 4)
                {
                    pLength = clientBuffer.ReadInteger(false);
                    if (pLength <= 0)
                    {
                        clientBuffer.Clear();
                        return;
                    }
                }
            }

            if (pLength <= 1)
            {
                clientBuffer.Clear();
            }
        }

        private static void HandleDataPackets(byte[] data)
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteBytes(data);
            int packetID = buffer.ReadInteger();
            buffer.Dispose();
            if (packets.TryGetValue(packetID, out Packet packet))
            {
                packet.Invoke(data);
            }
        }
    }
}
