using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;

namespace GuitarReader.ViewModels
{
    class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected static int playId;

        protected void OnPropertyChanged(string param)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(param));
            }
        }

        /// <summary>
        /// 악보 목록에서 악보 선택 시 작동
        /// </summary>
        /// <param name="music"></param>
        protected void EnqueuePlayList(int id)
        {
            playId = id;
        }

    }
}
