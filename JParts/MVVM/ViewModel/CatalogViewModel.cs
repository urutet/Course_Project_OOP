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


        public UnitOfWork.UnitOfWork uoW;

        private ObservableCollection<Part> partsList;
        public ObservableCollection<Part> PartsList { get => partsList; set { partsList = value; OnPropertyChanged(); } }

        private ObservableCollection<Part> defaultList;
        public ObservableCollection<Part> DefaultList { get => defaultList; set { defaultList = value; OnPropertyChanged(); } }

        private Part _selectedPart;

        public Part SelectedPart
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
                SearchExpressionChanged();
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Part> partsToAdd;

        public ObservableCollection<Part> PartsToAdd
        {
            get { return partsToAdd; }
            set { partsToAdd = value; OnPropertyChanged(); }
        }

        public CatalogViewModel(MainViewModel mainViewModel)
        {
            PartsToAdd = new ObservableCollection<Part>();

            AddPartCommand = new RelayCommand(o => 
            {
                AddPartWindow window = new AddPartWindow()
                {
                    DataContext = new AddPartViewModel(this, Enums.PartOperation.Add)
                };
                window.Show();
            });

            UpdatePartCommand = new RelayCommand(o =>
            {
                if (o != null)
                {
                    var UpdatedPart = o as Part;
                    AddPartWindow window = new AddPartWindow()
                    {
                        DataContext = new AddPartViewModel(this, Enums.PartOperation.Edit, UpdatedPart)
                    };
                    window.Show();
                }


            });

            AddToCartCommand = new RelayCommand(o =>
            {
                if (o != null)
                {
                    var _partToAdd = o as Part;
                    PartsToAdd.Add(_partToAdd);
                    mainViewModel.PartsToAdd = PartsToAdd;
                }
            });

            DeletePartCommand = new RelayCommand(o =>
            {
                if (SelectedPart != null)
                {
                    uoW.Parts.Remove(SelectedPart);
                    uoW.Complete();

                    DefaultList = new ObservableCollection<Part>(uoW.Parts.GetAllParts());
                    PartsList = new ObservableCollection<Part>(uoW.Parts.GetAllParts());
                }
                else
                {
                    MessageBox.Show("Не выбран элемент для удаления");
                }

            });

            uoW = new UnitOfWork.UnitOfWork(new JPartsContext());

            PartsList = new ObservableCollection<Part>(uoW.Parts.GetAllParts());

            DefaultList = new ObservableCollection<Part>(uoW.Parts.GetAllParts());
        }

        private void SearchExpressionChanged()
        {
            if (SearchExpression == string.Empty)
                PartsList = DefaultList;
            else
            {
                PartsList = new ObservableCollection<Part>(DefaultList.Where(p => p.Name.ToLower().Contains(SearchExpression)).ToList());
            }
        }
    }
}
