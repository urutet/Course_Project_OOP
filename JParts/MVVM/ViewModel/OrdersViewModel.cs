using JParts.DBContext;
using JParts.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Net.Mail;
using System.Net;
using JParts.MVVM.Commands;
using JParts.Windows;

namespace JParts.MVVM.ViewModel
{
    class OrdersViewModel  : ViewModelBase
    {
        public UnitOfWork.UnitOfWork uoW { get; set; }

        MailAddress from;
        MailMessage mail;

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

        private List<PartsOrders> _orderedPartsList;

        public List<PartsOrders> OrderedPartsList
        {
            get { return _orderedPartsList; }
            set { _orderedPartsList = value; OnPropertyChanged(); }
        }

        public RelayCommand UpdateOrders { get; set; }

        public OrdersViewModel(MainViewModel mainViewModel, Order order = null)
        {
            statusList = new ObservableCollection<bool>();
            statusList.Add(true);
            statusList.Add(false);

            
            //Mail
            from = new MailAddress("ilyshka88@gmail.com", "JParts");

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

            UpdateOrders = new RelayCommand(o =>
            {
                OnStatusChanged();
                uoW.Complete();
            });

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

        private void OnStatusChanged()
        {
            foreach(Order order in OrdersList)
            {
                if (order.Status == true && uoW.Orders.Get(order.OrderID).Status == false)
                {
                    MailAddress to = new MailAddress("buz-14@mail.ru");
                    mail = new MailMessage(from, to);
                    mail.Subject = "Заказ доставлен";
                    mail.Body = $@"Заказ №{order.OrderID} был успешно доставлен";
                    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                    smtp.Credentials = new NetworkCredential("ilyshka88@gmail.com", "2876544Iy");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                    MessageBox.Show("Сообщение отправлено");
                }
            }
        }
    }
}
