using JParts.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace JParts.Converters
{
    class OrderConverter : IValueConverter
    {
        UnitOfWork.UnitOfWork uoW;

        public OrderConverter()
        {
            uoW = new UnitOfWork.UnitOfWork(new DBContext.JPartsContext());
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Order order = uoW.Orders.Get((int)value);
            Client client = uoW.Clients.Get(order.ClientID);
            return order.OrderDate + client.Name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
