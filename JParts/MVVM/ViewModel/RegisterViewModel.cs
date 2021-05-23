using JParts.DBContext;
using JParts.MVVM.Commands;
using JParts.Services.AuthenticationServices;
using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using JParts.Enums;
using JParts.Windows.Interfaces;

namespace JParts.MVVM.ViewModel
{
    class RegisterViewModel : ViewModelBase, ICloseWindows, IDataErrorInfo
    {

        //Client
        private string clientID;
        public string ClientID { get => clientID; set { clientID = value; OnPropertyChanged(); } }
        private string name;
        public string Name { get => name; set { name = value; OnPropertyChanged(); } }
        private string phone_Num;
        public string Phone_Num { get => phone_Num; set { phone_Num = value; OnPropertyChanged(); } }

        private string addressID;
        public string AddressID { get => addressID; set { addressID = value; OnPropertyChanged(); } }
        private string email;
        public string Email { get => email; set { email = value; OnPropertyChanged(); } }
        private string login;
        public string Login { get => login; set { login = value; OnPropertyChanged(); } }
        private string password;
        public string Password { get => password; set { password = value; OnPropertyChanged(); } }
        private string confirmPassword;
        public string ConfirmPassword { get => confirmPassword; set { confirmPassword = value; OnPropertyChanged(); } }

        private bool isAdmin;
        public bool IsAdmin { get => isAdmin; set { isAdmin = value; OnPropertyChanged(); } }


        //Address
        private string city;
        public string City { get => city; set { city = value; OnPropertyChanged(); } }
        private string street;
        public string Street { get => street; set { street = value; OnPropertyChanged(); } }
        private int? house_Num;
        public int? House_Num { get => house_Num; set { house_Num = value; OnPropertyChanged(); } }
        private int? flat_Num;
        public int? Flat_Num { get => flat_Num; set { flat_Num = value; OnPropertyChanged(); } }

        public RelayCommand RegisterUserCommand { get; set; }


        public Action Close { get; set; }

        public string Error { get; set; }

        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "Phone_Num":
                        Regex phRegex = new Regex("^(\\+375|80)(29|25|44|33)(\\d{3})(\\d{2})(\\d{2})$");
                        if (!phRegex.IsMatch(Phone_Num))
                        {
                            error = "Введите корректный номер (прим. +375291111111)";
                        }
                        break;
                    case "Email":
                        Regex eRegex = new Regex("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$");
                        if (!eRegex.IsMatch(Email))
                        {
                            error = "Введите корректный Email";
                        }
                        break;
                    case "Login":
                        Regex lRegex = new Regex("^[A-Za-z0-9]+$");
                        if (!lRegex.IsMatch(Login))
                        {
                            error = "Используйте только символы латинского алфавита и цифры";
                        }
                        break;
                    case "Password":
                        Regex pRegex = new Regex("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)[a-zA-Z\\d]{8,}$");
                        if (!pRegex.IsMatch(Password))
                        {
                            error = "Введите верный пароль (8 символов, из которых 2 - буквы латинского алфавита)";
                        }
                        break;
                    case "ConfirmPassword":
                        if (ConfirmPassword != Password)
                        {
                            error = "Пароли не совпадают";
                        }
                        break;
                    default:
                        error = null;
                        break;
                }
                return error;
            }
        }

        protected bool CanRegister
        {
            get
            {
                if (Error == String.Empty || Error == null)
                    return true;
                else
                    return false;
            }
        }

        public RegisterViewModel()
        {
            RegisterUserCommand = new RelayCommand(obj =>
            {
                UnitOfWork.UnitOfWork unitOfWork = new UnitOfWork.UnitOfWork(new JPartsContext());
                IAuthenticationService authentication = new AuthenticationService(unitOfWork);
                try
                {
                    RegistrationResult result = authentication.Register(Login, Name, Phone_Num, House_Num, Flat_Num, Street,
                        City, Email, Login, Password, ConfirmPassword, IsAdmin);


                    if (result == RegistrationResult.Success)
                    {
                        MessageBox.Show("Регистрация прошла успешно");
                        ClearAllFields();
                    }
                    if (result == RegistrationResult.LoginAlreadyExists)
                    {
                        MessageBox.Show("Пользователь с таким именем уже существует");
                    }
                    if (result == RegistrationResult.PasswordDoNotMatch)
                    {
                        MessageBox.Show("Пароли не совпадают");
                    }
                }
                catch(Exception e)
                {
                    MessageBox.Show("Заполните все поля корректно");
                }
            }, param => CanRegister);
        }

        private void ClearAllFields()
        {
            ClientID = null;
            Name = null;
            Phone_Num = null;
            AddressID = null;
            Email = null;
            Login = null;
            Password = null;
            ConfirmPassword = null;
            IsAdmin = false;
            City = null;
            Street = null;
            House_Num = null;
            Flat_Num = null;
        }

        void CloseWindow()
        {
            Close?.Invoke();
        }

    }
}
