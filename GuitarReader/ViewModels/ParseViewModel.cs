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
        DispatcherTimer timer;
        private int testCount = 0;

        public ParseViewModel()
        {
            playservice = PlayService.GetInstacne();
            playservice.Open();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.5);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Random random = new Random();
            RowPosition = random.Next(0, 6);
            ColumnPosition = random.Next(0, 6);
            note.stringPos = RowPosition + 1;
            note.fretPos = columnPosition + 1;
            CodeStr = note.CodeStr;
            playservice.Play(CodeStr);
            testCount++;

            if(testCount >= 10)
            {
                timer.Stop();
            }
        }
    }
}
