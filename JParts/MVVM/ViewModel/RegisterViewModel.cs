using JParts.DBContext;
using JParts.MVVM.Commands;
using JParts.MVVM.Model;
using JParts.Services.AuthenticationServices;
using JParts.Windows.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace JParts.MVVM.ViewModel
{
    class RegisterViewModel : ViewModelBase
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


        //Address
        private string city;
        public string City { get => city; set { city = value; OnPropertyChanged(); } }
        private string street;
        public string Street { get => street; set { street = value; OnPropertyChanged(); } }
        private int house_Num;
        public int House_Num { get => house_Num; set { house_Num = value; OnPropertyChanged(); } }
        private int flat_Num;
        public int Flat_Num { get => flat_Num; set { flat_Num = value; OnPropertyChanged(); } }



        public RelayCommand RegisterUserCommand { get; set; }

        public RegisterViewModel()
        {
            RegisterUserCommand = new RelayCommand(obj =>
            {
                UnitOfWork.UnitOfWork unitOfWork = new UnitOfWork.UnitOfWork(new JPartsContext());
                IAuthenticationService authentication = new AuthenticationService(unitOfWork);

                Address address = new Address(Convert.ToString(House_Num + Flat_Num), City, Street, House_Num, Flat_Num);
                unitOfWork.Addresses.Add(address);
                unitOfWork.Complete();

                if (authentication.Register(Name + Login, Name, Phone_Num, Convert.ToString(House_Num + Flat_Num), Email, Login, Password, ConfirmPassword).Result)
                {
                    CloseWindow();
                }
                else
                {
                    //add logic here
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
