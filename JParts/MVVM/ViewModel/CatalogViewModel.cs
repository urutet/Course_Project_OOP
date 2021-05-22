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
                SearchExpressionChanged();
                OnPropertyChanged();
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
                    var _partToAdd = o as CartPart;

                    PartsToAdd.Add(_partToAdd);
                    mainViewModel.PartsToAdd = PartsToAdd;
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
                PartsList = new ObservableCollection<CartPart>(DefaultList.Where(p => p.Part.Name.ToLower().Contains(SearchExpression)).ToList());
            }
        }
    }
}
