using GuitarReader.Models;
using GuitarReader.Util;

namespace GuitarReader.ViewModels
{
    class ParseViewModel : BaseViewModel
    {
        private int rowPosition;
        private Note note = new Note();
        private ParseFrequencyService parseFrequencyService = new ParseFrequencyService();

        public int RowPosition
        {
            get { return rowPosition; }
            set
            {
                rowPosition = value;
                OnPropertyChanged("RowPosition");
            }
        }

        private int columnPosition;
        public int ColumnPosition
        {
            get { return columnPosition; }
            set
            {
                columnPosition = value;
                OnPropertyChanged("ColumnPosition");
            }
        }

        private string codeStr;
        public string CodeStr
        {
            get { return codeStr; }
            set
            {
                codeStr = value;
                OnPropertyChanged("CodeStr");
            }
        }


        

        public ParseViewModel()
        {

            if (SerialUtil.isOpen())
            {
                SerialUtil.GetOwnership(this.GetType().Name);
                SerialUtil.dataReceiveEvent += SerialService_dataReceiveEvent;
            }
            
        }

        private void SerialService_dataReceiveEvent(string owner, int hz)
        {
            if(owner != this.GetType().Name)
            {
                return;
            }

            CodeStr = parseFrequencyService.Parse(hz);

            if (!string.IsNullOrEmpty(CodeStr))
            {
                RowPosition = note.dict[CodeStr].Item1 - 1;
                ColumnPosition = note.dict[CodeStr].Item2;

                note.stringPos = RowPosition;
                note.fretPos = columnPosition;
            }
        }

    }
}
