﻿using GuitarReader.Command;
using GuitarReader.Models;
using GuitarReader.Services;
using GuitarReader.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GuitarReader.ViewModels
{
    class SheetListViewModel : BaseViewModel
    {
        public ObservableCollection<Sheet> SheetsCollection { get; set; } = new ObservableCollection<Sheet>();
        public ICommand PlaySheetCommand { get; private set; }
        public ICommand EditSheetCommand { get; private set; }

        private SheetService sheetService = new SheetService();

        public SheetListViewModel()
        {
            PlaySheetCommand = new RelayCommand(PlaySheetExecuteMethod);
            EditSheetCommand = new RelayCommand(EditSheetExecuteMethod);
            Refresh();
        }

        private void Refresh()
        {
            SheetsCollection.Clear();
            sheetService.ReadAll().ForEach((i) =>
            {
                i.PlaySheetCommand = this.PlaySheetCommand;
                i.EditSheetCommand = this.EditSheetCommand;
                SheetsCollection.Add(i);
            });
        }

        /// <summary>
        /// 클릭 시 악보 재생 커맨드
        /// </summary>
        /// <param name="sheetName">악보 이름</param>
        private void PlaySheetExecuteMethod(object sheetName)
        {
            Sheet sheet = sheetService.ReadByName(sheetName.ToString());
            PlayView view = new PlayView(sheet.id);
            PlayViewModel dataContext = view.DataContext as PlayViewModel;

            view.Closing += ((sender, e) =>
            {
                dataContext.StopCommand.Execute(null);
            });

            view.ShowDialog();

            
        }

        /// <summary>
        /// 클릭 시 악보 편집 커맨드
        /// </summary>
        /// <param name="sheetName">악보 이름</param>
        private void EditSheetExecuteMethod(object sheetName)
        {
            Sheet sheet = sheetService.ReadByName(sheetName.ToString());
            EditDialogView view = new EditDialogView(sheet.id);
            EditViewModel dataContext = view.DataContext as EditViewModel;

            view.Closing += ((sender, e) =>
            {
                dataContext.SaveCurrentInfo();
                Refresh();
            });

            view.ShowDialog();
        }
    }
}
