using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClientWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void connectButton_Click(object sender, RoutedEventArgs e)
        {
            Client client = new Client(serverIp.Text,userName.Text);
            client.init();
            Updater updater = new Updater(client);

        }

        class Updater
        {
            Client client;
            public Updater(Client client){
                this.client = client;
                Thread newThread = new Thread(new ThreadStart(Run));
                newThread.Start();
            }
            public void Run(){
                while(true)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        ((MainWindow)System.Windows.Application.Current.MainWindow).guess.Text = this.client.guess;
                        ((MainWindow)System.Windows.Application.Current.MainWindow).drawing.Text = this.client.drawing;
                    }));
                    Thread.Sleep(10);
                }
            }
        }
        


    }




}
