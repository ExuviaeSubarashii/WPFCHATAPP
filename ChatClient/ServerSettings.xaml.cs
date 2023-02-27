using Chat.Domain.Dtos;
using Chat.Domain.Models;
using Chat.Infrastructure;
using Chat.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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
    /// Interaction logic for ServerSettings.xaml
    /// </summary>
    public partial class ServerSettings : Window
    {
        public ServerSettings()
        {
            InitializeComponent();
        }
        private async void ServerNameBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && ServerNameBox.Text != "")
            {
                //USERDAN GELEN VERIYI USERDTO ILE DEGISTIR
                var newServerDTO = AppMain.User.Server;
                User oldServer = new User()
                {
                    Username=AppMain.User.Username,
                    Server = ServerNameBox.Text
                };
                MessageBoxResult dialogresult = MessageBox.Show("THIS WILL BRING DEVASTATING RESULTS, ARE YOU SURE YOU WANT TO CHANGE SERVER NAME?", "WARNING", MessageBoxButton.YesNo);
                HttpResponseMessage message = new HttpResponseMessage();
                if (dialogresult==MessageBoxResult.Yes)
                {
                    
                message= await HttpHelper.httpClient.PutAsJsonAsync($"/api/Users/ChangeServerName/", newServerDTO);
                }
                //APIYE GONDER
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            ServerNameBox.Text = AppMain.User.Server;   
        }

        private void CloseSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
