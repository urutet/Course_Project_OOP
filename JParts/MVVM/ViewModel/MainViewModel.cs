using JParts.MVVM.Commands;
using JParts.MVVM.Model;
using JParts.Windows;
using JParts.Windows.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;

namespace JParts.MVVM.ViewModel
{
    class MainViewModel : ViewModelBase, ICloseWindows
    {

        public CatalogViewModel CatalogVM { get; set; }

        public OrdersViewModel OrdersVM { get; set; }

        public ContactsViewModel ContactsVM { get; set; }

        public CarsViewModel CarsVM { get; set; }

        public RegisterViewModel RegisterVM { get; set; }

        public UnitOfWork.UnitOfWork uoW { get; }

        private Visibility _visibility;

        public Visibility visibility
        {
            get { return _visibility; }
            set { _visibility = value; }
        }


        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        private Client _authorisedClient;

        private ObservableCollection<CartPart> partsToAdd;

        public ObservableCollection<CartPart> PartsToAdd
        {
            get { return partsToAdd; }
            set { partsToAdd = value; OnPropertyChanged() ; }
        }


        public Client AuthorisedClient { get => _authorisedClient; set { _authorisedClient = value; OnPropertyChanged(); } }

        public RelayCommand CatalogViewCommand { get; set; }

        public RelayCommand OrdersViewCommand { get; set; }

        public RelayCommand ContactsViewCommand { get; set; }

        public RelayCommand RegisterViewCommand { get; set; }

        public RelayCommand ExitToLoginCommand { get; set; }

        public RelayCommand CarsViewCommand { get; set; }

        public RelayCommand CartCommand { get; set; }


        /*public MainViewModel()
        {
            CatalogVM = new CatalogViewModel();
            OrdersVM = new OrdersViewModel();
            ContactsVM = new ContactsViewModel();

            CurrentView = CatalogVM;

            CatalogViewCommand = new RelayCommand(o =>
            {
                CurrentView = CatalogVM;
            });

            OrdersViewCommand = new RelayCommand(o =>
            {
                CurrentView = OrdersVM;
            });
            
            ContactsViewCommand = new RelayCommand(o =>
            {
                CurrentView = ContactsVM;
            });
        }*/

        public MainViewModel(Client authorisedClient)
        {
            AuthorisedClient = authorisedClient;

            CatalogVM = new CatalogViewModel(this);
            OrdersVM = new OrdersViewModel(this);
            ContactsVM = new ContactsViewModel();
            CarsVM = new CarsViewModel(this);
            RegisterVM = new RegisterViewModel();

            uoW = new UnitOfWork.UnitOfWork(new DBContext.JPartsContext());

            if (authorisedClient.IsAdmin == true)
                visibility = Visibility.Visible;
            else
                visibility = Visibility.Collapsed;
            

            CurrentView = CatalogVM;

            CatalogViewCommand = new RelayCommand(o =>
            {
                CurrentView = CatalogVM;
            });

            OrdersViewCommand = new RelayCommand(o =>
            {
                CurrentView = OrdersVM;
            });

            ContactsViewCommand = new RelayCommand(o =>
            {
                CurrentView = ContactsVM;
            });

            ExitToLoginCommand = new RelayCommand(o =>
            {
                LoginWindow loginWindow = new LoginWindow()
                {
                    DataContext = new LoginViewModel()
                };
                CloseWindow();
                loginWindow.Show();
            });

            CarsViewCommand = new RelayCommand(o =>
            {
                CurrentView = CarsVM;
            });

            CartCommand = new RelayCommand(o =>
            {
                CartWindow window = new CartWindow()
                {
                    DataContext = new CartViewModel(this)
                };
                window.Show();
            });

            RegisterViewCommand = new RelayCommand(o =>
            {
                CurrentView = RegisterVM;
            });

        }

        public MainViewModel()
        {
            CatalogVM = new CatalogViewModel(this);
            OrdersVM = new OrdersViewModel(this);
            ContactsVM = new ContactsViewModel();

            CurrentView = CatalogVM;

            CatalogViewCommand = new RelayCommand(o =>
            {
                CurrentView = CatalogVM;
            });

            OrdersViewCommand = new RelayCommand(o =>
            {
                CurrentView = OrdersVM;
            });

            ContactsViewCommand = new RelayCommand(o =>
            {
                CurrentView = ContactsVM;
            });

            ExitToLoginCommand = new RelayCommand(o =>
            {
                LoginWindow loginWindow = new LoginWindow()
                {
                    DataContext = new LoginViewModel()
                };
                loginWindow.Show();
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
