using JParts.MVVM.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace JParts.MVVM.ViewModel
{
    class MainViewModel : ViewModelBase
    {

        public CatalogViewModel CatalogVM { get; set; }

        public OrdersViewModel OrdersVM { get; set; }

        public ContactsViewModel ContactsVM { get; set; }

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

        public RelayCommand CatalogViewCommand { get; set; }

        public RelayCommand OrdersViewCommand { get; set; }

        public RelayCommand ContactsViewCommand { get; set; }

        public MainViewModel()
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
        }
    }
}
