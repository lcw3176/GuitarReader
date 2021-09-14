using GuitarReader.Command;
using GuitarReader.Services;
using GuitarReader.Views;
using System.Windows.Input;

namespace GuitarReader.ViewModels
{
    class RecordViewModel : BaseViewModel
    {
        public ICommand RecordCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        private RecordService recordService = new RecordService();
        private bool isRun = false;

        public RecordViewModel()
        {
            RecordCommand = new RelayCommand(RecordExecuteMethod);
            SaveCommand = new RelayCommand(SaveExeucteMethod);
        }

        /// <summary>
        /// 악보 저장
        /// </summary>
        /// <param name="obj"></param>
        private void SaveExeucteMethod(object obj)
        {
            if (!isRun)
            {
                InputTiitleDialog inputTiitleDialog = new InputTiitleDialog();
                if (inputTiitleDialog.ShowDialog() == true && !string.IsNullOrEmpty(inputTiitleDialog.titleTextBox.Text))
                {
                    string sheetName = inputTiitleDialog.titleTextBox.Text;
                    recordService.SaveRecord(sheetName);
                }
            }
            
        }

        /// <summary>
        /// 녹음 시작
        /// </summary>
        /// <param name="gridSheet">그리드 오브젝트</param>
        private void RecordExecuteMethod(object gridSheet)
        {
            if (!isRun)
            {
                recordService.StartRecord(gridSheet);
                isRun = true;
            }

            else
            {
                recordService.StopRecord();
                isRun = false;
            }
            
        }
    }
}
