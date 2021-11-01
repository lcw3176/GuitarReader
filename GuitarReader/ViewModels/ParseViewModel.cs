using GuitarReader.Models;
using GuitarReader.Services;
using GuitarReader.Util;
using System;
using System.Windows.Threading;

namespace GuitarReader.ViewModels
{
    class ParseViewModel : BaseViewModel
    {
        private int rowPosition;
        private Note note = new Note();
        private ParseFrequencyUtil parseFrequencyUtil = new ParseFrequencyUtil();

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

            CodeStr = parseFrequencyUtil.Parse(hz);

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
