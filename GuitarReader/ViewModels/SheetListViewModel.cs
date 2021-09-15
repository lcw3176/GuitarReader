using GuitarReader.Models;
using GuitarReader.Services;
using System.Collections.ObjectModel;

namespace GuitarReader.ViewModels
{
    class SheetListViewModel : BaseViewModel
    {
        public ObservableCollection<Sheet> SheetsCollection { get; set; } = new ObservableCollection<Sheet>();

        public SheetListViewModel()
        {
            SheetService sheetService = new SheetService();
            sheetService.ReadAll().ForEach(i => SheetsCollection.Add(i));
        }
    }
}
