using GuitarReader.Command;
using GuitarReader.Util;
using GuitarReader.Views;
using System.Windows.Controls;
using System.Windows.Input;

namespace GuitarReader.ViewModels
{
    class RecordViewModel : BaseViewModel
    {
        public ICommand RecordCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        private RecordUtil recordUtil = new RecordUtil();
        private bool isRun = false;
        private AnimUtil animUtil = null;

        public RecordViewModel()
        {
            recordUtil.recordAddEvent += RecordService_recordAddEvent;
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
                InputTiitleDialogView inputTiitleDialog = new InputTiitleDialogView();
                if (inputTiitleDialog.ShowDialog() == true && !string.IsNullOrEmpty(inputTiitleDialog.titleTextBox.Text))
                {
                    string sheetName = inputTiitleDialog.titleTextBox.Text;
                    recordUtil.SaveRecord(sheetName);
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
                if(animUtil == null)
                {
                    animUtil = new AnimUtil(gridSheet as Grid);
                }

                recordUtil.StartRecord(gridSheet);
                isRun = true;
            }

            else
            {
                recordUtil.StopRecord();
                isRun = false;
            }
            
        }

        /// <summary>
        /// 음표 녹음 감지 시 애니메이션 그리기
        /// </summary>
        /// <param name="stringPos">줄 종류</param>
        /// <param name="fretPos">프렛 위치</param>
        private void RecordService_recordAddEvent(int stringPos, int fretPos)
        {
            if (animUtil == null || !isRun)
            {
                return;
            }

            animUtil.AddTab(stringPos, fretPos);

        }

    }
}
