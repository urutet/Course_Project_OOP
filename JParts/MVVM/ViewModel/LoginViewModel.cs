using JParts.MVVM.Commands;
using JParts.MVVM.Model;
using JParts.Services.AuthenticationServices;
using JParts.Windows;
using JParts.Windows.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace JParts.MVVM.ViewModel
{
    class LoginViewModel : ViewModelBase, ICloseWindows
    {
        private string login;
        public string Login { get => login; set { login = value; OnPropertyChanged("Login"); } }

        private string password;
        public string Password { get => password; set { password = value; OnPropertyChanged("Password"); } }

        public IAuthenticationService authenticationService { get; }

        public UnitOfWork.UnitOfWork _unitOfWork { get; }

        public RelayCommand RegisterViewCommand { get; set; }

        public RelayCommand ForgotPasswordCommand { get; set; }

        public RelayCommand LoginCommand { get; set; }

        public LoginViewModel()
        {
            _unitOfWork = new UnitOfWork.UnitOfWork(new DBContext.JPartsContext());
            authenticationService = new AuthenticationService(_unitOfWork);

            RegisterViewCommand = new RelayCommand(obj =>
            {
                RegisterWindow regWindow = new RegisterWindow()
                {
                    DataContext = new RegisterViewModel()
                };
                regWindow.Show();
            });

            LoginCommand = new RelayCommand(obj =>
            {
                try
                {
                    Client result = authenticationService.Login(Login, Password);
                    if (result.IsAdmin == true)
                    {
                        MainWindowAdmin windowAdmin = new MainWindowAdmin()
                        {
                            DataContext = new MainViewModel(result)
                        };
                        windowAdmin.Show();
                        CloseWindow();
                    }
                    else if (result.IsAdmin == false)
                    {
                        //Add default user implementation
                        CloseWindow();
                    }
                    //MainWindowAdmin windowAdmin = new MainWindowAdmin()
                    //{
                    //    DataContext = new MainViewModel()
                    //};
                    //windowAdmin.Show();
                    //CloseWindow();
                }
                catch(Exception e)
                {
                    MessageBox.Show("Неверный логин или пароль!");
                }
            });

        }

        void CloseWindow()
        {
            Close?.Invoke();
        }

        public Action Close { get; set; }
    }
}
