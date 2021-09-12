using GuitarReader.Models;
using System.Collections.ObjectModel;

namespace GuitarReader.ViewModels
{
    class SheetListViewModel : BaseViewModel
    {
        public ObservableCollection<Sheet> SheetsCollection { get; set; } = new ObservableCollection<Sheet>();

        public SheetListViewModel()
        {

        }
    }
}
