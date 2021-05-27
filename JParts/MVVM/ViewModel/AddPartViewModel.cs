using JParts.DBContext;
using JParts.MVVM.Commands;
using JParts.MVVM.Model;
using JParts.Windows;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using JParts.Enums;
using System.Collections.ObjectModel;

namespace JParts.MVVM.ViewModel
{
    class AddPartViewModel : ViewModelBase
    {
        //VM
        public MainViewModel MainVM { get; set; }
        //Dictionnary
        string partButton;
        public string PartButton { get => partButton; set { partButton = value; OnPropertyChanged(); } }

        string imageButton;
        public string ImageButton { get => imageButton; set { imageButton = value; OnPropertyChanged(); } }

        string headerText;
        public string HeaderText { get => headerText; set { headerText = value; OnPropertyChanged(); } }
        //VM for its update
        CatalogViewModel CatalogVM;

        Part _partToUpdate;
        public Part PartToUpdate { get => _partToUpdate; set { _partToUpdate = value; OnPropertyChanged(); } }

        //Enum for operation
        PartOperation partOperation;
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

        private int? amount;

        public int? Amount
        {
            get { return amount; }
            set { amount = value; OnPropertyChanged(); }
        }


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

        public AddPartViewModel(CatalogViewModel catalogVM, PartOperation _partOperation, Part part = null, MainViewModel mainViewModel = null)
        {
            CatalogVM = catalogVM;

            MainVM = mainViewModel;

            partOperation = _partOperation;

            if(partOperation == PartOperation.Add)
            {
                PartButton = "Добавить запчасть";
                ImageButton = "Добавить картинку";
                HeaderText = "Добавить запчасть";

                AddPartCommand = new RelayCommand(o =>
                {
                    try
                    {
                        Part part = new Part(GetCar(SelectedManufacturer, SelectedModel, SelectedYear).CarID, Name, Type, Price, Availability, Image, Amount);

                        UnitOfWork.UnitOfWork uoW = new UnitOfWork.UnitOfWork(new JPartsContext());
                        uoW.Parts.Add(part);
                        uoW.Complete();
                        MessageBox.Show("Деталь успешно добавлена");
                        CatalogVM.DefaultList = new ObservableCollection<CartPart>();
                        foreach (var p in uoW.Parts.GetAllParts())
                        {
                            catalogVM.DefaultList.Add(new CartPart(p, p.Amount));
                        }
                        CatalogVM.DefaultList.Clear();

                        foreach (var p in uoW.Parts.GetAllParts())
                        {
                            catalogVM.DefaultList.Add(new CartPart(p, p.Amount));
                        }
                        catalogVM.PartsList = CatalogVM.DefaultList;
                        ClearAllFields();
                    }
                    catch(Exception e)
                    {
                        MessageBox.Show("Заполните все поля");
                    }
                });

            }
            else if(partOperation == PartOperation.Edit && part != null)
            {
                PartButton = "Обновить запчасть";
                ImageButton = "Обновить картинку";
                HeaderText = "Обновить запчасть";

                Name = part.Name;
                Type = part.Type;
                Price = part.Price;
                Image = part.Image;
                Amount = part.Amount;
                UnitOfWork.UnitOfWork uoW = new UnitOfWork.UnitOfWork(new JPartsContext());
                Car car = uoW.Cars.Get(part.CarID);
                SelectedManufacturer = car.Manufacturer;
                SelectedModel = car.Model;
                SelectedYear = car.Year;

                AddPartCommand = new RelayCommand(o =>
                {
                    try
                    {
                        UnitOfWork.UnitOfWork uoW = new UnitOfWork.UnitOfWork(new JPartsContext());
                        var partToEdit = uoW.Parts.Get(part.PartID);
                        partToEdit.Name = Name;
                        partToEdit.Type = Type;
                        partToEdit.Price = Price;
                        partToEdit.Amount = Amount;
                        partToEdit.CarID = GetCar(SelectedManufacturer, SelectedModel, SelectedYear).CarID;
                        uoW.Complete();
                        MessageBox.Show("Деталь успешно обновлена");
                        CatalogVM.DefaultList.Clear();

                        foreach (var p in uoW.Parts.GetAllParts())
                        {
                            catalogVM.DefaultList.Add(new CartPart(p, p.Amount));
                        }
                        catalogVM.PartsList = CatalogVM.DefaultList;

                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Заполните все поля");
                    }
                });
            }

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

            

            AddCarCommand = new RelayCommand(o =>
            {
                AddCarWindow window = new AddCarWindow()
                {
                    DataContext = new AddCarViewModel(this, MainVM.CarsVM)
                };
                window.Show();
            });

            LoadManufacturers();
        }

        public async void LoadManufacturers()
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

        private void ClearAllFields()
        {
            Name = null;
            Type = null;
            Price = null;
            Availability = false;
            Image = null;
            Amount = null;
        }
    }
}
