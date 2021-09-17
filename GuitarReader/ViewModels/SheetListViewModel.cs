using GuitarReader.Command;
using GuitarReader.Models;
using GuitarReader.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows.Input;
using Toub.Sound.Midi;

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
