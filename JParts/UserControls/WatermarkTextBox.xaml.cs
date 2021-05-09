using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class WatermarkTextBox : UserControl
    {

        public static readonly RoutedEvent MouseEnterRoutedEvent =
            EventManager.RegisterRoutedEvent("MouseEnterRouted", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(WatermarkTextBox));

        public string TextBind
        {
            get { return (string)GetValue(TextBindProperty); }
            set { SetValue(TextBindProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextBind.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBindProperty =
            DependencyProperty.Register("TextBind", typeof(string), typeof(WatermarkTextBox), new PropertyMetadata(""));



        public string Watermark
        {
            get { return (string)GetValue(WatermarkProperty); }
            set { SetValue(WatermarkProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Watermark.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WatermarkProperty =
            DependencyProperty.Register("Watermark", typeof(string), typeof(WatermarkTextBox), new PropertyMetadata("Enter watermark"));



        public WatermarkTextBox()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(WMTextBox.Text == "")
                WatermarkTextBlock.Visibility = Visibility.Visible;
            else
                WatermarkTextBlock.Visibility = Visibility.Hidden;
        }

        public event RoutedEventHandler MouseEnterRouted
        {
            add { AddHandler(WatermarkTextBox.MouseEnterEvent, value); }
            remove { RemoveHandler(WatermarkTextBox.MouseEnterEvent, value); }
        }
    }
}
