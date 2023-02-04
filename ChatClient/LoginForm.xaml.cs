using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
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
using Chat.Infrastructure;
using Newtonsoft.Json;
using Chat.Service;
using System.Net.Http.Json;

namespace ChatClient
{
    /// <summary>
    /// Interaction logic for LoginForm.xaml
    /// </summary>
    public partial class LoginForm : Window
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, RoutedEventArgs e)
        {
            AppMain.User = new Chat.Domain.Models.User();
            AppMain.User.Username = textBox1.Text;

            Chat.Domain.Models.User newUser = new Chat.Domain.Models.User()
            {
                Username = textBox1.Text,
                Password = textBox2.Text
            };
            var json = JsonConvert.SerializeObject(newUser);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage message = await HttpHelper.httpClient.PostAsJsonAsync($"/api/Users/Login", newUser);
            if (message.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show("Kullanici Bulunamadi");
                return;
            }
            this.Close();
        }
    }
}
