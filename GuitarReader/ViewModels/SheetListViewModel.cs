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
        public ICommand PlaySheetCommand { get; private set; }
        public ICommand EditSheetCommand { get; private set; }

        private SheetService sheetService = new SheetService();

        public SheetListViewModel()
        {
            PlaySheetCommand = new RelayCommand(PlaySheetExecuteMethod);
            EditSheetCommand = new RelayCommand(EditSheetExecuteMethod);
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
        }

        /// <summary>
        /// 클릭 시 악보 편집 커맨드
        /// </summary>
        /// <param name="sheetName">악보 이름</param>
        private void EditSheetExecuteMethod(object sheetName)
        {
            
        }
    }
}
