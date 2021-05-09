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

namespace JParts.UserControls
{
    /// <summary>
    /// Interaction logic for BindableWatermarkPasswordBox.xaml
    /// </summary>
    public partial class BindableWatermarkPasswordBox : UserControl
    {
        private bool _isPasswordChanging;

        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), typeof(BindableWatermarkPasswordBox),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    PasswordPropertyChangedCallback, null, false, UpdateSourceTrigger.PropertyChanged));
        
        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }


        public static readonly DependencyProperty WatermarkProperty =
            DependencyProperty.Register("Watermark", typeof(string), typeof(BindableWatermarkPasswordBox),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, 
                    WatermarkPropertyChangedCallback, null, false, UpdateSourceTrigger.PropertyChanged));

       
        public string Watermark
        {
            get { return (string)GetValue(WatermarkProperty); }
            set { SetValue(WatermarkProperty, value); }
        }


        private static void PasswordPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is BindableWatermarkPasswordBox bindablePasswordBox)
            {
                bindablePasswordBox.UpdatePassword();
            }
        }
        private static void WatermarkPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is BindableWatermarkPasswordBox bindablePasswordBox)
            {
                bindablePasswordBox.UpdateWatermark();
            }
        }

        private void UpdateWatermark()
        {
            WatermarkTextBlock.Text = Watermark;
        }


        private void UpdatePassword()
        {
            if (!_isPasswordChanging)
                PasswordBox.Password = Password;
        }

        private void PasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            WatermarkTextBlock.Visibility = PasswordBox.Password == string.Empty ? Visibility.Visible : Visibility.Hidden;

            _isPasswordChanging = true;
            Password = PasswordBox.Password;
            _isPasswordChanging = false;
        }
        public BindableWatermarkPasswordBox()
        {
            InitializeComponent();
        }
    }
}
