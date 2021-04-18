using JParts.MVVM.ViewModel;
using JParts.Windows;
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

namespace JParts
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        //ADD LOGIN AND PASSWORD CHECK!!!!!!!!!!
        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindowAdmin windowAdmin = new MainWindowAdmin()
            {
                DataContext = new MainViewModel()
            };
            windowAdmin.Show();
            this.Close();
        }
    }
}
