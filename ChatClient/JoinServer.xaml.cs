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
    /// Interaction logic for JoinServer.xaml
    /// </summary>
    public partial class JoinServer : Window
    {
        public JoinServer()
        {
            InitializeComponent();
        }

        private void link1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            inviteLink.Text = link1.Content.ToString();
        }

        private void link2_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            inviteLink.Text = link2.Content.ToString();
        }

        private void link3_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            inviteLink.Text = link3.Content.ToString();
        }
    }
}
