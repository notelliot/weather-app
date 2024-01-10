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

namespace WpfApp1.control
{
    public partial class header : UserControl
    {
        public header()
        {
            InitializeComponent();
        }

        private void Clicked(object sender, MouseButtonEventArgs e)
        {
            searchBox.Content = string.Empty;
        }

        private void Changed(object sender, RoutedEventArgs e)
        {
            searchBox.Content = string.Empty;
        }
    }
}
