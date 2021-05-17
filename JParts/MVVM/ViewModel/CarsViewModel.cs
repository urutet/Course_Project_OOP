using JParts.DBContext;
using JParts.Enums;
using JParts.MVVM.Commands;
using JParts.MVVM.Model;
using JParts.Windows;
using System;
using System.Collections.Generic;
using System.Text;

namespace JParts.MVVM.ViewModel
{
    class CarsViewModel : ViewModelBase
    {
        public RelayCommand AddCarCommand { get; set; }

        public RelayCommand UpdateCarCommand { get; set; }



        private List<Car> cars;
        public List<Car> Cars { get => cars; set { cars = value; OnPropertyChanged(); } }

        public CarsViewModel()
        {
            AddCarCommand = new RelayCommand(o =>
            {
                AddCarWindow window = new AddCarWindow()
                {
                    DataContext = new AddCarViewModel(null, this, CarOperation.Add, null)
                };
                window.Show();
            });

            UpdateCarCommand = new RelayCommand(o =>
            {
                if (o != null)
                {
                    var UpdatedCar = o as Car;
                    AddCarWindow window = new AddCarWindow()
                    {
                        DataContext = new AddCarViewModel(null, this, CarOperation.Edit, UpdatedCar)
                    };
                    window.Show();
                }
            });

            LoadCars();
        }

        public async void LoadCars()
        {
            UnitOfWork.UnitOfWork uoW = new UnitOfWork.UnitOfWork(new JPartsContext());
            Cars = await uoW.Cars.GetAllCarsAsync();
        }
    }
}
