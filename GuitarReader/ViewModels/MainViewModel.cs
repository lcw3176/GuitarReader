using System;
using System.Windows.Controls;

namespace GuitarReader.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        private BaseViewModel selectedViewModel;
        public BaseViewModel SelectedViewModel
        {
            get { return selectedViewModel; }
            set
            {
                selectedViewModel = value;
                OnPropertyChanged("SelectedViewModel");
            }
        }

        private int viewIndex;
        public int ViewIndex
        {
            get { return viewIndex; }
            set
            {
                viewIndex = value;
                OnPropertyChanged("ViewIndex");

                switch (ViewIndex)
                {
                    case 0:
                        SelectedViewModel = new HomeViewModel();
                        break;
                    case 1:
                        SelectedViewModel = new ParseViewModel();
                        break;
                    default:
                        break;
                }
            }
        }

        public MainViewModel()
        {
            SelectedViewModel = new HomeViewModel();
            ViewIndex = 0;
        }
    }
}
