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
using System.Windows.Shapes;

namespace ChatClient
{
    /// <summary>
    /// Interaction logic for CreateServer.xaml
    /// </summary>
    public partial class CreateServer : Window
    {
        public CreateServer()
        {
            InitializeComponent();
        }

        private void joinAServer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            JoinServer JS=new JoinServer();
            JS.WindowStartupLocation= WindowStartupLocation.CenterScreen;
            JS.ShowDialog();
        }
    }
}
