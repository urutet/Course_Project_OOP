using JParts.DBContext;
using JParts.Enums;
using JParts.MVVM.Commands;
using JParts.MVVM.Model;
using JParts.Windows;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace JParts.MVVM.ViewModel
{
    class CarsViewModel : ViewModelBase
    {
        public RelayCommand AddCarCommand { get; set; }

        public RelayCommand UpdateCarCommand { get; set; }

        private Visibility _visibility;

        public Visibility visibility
        {
            get { return _visibility; }
            set { _visibility = value; }
        }


        private List<Car> cars;
        public List<Car> Cars { get => cars; set { cars = value; OnPropertyChanged(); } }

        public CarsViewModel(MainViewModel mainViewModel)
        {
            if (mainViewModel.AuthorisedClient.IsAdmin == true)
                visibility = Visibility.Visible;
            else
                visibility = Visibility.Hidden;

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
