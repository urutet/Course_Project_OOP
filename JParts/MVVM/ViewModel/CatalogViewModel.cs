using JParts.DBContext;
using JParts.MVVM.Commands;
using JParts.MVVM.Model;
using JParts.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace JParts.MVVM.ViewModel
{
    class CatalogViewModel : ViewModelBase
    {

        public RelayCommand AddPartCommand { get; set; }

        public RelayCommand UpdatePartCommand { get; set; }

        public RelayCommand AddToCartCommand { get; set; }

        public RelayCommand DeletePartCommand { get; set; }

        public RelayCommand ShowPartCommand { get; set; }

        private Visibility _visibility;

        public Visibility visibility
        {
            get { return _visibility; }
            set { _visibility = value; OnPropertyChanged(); }
        }



        //Car filter
        private List<string> manufacturer;
        public List<string> Manufacturer
        {
            get => manufacturer;
            set { manufacturer = value; OnPropertyChanged(); }
        }

        private List<string> model;
        public List<string> Model
        {
            get => model;
            set { model = value; OnPropertyChanged(); }
        }

        private List<int?> year;
        public List<int?> Year
        {
            get => year;
            set { year = value; OnPropertyChanged(); }
        }

        private string _selectedManufacturer;

        public string SelectedManufacturer
        {
            get { return _selectedManufacturer; }
            set
            {
                _selectedManufacturer = value;
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
            set { _selectedYear = value; FilterExpressionChanged(); OnPropertyChanged(); }
        }

        public UnitOfWork.UnitOfWork uoW;

        private ObservableCollection<CartPart> partsList;
        public ObservableCollection<CartPart> PartsList { get => partsList; set { partsList = value; OnPropertyChanged(); } }



        private ObservableCollection<CartPart> defaultList;
        public ObservableCollection<CartPart> DefaultList { get => defaultList; set { defaultList = value; OnPropertyChanged(); } }

        private CartPart _selectedPart;

        public CartPart SelectedPart
        {
            get => _selectedPart;
            set
            {
                _selectedPart = value;
                OnPropertyChanged();
            }
        }

        private string _searchExpression;
        public string SearchExpression
        {
            get => _searchExpression;
            set
            {
                _searchExpression = value;
                OnPropertyChanged();
                SearchExpressionChanged();
            }
        }

        private ObservableCollection<CartPart> partsToAdd;

        public ObservableCollection<CartPart> PartsToAdd
        {
            get { return partsToAdd; }
            set { partsToAdd = value; OnPropertyChanged(); }
        }


        public CatalogViewModel(MainViewModel mainViewModel)
        {
            PartsToAdd = new ObservableCollection<CartPart>();
            DefaultList = new ObservableCollection<CartPart>();
            PartsList = new ObservableCollection<CartPart>();

            if (mainViewModel.AuthorisedClient.IsAdmin == true)
                visibility = Visibility.Visible;
            else
                visibility = Visibility.Collapsed;

            LoadManufacturers();


            AddPartCommand = new RelayCommand(o => 
            {
                AddPartWindow window = new AddPartWindow()
                {
                    DataContext = new AddPartViewModel(this, Enums.PartOperation.Add, null, mainViewModel)
                };
                window.Show();
            });

            UpdatePartCommand = new RelayCommand(o =>
            {
                if (o != null)
                {
                    var UpdatedPart = o as CartPart;
                    AddPartWindow window = new AddPartWindow()
                    {
                        DataContext = new AddPartViewModel(this, Enums.PartOperation.Edit, UpdatedPart.Part, mainViewModel)
                    };
                    window.Show();
                }


            });

            ShowPartCommand = new RelayCommand(o =>
            {
                if (o != null)
                {
                    var UpdatedPart = o as CartPart;
                    ShowPartWindow window = new ShowPartWindow()
                    {
                        DataContext = new AddPartViewModel(this, Enums.PartOperation.Edit, UpdatedPart.Part, mainViewModel)
                    };
                    window.Show();
                }
            });

            AddToCartCommand = new RelayCommand(o =>
            {
                if (o != null)
                {
                    var _partToAdd = o as CartPart;
                    if (_partToAdd.Amount <= 0 || (_partToAdd.Part.Amount - _partToAdd.Amount) < 0)
                        MessageBox.Show("Нет в наличии");
                    else
                    {
                        PartsToAdd.Add(_partToAdd);
                        mainViewModel.PartsToAdd = PartsToAdd;
                        MessageBox.Show("Деталь добавлена в корзину");
                    }
                }
            });

            DeletePartCommand = new RelayCommand(o =>
            {
                if (SelectedPart != null)
                {
                    uoW.Parts.Remove(SelectedPart.Part);
                    uoW.Complete();

                    PartsList = new ObservableCollection<CartPart>();
                    DefaultList = new ObservableCollection<CartPart>();

                    foreach(var p in uoW.Parts.GetAllParts())
                    {
                        PartsList.Add(new CartPart(p, p.Amount));
                        DefaultList.Add(new CartPart(p, p.Amount));
                    }
                }
                else
                {
                    MessageBox.Show("Не выбран элемент для удаления");
                }

            });

            uoW = new UnitOfWork.UnitOfWork(new JPartsContext());

            foreach (var p in uoW.Parts.GetAllParts())
            {
                DefaultList.Add(new CartPart(p, p.Amount));
            }

            PartsList = DefaultList;

        }

        private void SearchExpressionChanged()
        {
            if (SearchExpression == string.Empty)
                PartsList = DefaultList;
            else
            {
                PartsList = new ObservableCollection<CartPart>(DefaultList.Where(p => p.Part.Name.ToLower().Contains(SearchExpression.ToLower())).ToList());
            }
        }

        private void FilterExpressionChanged()
        {
            if (SelectedYear == null)
                PartsList = DefaultList;
            else
            {
                Car car = uoW.Cars.GetCar(SelectedManufacturer, SelectedModel, SelectedYear);
                PartsList = new ObservableCollection<CartPart>(DefaultList.Where(p => p.Part.CarID == car.CarID).ToList());
            }
        }

        public async void LoadManufacturers()
        {
            UnitOfWork.UnitOfWork uoW = new UnitOfWork.UnitOfWork(new JPartsContext());
            Manufacturer = await uoW.Cars.GetAllManufacturers();
        }
        private async void LoadModels()
        {
            UnitOfWork.UnitOfWork uoW = new UnitOfWork.UnitOfWork(new JPartsContext());
            Model = await uoW.Cars.GetManufacturerModels(SelectedManufacturer);
        }
        private async void LoadYears()
        {
            UnitOfWork.UnitOfWork uoW = new UnitOfWork.UnitOfWork(new JPartsContext());
            Year = await uoW.Cars.GetModelsYears(SelectedManufacturer, SelectedModel);
        }
    }
}
