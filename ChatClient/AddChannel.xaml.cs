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
    /// Interaction logic for AddChannel.xaml
    /// </summary>
    public partial class AddChannel : Window
    {
        public AddChannel()
        {
            InitializeComponent();
        }

        private async void addChannelName_Click(object sender, RoutedEventArgs e)
        {
            Server newServer = new Server()
            {
                ServerName = AppMain.User.Server,
                Channels = channelName.Text,
                Usernames = AppMain.User.Username,
            };
            if (newServer.Channels == null)
            {
                MessageBox.Show("Bir kanal ismi ekleyiniz");
                return;
            }
            var newServerjson = JsonConvert.SerializeObject(newServer);
            var newServercontent = new StringContent(newServerjson, Encoding.UTF8, "application/json");
            HttpResponseMessage message2 = await HttpHelper.httpClient.PutAsync($"/api/Users/AddChannel", newServercontent);
        }
    }
}
