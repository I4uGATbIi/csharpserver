using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;

namespace Server
{
    public class Client
    {
        public int connectionID;
        public TcpClient socket;
        public NetworkStream Stream;
        private byte[] recBuffer;
        public ByteBuffer buffer;

        public void Start()
        {
            socket.SendBufferSize = 4096;
            socket.ReceiveBufferSize = 4096;
            Stream = socket.GetStream();

            Stream.BeginRead(recBuffer, 0, socket.ReceiveBufferSize, OnRecieveData, null);
            Console.WriteLine($"Incoming connection from {socket.Client.RemoteEndPoint.ToString()}");
        }

        private void OnRecieveData(IAsyncResult result)
        {
            try
            {
                int length = Stream.EndRead(result);
                if (length <= 0)
                {
                    CloseConnection();
                    return;
                }

                byte[] newBytes = new byte[length];
                Array.Copy(recBuffer, newBytes, length);
                ServerHandleData.HandleData(connectionID, newBytes);
                Stream.BeginRead(recBuffer, 0, socket.ReceiveBufferSize, OnRecieveData, null);

            }
            catch (Exception)
            {
                CloseConnection();
                return;
            }
        }

        private void CloseConnection()
        {
            Console.WriteLine($"Connection from {socket.Client.RemoteEndPoint.ToString()} has been terminated");
            socket.Close();
        }
    }
}
