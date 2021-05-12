using JParts.MVVM.Commands;
using JParts.Services.AuthenticationServices;
using JParts.Windows;
using JParts.Windows.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

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

        public RelayCommand MainViewCommand { get; set; }

        public RelayCommand RegisterViewCommand { get; set; }

        public RelayCommand ForgotPasswordCommand { get; set; }

        public RelayCommand LoginCommand { get; set; }

        public LoginViewModel()
        {
            _unitOfWork = new UnitOfWork.UnitOfWork(new DBContext.JPartsContext());
            authenticationService = new AuthenticationService(_unitOfWork);

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
            });

            LoginCommand = new RelayCommand(obj =>
            {
                
                if(authenticationService.Login(Login, Password).IsAdmin == true)
                {
                    MainWindowAdmin windowAdmin = new MainWindowAdmin()
                    {
                        DataContext = new MainViewModel()
                    };
                    windowAdmin.Show();
                    CloseWindow();
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
