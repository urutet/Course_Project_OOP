using JParts.DBContext;
using JParts.MVVM.Commands;
using JParts.MVVM.Model;
using JParts.Windows;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JParts.MVVM.ViewModel
{
    class AddPartViewModel : ViewModelBase
    {
        //Part
        public string PartID { get; set; }

        private string name;
        public string Name { get => name; set { name = value; OnPropertyChanged(); } }

        private string type { get; set; }
        public string Type { get => type; set { type = value; OnPropertyChanged(); } }

        private double? price;
        public double? Price { get => price; set { price = value; OnPropertyChanged(); } }

        private bool availability;
        public bool Availability { get => availability; set { availability = value; OnPropertyChanged(); } }

        private string image;
        public string Image { get => image; set { image = value; OnPropertyChanged(); } }

        //Car
        public string CarID { get; set; }

        private List<string> manufacturer;
        public List<string> Manufacturer { get => manufacturer; 
            set { manufacturer = value; OnPropertyChanged(); } }

        private List<string> model;
        public List<string> Model {
            get => model;
            set { model = value; OnPropertyChanged(); }
        }

        private List<int?> year;
        public List<int?> Year {
            get => year;
            set { year = value; OnPropertyChanged(); }
        }

        private string _selectedManufacturer;

        public string SelectedManufacturer
        {
            get { return _selectedManufacturer; }
            set
            {   _selectedManufacturer = value;
                OnPropertyChanged();
                if (_selectedManufacturer != null)
                    LoadModels();
            }
        }

        private string _selectedModel;

        public string SelectedModel
        {
            get { return _selectedModel; }
            set
            {
                _selectedModel = value;
                OnPropertyChanged();
                if (_selectedManufacturer != null && _selectedModel != null)
                    LoadYears();

            }
        }

        private int? _selectedYear;

        public int? SelectedYear
        {
            get { return _selectedYear; }
            set { _selectedYear = value; OnPropertyChanged(); }
        }



        public RelayCommand AddImageCommand { get; set; }

        public RelayCommand AddPartCommand { get; set; }

        public RelayCommand AddCarCommand { get; set; }

        public AddPartViewModel()
        {
            AddImageCommand = new RelayCommand(o =>
            {
                OpenFileDialog openDialog = new OpenFileDialog();
                openDialog.Filter = "Image files (*.png;*.jpeg; *.jpg)|*.png;*.jpeg; *.jpg";
                openDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                if(openDialog.ShowDialog() == true)
                {
                    Image = openDialog.FileName;
                }
            });

            AddPartCommand = new RelayCommand(o =>
            {
                Part part = new Part(GetCar(SelectedManufacturer, SelectedModel, SelectedYear).CarID, Name, Type, Price, Availability, Image);

                UnitOfWork.UnitOfWork uoW = new UnitOfWork.UnitOfWork(new JPartsContext());
                uoW.Parts.Add(part);
                uoW.Complete();
                MessageBox.Show("Деталь успешно добавлена");
            });

            AddCarCommand = new RelayCommand(o =>
            {
                AddCarWindow window = new AddCarWindow();
                window.Show();
            });

            LoadManufacturers();
        }

        private async void LoadManufacturers()
        {
            UnitOfWork.UnitOfWork uoW = new UnitOfWork.UnitOfWork(new JPartsContext());
            Manufacturer =  await uoW.Cars.GetAllManufacturers();
        }
        private async void LoadModels()
        {
            UnitOfWork.UnitOfWork uoW = new UnitOfWork.UnitOfWork(new JPartsContext());
            Model =  await uoW.Cars.GetManufacturerModels(SelectedManufacturer);
        }
        private async void LoadYears()
        {
            UnitOfWork.UnitOfWork uoW = new UnitOfWork.UnitOfWork(new JPartsContext());
            Year =  await uoW.Cars.GetModelsYears(SelectedManufacturer, SelectedModel);
        }

        private Car GetCar(string manufacturer, string model, int? year)
        {
            UnitOfWork.UnitOfWork uoW = new UnitOfWork.UnitOfWork(new JPartsContext());
            return uoW.Cars.GetCar(manufacturer, model, year);
        }

    }
}
