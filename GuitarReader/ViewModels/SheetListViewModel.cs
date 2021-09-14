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
            DBService<Sheet> service = new DBService<Sheet>();
            foreach (var i in service.Read("sheet", "*"))
            {
                SheetsCollection.Add(i);
            }
        }
    }
}
