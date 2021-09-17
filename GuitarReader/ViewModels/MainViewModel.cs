using System;

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
                    case 2:
                        SelectedViewModel = new RecordViewModel();
                        break;
                    case 3:
                        SelectedViewModel = new SheetListViewModel();
                        break;
                    case 4:
                        SelectedViewModel = new PlayViewModel();
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
