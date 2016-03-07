using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace Server
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BackBone mainBackbone = new BackBone();
        public MainWindow()
        {
            
            InitializeComponent();
            mainBackbone.NumberOfClientsYouNeedToConnect = 2;
            
        }

        private void connectButton_Click(object sender, RoutedEventArgs e)
        {
            mainBackbone.PlayGame();
        }

        public void addLog(string logString)
        {
            LogBox.Text = logString;
        }
    }
}
