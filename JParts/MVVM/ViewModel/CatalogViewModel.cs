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

        public UnitOfWork.UnitOfWork uoW;

        public List<Part> PartsList { get; set; }

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
                    DataContext = new AddPartViewModel()
                };
                window.Show();
            });

            uoW = new UnitOfWork.UnitOfWork(new JPartsContext());

            PartsList = uoW.Parts.GetAllParts();
        }
    }
}
