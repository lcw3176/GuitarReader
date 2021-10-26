using GuitarReader.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuitarReader.ViewModels
{
    class HomeViewModel : BaseViewModel
    {
        private ObservableCollection<SerialDevice> serialDevices = new ObservableCollection<SerialDevice>();

        public ObservableCollection<SerialDevice> SerialDevices
        {
            get { return serialDevices; }
            set
            {
                serialDevices = value;
                OnPropertyChanged("SerialDevices");
            }
        }

        private SerialDevice selectedSerial;

        public SerialDevice SelectedSerial
        {
            get { return selectedSerial; }
            set
            {
                selectedSerial = value;
                OnPropertyChanged("SelectedSerial");
            }
        }

        private int selectedBaudRate;

        public int SelectedBaudRate
        {
            get { return selectedBaudRate; }
            set
            {
                selectedBaudRate = value;
                OnPropertyChanged("SelectedBaudRate");
            }
        }

        enum BaudRate
        {
            Normal, // 9600 
            Fast, // 115200
        };

        public HomeViewModel()
        {
            /// 포트 뷰 테스트
            for(int i = 0; i < 5; i++)
            {
                serialDevices.Add(new SerialDevice()
                {
                    name = "COM" + i,
                });

            }

            SelectedSerial = serialDevices[0];
            SelectedBaudRate = (int)BaudRate.Fast;
        }
    }
}
