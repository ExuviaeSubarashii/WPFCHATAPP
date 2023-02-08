using Chat.Domain.Models;
using Chat.Infrastructure;
using Chat.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ChatClient
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
        ChatContext _CP = new ChatContext();
        private void Form1_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        string[] sw;
        Button button = new Button();
        private async void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && textBox1.Text != "")
            {
                Message newMessage = new Message()
                {
                    Message1 = textBox1.Text,
                    SenderName = AppMain.User.Username,
                    SenderTime = DateTime.UtcNow,
                    Server = servername.Content.ToString(),
                    Channel = channelname.Content.ToString()
                };
                var json = JsonConvert.SerializeObject(newMessage);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                await HttpHelper.httpClient.PostAsync($"/api/Messages/SendMessage", content).Result.Content.ReadAsStringAsync();
                var query = _CP.Messages.Where(x => x.Server == servername.Content.ToString() && x.Channel == channelname.Content.ToString()).ToList();
                dataGridView1.ItemsSource = query.ToList();
                textBox1.Clear();
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            LoginForm LF = new LoginForm();
            LF.Topmost= true;
            LF.ShowDialog();
            System.Windows.Threading.DispatcherTimer timer1 =new System.Windows.Threading.DispatcherTimer();
            timer1.Tick += Timer1_Tick;
            timer1.Interval = new TimeSpan(0,0,1);
            timer1.Start();
            if (AppMain.User != null)
            {

            }
            else
            {
                this.Close();
            }
            if (AppMain.User == null)
            {
                return;
            }
            username.Content = AppMain.User.Username;
            GetAll();
            var imageQuery = (_CP.Users.Where(x => x.Username == AppMain.User.Username).Select(y => y.Image).FirstOrDefault());
            if (imageQuery != null)
            {
                MemoryStream ms = new MemoryStream(imageQuery);
                Image returnImage = new Image();
                returnImage.Source = BitmapFrame.Create(ms,
                                      BitmapCreateOptions.None,
                                      BitmapCacheOption.OnLoad);
                picturebox1 = returnImage;
            }
            else if (imageQuery == null)
            {
                picturebox1 = null;
            }
            GetServerNames();
        }

        private void Timer1_Tick(object? sender, EventArgs e)
        {
            var query = _CP.Messages.Where(x => x.Server == servername.Content.ToString() && x.Channel == channelname.Content.ToString()).ToList();
            dataGridView1.ItemsSource = query.ToList();
            GetServerNames();
        }

        private async void GetServerNames()
        {
            flyoutPanel1.Children.Clear();
            var result = await HttpHelper.httpClient.GetAsync($"/api/Users/GetAll/" + AppMain.User.Username).Result.Content.ReadAsStringAsync();
            var json = result;
            List<User> model = JsonConvert.DeserializeObject<List<User>>(json);

            foreach (var item in model)
            {
                if (item.Server != null)
                {
                    sw = item.Server.Split(',');
                    for (int i = 0; i < sw.Count(); i++)
                    {
                        Button button2 = new Button();
                        var bc = new BrushConverter();
                        flyoutPanel1.Children.Add(button2);
                        button2.Content = sw[i];
                        button = button2;
                        button3.Background = (Brush)bc.ConvertFrom("#202225");
                        button3.Foreground = Brushes.White;
                        button2.Click += HandleClick_Click;
                        button2.MouseUp += DeleteServers_MouseUp;
                    }
                }
                else
                {
                    return;
                }
            }
        }

        private async void DeleteServers_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var btn = sender as Button;
            User serverUser = new User()
            {
                Username = AppMain.User.Username,
                Server = btn.Content.ToString()
            };
            if (e.ButtonState == Mouse.RightButton)
            {

                MessageBoxResult dialogresult = MessageBox.Show("Do you want to leave from this server?", btn.Content.ToString(), MessageBoxButton.YesNo);
                if (dialogresult == MessageBoxResult.Yes)
                {
                    await HttpHelper.httpClient.PutAsJsonAsync($"/api/Users/DeleteServer/", serverUser).Result.Content.ReadAsStringAsync();
                    GetServerNames();
                }
            }
            else if (e.ButtonState == Mouse.MiddleButton)
            {
                servername.Content = btn.Content;
            }
        }

        private void HandleClick_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            servername.Content = btn.Content.ToString().Trim();
            var query = _CP.Messages.Where(x => x.Server == servername.Content.ToString() && x.Channel == channelname.Content.ToString()).ToList();
            dataGridView1.ItemsSource = query.ToList();
            GetChannelNames();
            GetUserNames();
        }
        Button buttonuser = new Button();
        private async void GetUserNames()
        {
            string[] users;
            flyoutPanel3.Children.Clear();
            var query = _CP.Servers.Where(x => x.ServerName == servername.Content.ToString()).ToList();
            foreach (var item in query)
            {
                users = item.Usernames.Split(',');
                for (int i = 0; i < users.Count(); i++)
                {
                    Button button2 = new Button();
                    var bc = new BrushConverter();
                    flyoutPanel3.Children.Add(button2);
                    button2.Content = users[i];
                    button2.FontSize = 12;
                    button2.FontStyle = FontStyles.Italic;
                    buttonuser.Content = button2.Content;
                    button3.Background = (Brush)bc.ConvertFrom("#202225");
                    button3.Foreground = Brushes.White;
                }
            }
        }

        string[] sww;
        Button button3 = new Button();
        
        private async void GetChannelNames()
        {
            flyoutPanel2.Children.Clear();
            var result = await HttpHelper.httpClient.GetAsync($"/api/Users/GetAllChannels/" + servername.Content).Result.Content.ReadAsStringAsync();
            var json = result;
            List<Server> model = JsonConvert.DeserializeObject<List<Server>>(json);
            foreach (var item in model)
            {
                if (item.Channels != null)
                {
                    sww = item.Channels.Split(',');
                    for (int i = 0; i < sww.Count(); i++)
                    {
                        Button button2 = new Button();
                        var bc = new BrushConverter();
                        flyoutPanel2.Children.Add(button2);
                        button2.Content = sww[i];
                        button2.FontSize= 12;
                        button2.FontStyle = FontStyles.Italic;
                        button3.Content = button2.Content;
                        //Background = "#202225" Foreground = "White"
                        button3.Background= (Brush)bc.ConvertFrom("#202225");
                        button3.Foreground=Brushes.White;
                        button2.Click += Channel_Click;
                    }
                }
            }
        }
        private void Channel_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            channelname.Content = btn.Content;
            var query = _CP.Servers.Where(x => x.ServerName == servername.Content.ToString() && x.Channels == channelname.Content.ToString()).ToList();
            dataGridView1.ItemsSource = query.ToList();
        }

        private void GetAll()
        {
            var query = _CP.Messages.Where(x => x.Server == servername.Content.ToString() && x.Channel == channelname.Content.ToString()).ToList();
            dataGridView1.ItemsSource = query.ToList();
        }

        private void ButtonCreateServer_Click(object sender, RoutedEventArgs e)
        {
            CreateServer CS = new CreateServer();
            CS.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            CS.ShowDialog();
        }
    }
}
