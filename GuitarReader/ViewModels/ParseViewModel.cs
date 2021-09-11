using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace GuitarReader.ViewModels
{
    class ParseViewModel : BaseViewModel
    {
        private int rowPosition;
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

        

        public ParseViewModel()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.5);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Random random = new Random();
            RowPosition = random.Next(0, 6);
            ColumnPosition = random.Next(0, 10);
        }
    }
}
