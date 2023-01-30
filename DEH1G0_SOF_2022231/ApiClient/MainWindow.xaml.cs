using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ApiClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        HttpClient client;
        HubConnection conn;

        public event PropertyChangedEventHandler? PropertyChanged;

        private ObservableCollection<TorrentLog> _logs;
        public ObservableCollection<TorrentLog> Logs
        {
            get { return _logs; }
            set
            {
                _logs = value;
                OnPropertyChanged(nameof(this.Logs));
            }
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7235");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
              new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            Task.Run(async () =>
            {
                var list = await GetLogs();
                Logs = new ObservableCollection<TorrentLog>(list.OrderBy(t => t.Created));


            }).Wait();

            this.OnPropertyChanged(nameof(this.Logs));

            conn = new HubConnectionBuilder().WithUrl("https://localhost:7235/events").Build();

            conn.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await conn.StartAsync();
            };

            conn.On<TorrentLog>("logCreated", async t => await Refresh());

            Task.Run(async () =>
            {
                await conn.StartAsync();
            }).Wait();
        }

        private async Task Refresh()
        {
            var list = await GetLogs();
            Logs = new ObservableCollection<TorrentLog>(list.OrderBy(t=>t.Created));
        }

        private async Task<IEnumerable<TorrentLog>> GetLogs()
        {
            var response = await client.GetAsync("api/log");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<TorrentLog>>();
            }
            throw new Exception("something wrong...");
        }
    }
}
