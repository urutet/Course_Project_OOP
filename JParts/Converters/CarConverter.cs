using JParts.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace JParts.Converters
{
    public class CarConverter : IValueConverter
    {
        UnitOfWork.UnitOfWork uoW;

        public CarConverter()
        {
            uoW = new UnitOfWork.UnitOfWork(new DBContext.JPartsContext());
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Car car = uoW.Cars.Get((int)value);
            return car.Manufacturer + ' ' + car.Model + ' ' + car.Year;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
