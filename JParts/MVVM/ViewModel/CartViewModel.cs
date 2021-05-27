using JParts.DBContext;
using JParts.MVVM.Commands;
using JParts.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Linq;

namespace JParts.MVVM.ViewModel
{
    class CartViewModel : ViewModelBase
    {
        private ObservableCollection<CartPart> orderedPartsList;

        public ObservableCollection<CartPart> OrderedPartsList
        {
            get { return orderedPartsList; }
            set { orderedPartsList = value; OnPropertyChanged(); }
        }

        UnitOfWork.UnitOfWork uoW { get; set; }

        private decimal price;

        public decimal Price
        {
            get { return price; }
            set { price = value; OnPropertyChanged(); }
        }

        public bool CanAdd
        {
            get
            {
                if (Price > 0)
                    return true;
                else
                    return false;
            }
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
                        //Order order = new Order(mainViewModel.AuthorisedClient.ClientID, new List<Part>(OrderedPartsList),
                        //    mainViewModel.AuthorisedClient.AddressID, Price, false, DateTime.Now);
                        var newOrder = new Order()
                        {
                            ClientID = mainViewModel.AuthorisedClient.ClientID,
                            PartsOrders = new List<PartsOrders>(),
                            AddressID = mainViewModel.AuthorisedClient.AddressID,
                            Price = this.Price,
                            Status = false,
                            OrderDate = DateTime.Now
                        };
                        foreach(var el in OrderedPartsList)
                        {
                            newOrder.PartsOrders.Add(new PartsOrders
                            {
                                Order = newOrder,
                                Part = uoW.Parts.Get(el.Part.PartID),
                                Amount = el.Amount
                            });
                        }
                        uoW.Orders.Add(newOrder);
                        uoW.Complete();

                        foreach(var el in OrderedPartsList)
                        {
                            uoW.Parts.Get(el.Part.PartID).Amount -= el.Amount;
                        }

                        uoW.Complete();

                        ClearAllFields();

                        if (mainViewModel.AuthorisedClient.IsAdmin == true)
                            mainViewModel.OrdersVM.LoadOrders();
                        else
                            mainViewModel.OrdersVM.LoadClientsOrders(mainViewModel.AuthorisedClient.ClientID);

                        mainViewModel.CatalogVM.DefaultList.Clear();
                        mainViewModel.CatalogVM.PartsList.Clear();

                        foreach (var p in uoW.Parts.GetAllParts())
                        {
                            mainViewModel.CatalogVM.DefaultList.Add(new CartPart(p, p.Amount));
                        }

                        mainViewModel.CatalogVM.PartsList = mainViewModel.CatalogVM.DefaultList;

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
        }, param => CanAdd);

            DeletePartCommand = new RelayCommand(o =>
            {
                if(o != null)
                {
                    var PartToDelete = o as CartPart;
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
            if (OrderedPartsList != null)
                Price = Convert.ToDecimal(OrderedPartsList.Sum(p => p.Part.Price * p.Amount));
            else
                MessageBox.Show("Корзина пуста");
        }

        private void ClearAllFields()
        {
            OrderedPartsList.Clear();
            Price = 0;
        }
    }
}
