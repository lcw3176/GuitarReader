using GuitarReader.Models;
using GuitarReader.Services;
using System;
using System.Windows.Threading;

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


        PlayService playservice;

        public ParseViewModel()
        {
            playservice = PlayService.GetInstacne();
            playservice.Open();

            if (SerialService.isOpen())
            {
                SerialService.dataReceiveEvent += SerialService_dataReceiveEvent;
            }
            
        }

        private void SerialService_dataReceiveEvent(int hz)
        {
            CodeStr = parseFrequencyService.Parse(hz);

            if (!string.IsNullOrEmpty(CodeStr))
            {
                RowPosition = note.dict[CodeStr].Item1 - 1;
                ColumnPosition = note.dict[CodeStr].Item2;

                Console.WriteLine(RowPosition);
                Console.WriteLine(ColumnPosition);
                note.stringPos = RowPosition + 1;
                note.fretPos = columnPosition + 1;
            }
            

            //playservice.Play(CodeStr);
        }

    }
}
