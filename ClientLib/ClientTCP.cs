using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using GeneralLib;
using System.Threading;

namespace ClientLib
{
    static class ClientTCP
    {
        private static TcpClient clientSocket;
        private static NetworkStream myStream;
        private static byte[] recBuffer;

        public static void InitializingNetworking()
        {
            clientSocket = new TcpClient();
            clientSocket.ReceiveBufferSize = 4096;
            clientSocket.SendBufferSize = 4096;
            recBuffer = new byte[4096 * 2];
            clientSocket.BeginConnect("127.0.0.1", 1488, new AsyncCallback(ClientConnectCallback), clientSocket);
        }

        private static void ClientConnectCallback(IAsyncResult result)
        {
            clientSocket.EndConnect(result);
            if (clientSocket.Connected == false)
            {
                return;
            }
            else
            {
                clientSocket.NoDelay = true;
                myStream = clientSocket.GetStream();
                myStream.BeginRead(recBuffer, 0, 4096 * 2, RecieveCallback, null);
            }
        }

        private static void RecieveCallback(IAsyncResult result)
        {
            try
            {
                int length = myStream.EndRead(result);
                if (length <= 0)
                {
                    return;
                }

                byte[] newBytes = new byte[length];
                Array.Copy(recBuffer, newBytes, length);
                ClientHandleData.HandleData(newBytes);
                myStream.BeginRead(recBuffer, 0, 4096 * 2, RecieveCallback, null);
            }
            catch (Exception)
            {

                return;
            }
        }

        public static void SendData(byte[] data)
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteInteger((data.GetUpperBound(0) - data.GetLowerBound(0) + 1));
            buffer.WriteBytes(data);

            myStream.BeginWrite(buffer.ToArray(), 0, buffer.ToArray().Length, null, null);
            buffer.Dispose();
        }

        public static void Disconnect()
        {
            clientSocket.Close();
        }
    }
}
