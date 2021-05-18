using JParts.DBContext;
using JParts.MVVM.Commands;
using JParts.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;

namespace JParts.MVVM.ViewModel
{
    class CartViewModel : ViewModelBase
    {
        private ObservableCollection<Part> orderedPartsList;

        public ObservableCollection<Part> OrderedPartsList
        {
            get { return orderedPartsList; }
            set { orderedPartsList = value; OnPropertyChanged(); }
        }

        UnitOfWork.UnitOfWork uoW { get; set; }

        private decimal price;

        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }


        public RelayCommand AddOrderCommand { get; set; }

        public RelayCommand DeletePartCommand { get; set; }



        public CartViewModel(MainViewModel mainViewModel)
        {
            OrderedPartsList = mainViewModel.PartsToAdd;

            GetSum();

            uoW = new UnitOfWork.UnitOfWork(new JPartsContext());

            AddOrderCommand = new RelayCommand(o =>
            {
                try
                {
                    if (OrderedPartsList.Count > 0)
                    {
                        Order order = new Order(mainViewModel.AuthorisedClient.ClientID, new List<Part>(OrderedPartsList), mainViewModel.AuthorisedClient.AddressID, Price, false, DateTime.Now);
                        uoW.Orders.Add(order);
                        uoW.Complete();

                        ClearAllFields();

                        MessageBox.Show("Заказ успешно добавлен");
                    }
                    else
                    {
                        MessageBox.Show("Не выбрано ни одного товара");
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
        });

            DeletePartCommand = new RelayCommand(o =>
            {
                if(o != null)
                {
                    var PartToDelete = o as Part;
                    OrderedPartsList.Remove(PartToDelete);
                    mainViewModel.PartsToAdd.Remove(PartToDelete);
                    mainViewModel.CatalogVM.PartsToAdd.Remove(PartToDelete);

                    GetSum();
                }
            });
        }

        private void GetSum()
        {
            Price = 0;
            foreach (Part part in OrderedPartsList)
                Price += Convert.ToDecimal(part.Price);
        }

        private void ClearAllFields()
        {
            OrderedPartsList.Clear();
            Price = 0;
        }


    }
}
