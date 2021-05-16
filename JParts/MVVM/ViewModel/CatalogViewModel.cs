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

        public CatalogViewModel()
        {
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

            uoW = new UnitOfWork.UnitOfWork(new JPartsContext());

            PartsList = uoW.Parts.GetAllParts();
        }
    }
}
