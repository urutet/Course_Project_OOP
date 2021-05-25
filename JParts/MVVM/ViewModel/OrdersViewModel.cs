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
using System.Threading.Tasks;
using System.Linq;

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

        private ObservableCollection<Order> ordersNoTrackingList;

        public ObservableCollection<Order> OrdersNoTrackingList
        {
            get { return ordersNoTrackingList; }
            set { ordersNoTrackingList = value; OnPropertyChanged(); }
        }

        private DateTime _date;

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; OnPropertyChanged(); }
        }

        private decimal _price;

        public decimal Price
        {
            get { return _price; }
            set { _price = value; OnPropertyChanged(); }
        }



        private List<PartsOrders> _orderedPartsOrdersList;

        public List<PartsOrders> OrderedPartsOrdersList
        {
            get { return _orderedPartsOrdersList; }
            set { _orderedPartsOrdersList = value; OnPropertyChanged(); }
        }

        private List<Part> _orderedPartsList;

        public List<Part> OrderedPartsList
        {
            get { return _orderedPartsList; }
            set { _orderedPartsList = value; OnPropertyChanged(); }
        }

        public RelayCommand UpdateOrders { get; set; }

        public RelayCommand ShowOrder { get; set; }

        public OrdersViewModel(MainViewModel mainViewModel, Order order = null)
        {
            statusList = new ObservableCollection<bool>();
            statusList.Add(true);
            statusList.Add(false);

            OrderedPartsList = new List<Part>();


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
                OnStatusChanged().GetAwaiter();
                uoW.Complete();
                LoadOrders();
            });

            ShowOrder = new RelayCommand(o =>
            {
                if (o != null)
                {
                    Order order = o as Order;
                    OrderWindow window = new OrderWindow()
                    {
                        DataContext = new OrdersViewModel(mainViewModel, order)
                    };
                    window.Show();
                }
            });

            if (order != null)
            {
                OrderedPartsByOrderID(order.OrderID);
                Price = order.Price;
                Date = order.OrderDate;
            }

        }

        public void LoadOrders()
        {
            uoW = new UnitOfWork.UnitOfWork(new JPartsContext());
            OrdersList = new ObservableCollection<Order>(uoW.Orders.GetAllOrders());
            OrdersNoTrackingList = new ObservableCollection<Order>(uoW.Orders.GetAllOrdersAsNoTracking());
        }

        public void LoadClientsOrders(int clientID)
        {
            uoW = new UnitOfWork.UnitOfWork(new JPartsContext());
            OrdersList = new ObservableCollection<Order>(uoW.Orders.GetClientOrders(clientID));
        }

        public void OrderedPartsByOrderID(int orderID)
        {
            OrderedPartsOrdersList = uoW.Parts.GetPartsByOrderID(orderID);
        }

        private async Task OnStatusChanged()
        {
            SmtpClient client = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential()
                {
                    UserName = "jpartsshop24@gmail.com",
                    Password = "Jpartsshop24@!"
                }
            };
            MailAddress From = new MailAddress("jpartsshop24@gmail.com", "JParts");

            foreach (Order order in OrdersList)
            {
                Order oldOrder = ordersNoTrackingList.Where(o => o.OrderID == order.OrderID).First();
                if (order.Status == true && oldOrder.Status == false)      //Need more "Advanced" logic to check the change of property status
                {
                    
                    Client reciever = uoW.Clients.GetByID(order.ClientID);
                    MailAddress To = new MailAddress(reciever.Email);
                    MailMessage message = new MailMessage()
                    {
                        From = From,
                        Subject = "Заказ JParts",
                        Body = $"Здравствуйте, {reciever.Name}! ваш заказ №{order.OrderID} доставлен."
                    };
                    message.To.Add(To);

                    try
                    {
                        await client.SendMailAsync(message);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }
            }
            client.SendCompleted += Client_SendCompleted;
        }

        private void Client_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if(e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
                return;
            }
            else
            {
                MessageBox.Show("Сообщения успешно отправлены");
            }
        }
    }
}
