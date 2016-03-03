using System;
using System.Net.Sockets;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

public class User
{
    static TcpListener tcpListener = new TcpListener(10);
    public string message = "";
    public string name = "";
    public bool isConnected = false;

    public void setMessage(string message){
        this.message = message;
    }

    void Listeners()
    {
        Socket socketForClient = tcpListener.AcceptSocket();
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
                Thread.Sleep(1000);
            }
            streamReader.Close();
            networkStream.Close();
            streamWriter.Close();

        }
        socketForClient.Close();
        Console.WriteLine("Press any key to exit from server program");
        Console.ReadKey();
    }

    public static void Main()
    {
        //TcpListener tcpListener = new TcpListener(10);
        tcpListener.Start();
        Console.WriteLine("************This is Server program************");
        Console.WriteLine("How many clients are going to connect to this server?:");
        List<User> users = new List<User>();
        int numberOfClientsYouNeedToConnect = int.Parse(Console.ReadLine());
 
        for (int i = 0; i < numberOfClientsYouNeedToConnect; i++)
        {
            User user = new User();
            user.name = i+"";
            users.Add(user);
            Thread newThread = new Thread(new ThreadStart(user.Listeners));
            newThread.Start();
        }
        
        int userCounter = 0;
        int counter = 0;
        while(true){


            if (counter > 20000000)
            {
                counter = 0;
                Console.WriteLine("userCounter: " + userCounter);
                userCounter++;
            }


            if (userCounter == users.Count)
            {
                userCounter = 0;
            }

            foreach(User user in users){
                if (users[userCounter].name == user.name)
                {
                    user.message = user.name + " Tev jazime!";
                }
                else
                {
                    user.message = "Tagad zime " + users[userCounter].name;
                }

            }

            counter++;
            
           

        }

    }
}