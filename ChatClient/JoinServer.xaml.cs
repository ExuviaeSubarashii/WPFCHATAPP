using Chat.Domain.Models;
using Chat.Infrastructure;
using Chat.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        private void back_Click(object sender, RoutedEventArgs e)
        {
            CreateServer CS=new CreateServer();
            this.Close();
            CS.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            CS.ShowDialog();
        }

        private async void joinServer_Click(object sender, RoutedEventArgs e)
        {
            User newUser = new User()
            {
                Username = AppMain.User.Username,
                Server = inviteLink.Text
            };
            Server newServer = new Server()
            {
                ServerName = inviteLink.Text,
                Channels = "Main",
                Usernames = AppMain.User.Username,
            };
            if (newServer.ServerName == null)
            {
                MessageBox.Show("Bir sunucu ismi ekleyiniz");
                return;
            }
            var newUserjson = JsonConvert.SerializeObject(newUser);
            var newUsercontent = new StringContent(newUserjson, Encoding.UTF8, "application/json");
            HttpResponseMessage message = await HttpHelper.httpClient.PutAsync($"/api/Users/AddServer", newUsercontent);

            var newtoServerjson = JsonConvert.SerializeObject(newServer);
            var newtoServerrcontent = new StringContent(newtoServerjson, Encoding.UTF8, "application/json");
            HttpResponseMessage message3 = await HttpHelper.httpClient.PostAsync($"/api/Users/AddNewServer", newtoServerrcontent);

            var newServerjson = JsonConvert.SerializeObject(newServer);
            var newServercontent = new StringContent(newServerjson, Encoding.UTF8, "application/json");
            HttpResponseMessage message2 = await HttpHelper.httpClient.PutAsync($"/api/Users/AddChannel", newServercontent);
            this.Close();
        }
    }
}
