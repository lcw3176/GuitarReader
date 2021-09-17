using GuitarReader.Command;
using GuitarReader.Models;
using GuitarReader.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GuitarReader.ViewModels
{
    class SheetListViewModel : BaseViewModel
    {
        public ObservableCollection<Sheet> SheetsCollection { get; set; } = new ObservableCollection<Sheet>();
        public ICommand LoadSheetCommand { get; private set; }

        private SheetService sheetService = new SheetService();

        public SheetListViewModel()
        {
            LoadSheetCommand = new RelayCommand(LoadSheetExecuteMethod);

            sheetService.ReadAll().ForEach((i) =>
            {
                i.LoadSheetCommand = this.LoadSheetCommand;
                SheetsCollection.Add(i);
            });
        }

        private void LoadSheetExecuteMethod(object obj)
        {
            Sheet sheet = sheetService.ReadByName(obj.ToString());
            EnqueuePlayList(sheet.id);
        }
    }
}
