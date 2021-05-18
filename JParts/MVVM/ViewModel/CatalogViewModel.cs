using JParts.DBContext;
using JParts.MVVM.Commands;
using JParts.MVVM.Model;
using JParts.Windows;
using System;
using System.Collections.Generic;
using System.Text;

namespace JParts.MVVM.ViewModel
{
    class CatalogViewModel : ViewModelBase
    {

        public RelayCommand AddPartCommand { get; set; }

        public RelayCommand UpdatePartCommand { get; set; }

        public RelayCommand AddToCartCommand { get; set; }


        public UnitOfWork.UnitOfWork uoW;

        private List<Part> partsList;
        public List<Part> PartsList { get => partsList; set { partsList = value; OnPropertyChanged(); } }

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

        private List<Part> partsToAdd;

        public List<Part> PartsToAdd
        {
            get { return partsToAdd; }
            set { partsToAdd = value; OnPropertyChanged(); }
        }

        public CatalogViewModel(MainViewModel mainViewModel)
        {
            PartsToAdd = new List<Part>();

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

            uoW = new UnitOfWork.UnitOfWork(new JPartsContext());

            PartsList = uoW.Parts.GetAllParts();
        }
    }
}
