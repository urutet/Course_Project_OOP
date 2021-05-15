using JParts.MVVM.Commands;
using JParts.MVVM.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

namespace JParts.MVVM.ViewModel
{
    class AddCarViewModel : ViewModelBase, IDataErrorInfo
    {
        public string Error => throw new NotImplementedException();

        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch(columnName)
                {
                    case "Year":
                        Regex regex = new Regex("^(19|20)\\d{2}$");
                        if (!regex.IsMatch(Convert.ToString(Year)))
                            error = "Введите корректный год";
                        break;
                    default:
                        error = null;
                        break;
                }
                return error;
            }
        }

        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public int? Year { get; set; }

        public RelayCommand AddCarCommand { get; set; }

        public UnitOfWork.UnitOfWork uoW;

        public AddCarViewModel()
        {
            uoW = new UnitOfWork.UnitOfWork(new DBContext.JPartsContext());

            AddCarCommand = new RelayCommand(o =>
            {
                try
                {
                    Car car = new Car(Manufacturer, Model, Year);
                    uoW.Cars.Add(car);
                    uoW.Complete();
                    MessageBox.Show("Машина успешно добавлена");
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            });
        }
    }
}
