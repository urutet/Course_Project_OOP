using JParts.MVVM.Commands;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;

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
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public int? Year { get; set; }

        public RelayCommand AddImage { get; set; }

        public AddPartViewModel()
        {
            AddImage = new RelayCommand(o =>
            {
                OpenFileDialog openDialog = new OpenFileDialog();
                openDialog.Filter = "Image files (*.png;*.jpeg; *.jpg)|*.png;*.jpeg; *.jpg";
                openDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                if(openDialog.ShowDialog() == true)
                {
                    Image = openDialog.FileName;
                }
            });
        }

    }
}
