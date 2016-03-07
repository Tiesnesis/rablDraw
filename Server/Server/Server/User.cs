using System;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
    class User
    {
        static TcpListener tcpListener = new TcpListener(10);
        string message = "";
        string answer = "";
        string name = "";
        bool isConnected = false;
        Socket socketForClient;

        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        public string Answer
        {
            get { return answer; }
            set { answer = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }


        public void setMessage(string message)
        {
            this.message = message;
        }

        public void Listeners()
        {

            if (socketForClient.Connected)
            {
                Console.WriteLine("Client:" + socketForClient.RemoteEndPoint + " now connected to server.");
                isConnected = true;
                NetworkStream networkStream = new NetworkStream(socketForClient);
                System.IO.StreamWriter streamWriter =
                new System.IO.StreamWriter(networkStream);
                System.IO.StreamReader streamReader =
                new System.IO.StreamReader(networkStream);

                name = streamReader.ReadLine();
                while (true)
                {
                    streamWriter.WriteLine(message);
                    streamWriter.Flush();
                    answer = streamReader.ReadLine();
                    Thread.Sleep(1000);
                }
                streamReader.Close();
                networkStream.Close();
                streamWriter.Close();

            }

        }

        public void startUser()
        {
            socketForClient = tcpListener.AcceptSocket();
        }

        public void stopUser()
        {
            socketForClient.Close();
        }

        public static void startListener()
        {
            tcpListener.Start();
        }
    }
}
