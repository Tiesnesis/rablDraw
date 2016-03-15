using System;
using System.Threading;
using System.Collections.Generic;

namespace Server
{
    class BackBone
    {
        int userCounter = 0;
        List<User> users = new List<User>();
        int numberOfClientsYouNeedToConnect;

        public int NumberOfClientsYouNeedToConnect
        {
            get { return numberOfClientsYouNeedToConnect; }
            set { numberOfClientsYouNeedToConnect = value; }
        }

        public void PlayGame()
        {
            User.startListener();


            for (int i = 0; i < numberOfClientsYouNeedToConnect; i++)
            {
                User user = new User();
                user.Name = i + "";
                user.startUser();
                users.Add(user);
                Thread newThread = new Thread(new ThreadStart(user.Listeners));
                newThread.Start();
            }


            int counter = 0;
            while (true)
            {


                if (counter > 20000000)
                {
                    counter = 0;
                   // ((MainWindow)System.Windows.Application.Current.MainWindow).addLog("userCounter: " + userCounter);
                    foreach (User user in users)
                    {
                        Console.WriteLine(user.Name + " : " + user.Answer);

                    }
                    userCounter++;
                }

                if (userCounter == users.Count)
                {
                    userCounter = 0;
                }

                foreach (User user in users)
                {
                    if (users[userCounter].Name == user.Name)
                    {
                        user.Message = user.Name + " Tev jazime!";
                    }
                    else
                    {
                        user.Message = "Tagad zime " + users[userCounter].Name + ", kas ir uzzimejis: " + users[userCounter].Answer;
                    }

                }

                counter++;



            }
        }
    }
}
