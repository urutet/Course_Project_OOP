using JParts.DBContext;
using JParts.MVVM.Commands;
using JParts.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace JParts.MVVM.ViewModel
{
    class CartViewModel : ViewModelBase
    {
        private List<Part> orderedPartsList;

        UnitOfWork.UnitOfWork uoW { get; set; }

        private decimal price;

        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }


        public RelayCommand AddOrderCommand { get; set; }

        public List<Part> OrderedPartsList
        {
            get { return orderedPartsList; }
            set { orderedPartsList = value; OnPropertyChanged(); }
        }

        public CartViewModel(MainViewModel mainViewModel)
        {
            OrderedPartsList = mainViewModel.PartsToAdd;

            GetSum();

            uoW = new UnitOfWork.UnitOfWork(new JPartsContext());

            AddOrderCommand = new RelayCommand(o =>
            {
                //try
                //{
                    Order order = new Order(mainViewModel.AuthorisedClient.ClientID, OrderedPartsList, mainViewModel.AuthorisedClient.AddressID, Price, false, DateTime.Now);
                    uoW.Orders.Add(order);
                    uoW.Complete();
                //}
                //catch(Exception e)
                //{
                //    MessageBox.Show(e.Message);
                //}
            });
        }

        private void GetSum()
        {
            foreach (Part part in OrderedPartsList)
                Price += Convert.ToDecimal(part.Price);
        }


    }
}
