using System;
using System.Collections.Generic;
using System.Text;

namespace JParts.MVVM.ViewModel
{
    class PopupViewModel : ViewModelBase
    {
        private string message;

        public string Message
        {
            get { return message; }
            set { message = value; OnPropertyChanged(); }
        }


        public PopupViewModel(string message)
        {
            Message = message;
        }
    }
}
