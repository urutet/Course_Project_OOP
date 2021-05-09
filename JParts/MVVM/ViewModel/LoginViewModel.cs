using JParts.MVVM.Commands;
using JParts.Windows;
using JParts.Windows.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace JParts.MVVM.ViewModel
{
    class LoginViewModel : ViewModelBase, ICloseWindows
    {
        public RelayCommand MainViewCommand { get; set; }

        public RelayCommand RegisterViewCommand { get; set; }

        public RelayCommand ForgotPasswordCommand { get; set; }

        public LoginViewModel()
        {
            MainViewCommand = new RelayCommand(obj =>
            {
                MainWindowAdmin windowAdmin = new MainWindowAdmin()
                {
                    DataContext = new MainViewModel()
                };
                windowAdmin.Show();
                CloseWindow();
            });

            RegisterViewCommand = new RelayCommand(obj =>
            {
                RegisterWindow regWindow = new RegisterWindow()
                {
                    DataContext = new RegisterViewModel()
                };
                regWindow.Show();
                CloseWindow();
            });

        }

        void CloseWindow()
        {
            Close?.Invoke();
        }

        public Action Close { get; set; }
    }
}
