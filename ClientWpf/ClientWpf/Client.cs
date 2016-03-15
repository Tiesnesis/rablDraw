using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace ClientWpf
{
    class Client
    {
        TcpClient socket;
        NetworkStream networkStream;
        StreamReader streamReader;
        StreamWriter streamWriter;
        String server = "localhost";
        String name = "DefaultClient";
        String receivedMessage;

        public String drawing;
        public String guess;

        public bool stayConnected;

        public Client(String server, String name)
        {
            this.server = server;
            this.name = name;
        }

        public void connect(string server) //connect to given server and initalize socket
        {
            try
            {
                this.socket = new TcpClient(server, 10);
            }
            catch
            {
                Console.WriteLine(
                "Failed to connect to server at {0}:999", "localhost");
                return;
            }
        }

        public void initStreams() //Open stream writers and readers
        {
            this.networkStream = this.socket.GetStream();
            this.streamReader = new StreamReader(this.networkStream);
            this.streamWriter = new StreamWriter(this.networkStream);
        }

        public void init() //initialise connection and start thread
        {
            connect(this.server);
            initStreams();
            this.stayConnected = true;
            Thread newThread = new Thread(new ThreadStart(Run));
            newThread.Start();
        }

        public void Run() //main communication thread
        {
            try
            {
                streamWriter.WriteLine(name);
                streamWriter.Flush();
                while (true)
                {
                    this.receivedMessage = streamReader.ReadLine();

                    if (this.receivedMessage.Contains("Tev"))
                    {
                        
                        streamWriter.WriteLine("######" + this.name + " " + this.drawing + new Random(100));
                        streamWriter.Flush();
                    }
                    else
                    {
                        this.drawing = receivedMessage;
                        streamWriter.WriteLine("######" + this.name + " " + this.guess + new Random(100));
                        streamWriter.Flush();
                    }

                }
                if (!stayConnected)
                {
                    streamWriter.WriteLine(this.name + "Disconnects");
                    streamWriter.Flush();
                    closeConnection();
                }
            }
            catch
            {
                Console.WriteLine("Exception reading from Server");
            }
        }

        
     
        public void closeConnection()
        {
            this.networkStream.Close();
       }

    }
}


