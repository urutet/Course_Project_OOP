using JParts.DBContext;
using JParts.Enums;
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
        //Dictionnary
        string carButton;
        public string CarButton { get => carButton; set { carButton = value; OnPropertyChanged(); } }

        string headerText;
        public string HeaderText { get => headerText; set { headerText = value; OnPropertyChanged(); } }

        //VM to add the car to list
        AddPartViewModel AddPartViewModel;
        CarsViewModel _carsViewModel;

        CarOperation _carOperation;


        //Car to update
        private Car carToUpdate;
        public Car CarToUpdate { get => carToUpdate; set { carToUpdate = value; OnPropertyChanged(); } }

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
                            error = "Введите корректный год (1900 - 2099)";
                        break;
                    default:
                        error = null;
                        break;
                }
                return error;
            }
        }

        private string manufacturer;
        public string Manufacturer { get => manufacturer; set { manufacturer = value; OnPropertyChanged(); } }

        private string model;
        public string Model { get => model; set { model = value; OnPropertyChanged(); } }

        private int? year;
        public int? Year { get => year; set { year = value; OnPropertyChanged(); } }

        public RelayCommand AddCarCommand { get; set; }

        public UnitOfWork.UnitOfWork uoW;

        public AddCarViewModel(AddPartViewModel addPartViewModel = null, CarsViewModel carsViewModel = null, CarOperation  carOperation = CarOperation.Add, Car _car = null)
        {
            AddPartViewModel = addPartViewModel;
            _carsViewModel = carsViewModel;
            _carOperation = carOperation;

                uoW = new UnitOfWork.UnitOfWork(new DBContext.JPartsContext());
            if (_carOperation == CarOperation.Add)
            {
                CarButton = "Добавить";
                HeaderText = "Добавить автомобиль";
                AddCarCommand = new RelayCommand(o =>
                {
                    try
                    {

                            Car car = new Car(Manufacturer, Model, Year);
                            uoW.Cars.Add(car);
                            uoW.Complete();
                        MessageBox.Show("Машина успешно добавлена");
                        if(carsViewModel != null)
                            carsViewModel.LoadCars();
                        if(addPartViewModel != null)
                            AddPartViewModel.LoadManufacturers();
                    }
                    catch (Exception e)
                    {
                    //Dummy
                }
                });
            }
            if (_carOperation == CarOperation.Edit && _car != null)
            {

                CarButton = "Обновить";
                HeaderText = "Обновить автомобиль";
                Manufacturer = _car.Manufacturer;
                Model = _car.Model;
                Year = _car.Year;
                AddCarCommand = new RelayCommand(o =>
                {
                    try
                    {
                        UnitOfWork.UnitOfWork uoW = new UnitOfWork.UnitOfWork(new JPartsContext());
                        var carToEdit = uoW.Cars.Get(_car.CarID);
                        MessageBox.Show("Машина успешно обновлена");
                        carToEdit.Manufacturer = Manufacturer;
                        carToEdit.Model = Model;
                        carToEdit.Year = Year;
                        uoW.Complete();
                        if (carsViewModel != null)
                            carsViewModel.LoadCars();
                        if (addPartViewModel != null)
                            AddPartViewModel.LoadManufacturers();
                    }
                    catch (Exception e)
                    {
                        //Dummy
                    }
                });
            }


        }

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

                    AddPartViewModel.LoadManufacturers();
                }
                catch (Exception e)
                {
                    //Dummy
                }
            });
        }

        
    }
}
