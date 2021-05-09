using JParts.MVVM.ViewModel;
using JParts.Windows;
using JParts.Windows.Interfaces;
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(DataContext is ICloseWindows vm)
            {
                vm.Close += () =>
                {
                    this.Close();
                };
            }
        }

        //ADD LOGIN AND PASSWORD CHECK!!!!!!!!!!

    }
}
