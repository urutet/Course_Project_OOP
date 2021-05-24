using JParts.DBContext;
using JParts.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;

namespace JParts.MVVM.ViewModel
{
    class OrdersViewModel  : ViewModelBase
    {
        public UnitOfWork.UnitOfWork uoW { get; set; }

        private Visibility _visibility;

        public Visibility visibility
        {
            get { return _visibility; }
            set { _visibility = value; OnPropertyChanged(); }
        }

        private ObservableCollection<bool> statusList;
        public ObservableCollection<bool> StatusList { get => statusList; set { statusList = value; OnPropertyChanged(); } }

        private ObservableCollection<Order> ordersList;

        public ObservableCollection<Order> OrdersList
        {
            get { return ordersList; }
            set { ordersList = value; OnPropertyChanged(); }
        }

        public OrdersViewModel(MainViewModel mainViewModel)
        {
            statusList = new ObservableCollection<bool>();
            statusList.Add(true);
            statusList.Add(false);

            if (mainViewModel.AuthorisedClient.IsAdmin == true)
            {
                visibility = Visibility.Visible;
                LoadOrders();
            }
            else
            {
                visibility = Visibility.Collapsed;
                LoadClientsOrders(mainViewModel.AuthorisedClient.ClientID);
            }

        }

        public void LoadOrders()
        {
            uoW = new UnitOfWork.UnitOfWork(new JPartsContext());
            OrdersList = new ObservableCollection<Order>(uoW.Orders.GetAllOrders());
        }

        public void LoadClientsOrders(int clientID)
        {
            uoW = new UnitOfWork.UnitOfWork(new JPartsContext());
            OrdersList = new ObservableCollection<Order>(uoW.Orders.GetClientOrders(clientID));
        }
    }
}
