using GuitarReader.Command;
using GuitarReader.Services;
using System;
using System.Windows.Input;

namespace GuitarReader.ViewModels
{
    class RecordViewModel : BaseViewModel
    {
        public ICommand RecordCommand { get; set; }
        private RecordService recordService = new RecordService();
        private bool isRun = false;

        public RecordViewModel()
        {
            RecordCommand = new RelayCommand(RecordExecuteMethod);
        }


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
