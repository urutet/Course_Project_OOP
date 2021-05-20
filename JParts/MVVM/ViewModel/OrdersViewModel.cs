using JParts.DBContext;
using JParts.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace JParts.MVVM.ViewModel
{
    class OrdersViewModel  : ViewModelBase
    {
        public UnitOfWork.UnitOfWork uoW { get; set; }


        private ObservableCollection<Order> ordersList;

        public ObservableCollection<Order> OrdersList
        {
            get { return ordersList; }
            set { ordersList = value; OnPropertyChanged(); }
        }

        public OrdersViewModel()
        {
            LoadOrders();
        }

        private void LoadOrders()
        {
            uoW = new UnitOfWork.UnitOfWork(new JPartsContext());
            OrdersList = new ObservableCollection<Order>(uoW.Orders.GetAllOrders());
        }
    }
}
