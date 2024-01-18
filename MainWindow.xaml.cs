using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.Json;
using static WpfApp1.Result;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Application.Current.MainWindow.Height = 50;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Minimize(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void typing(object sender, RoutedEventArgs e)
        {
            search.Content = string.Empty;
        }

        private void _search(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Height = 800;
            double height = SystemParameters.WorkArea.Height;
            double width = SystemParameters.WorkArea.Width;
            this.Top = (height - this.Height) / 2;
            this.Left = (width - this.Width) / 2;
            string? CityName = (string)searchBox.Text;
            int id = 0;

            var api = new Apiresponse
            {
                weather = CityName
            };

            var client = new HttpClient();
            client.BaseAddress = new Uri("https://api.openweathermap.org/data/2.5/weather?q=" + CityName + "&appid=a7f3efc4c67aa47c5c0f8b59ed8fa904&units=metric");

            var json = JsonSerializer.Serialize(api);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = client.PostAsync("", content).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                var result = JsonSerializer.Deserialize<Result>(responseContent);

                id = Convert.ToInt32(result?.weather?[0].id);

                cityName.Content = CityName.ToUpper();
                Console.WriteLine(result?.name);
                des.Content = result?.weather?[0].description;

                if (id >= 500)
                {
                    weatherIcon.Source = new BitmapImage(new Uri(@"C:\\Users\\notel\\OneDrive\\Documents\\source\\repo\\WpfApp1\\img\\rain.png"));
                }
                if (id >= 600)
                {
                    weatherIcon.Source = new BitmapImage(new Uri(@"C:\\Users\\notel\\OneDrive\\Documents\\source\\repo\\WpfApp1\\img\\snow.png"));
                }
                if (id >= 700)
                {
                    weatherIcon.Source = new BitmapImage(new Uri(@"C:\\Users\\notel\\OneDrive\\Documents\\source\\repo\\WpfApp1\\img\\mist.png"));
                }
                if (id == 800)
                {
                    weatherIcon.Source = new BitmapImage(new Uri(@"C:\\Users\\notel\\OneDrive\\Documents\\source\\repo\\WpfApp1\\img\\sunny.png"));
                }
                if (id > 800)
                {
                    weatherIcon.Source = new BitmapImage(new Uri(@"C:\\Users\\notel\\OneDrive\\Documents\\source\\repo\\WpfApp1\\img\\cloudy.png"));
                }

                temp.Content = result?.main?.temp + "°C";
                wind.Content = result?.wind?.speed + "kt";
                humid.Content = result?.main?.humidity + "%";
                press.Content = result?.main?.pressure + "hPa";
            }
            else
            {
                cityName.Content = "error";
            }
        }       
    }

    public class Apiresponse
    {
        public string? weather { get; set; }
    }

    public class Result
    {
        public string? name { get; set; }
        public Wind? wind { get; set; }
        public List<Weather>? weather { get; set; }
        public Main? main { get; set; }

        public class Weather
        {
            public int id { get; set; }
            public string? description { get; set; }
        }

        public class Wind
        {
            public float speed { get; set; }
        }

        public class Main
        {
            public float temp { get; set; }
            public int pressure { get; set; }
            public int humidity { get; set; }
        }
    }
}