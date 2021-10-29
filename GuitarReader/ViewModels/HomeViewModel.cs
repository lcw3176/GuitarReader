using GuitarReader.Command;
using GuitarReader.Models;
using GuitarReader.Services;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace GuitarReader.ViewModels
{
    class HomeViewModel : BaseViewModel
    {
        private ObservableCollection<SerialDevice> serialDevices = new ObservableCollection<SerialDevice>();
        private ObservableCollection<SerialBaudRate> serialBaudRates = new ObservableCollection<SerialBaudRate>();

        private SerialService serialService = new SerialService();
        public ICommand ConnectCommand { get; set; }
        public ObservableCollection<SerialDevice> SerialDevices
        {
            get { return serialDevices; }
            set
            {
                serialDevices = value;
                OnPropertyChanged("SerialDevices");
            }
        }

        public ObservableCollection<SerialBaudRate> SerialBaudRates
        {
            get { return serialBaudRates; }
            set
            {
                serialBaudRates = value;
                OnPropertyChanged("SerialBaudRates");
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

        private SerialBaudRate selectedBaudRate;

        public SerialBaudRate SelectedBaudRate
        {
            get { return selectedBaudRate; }
            set
            {
                selectedBaudRate = value;
                OnPropertyChanged("SelectedBaudRate");
            }
        }


        public HomeViewModel()
        {
            foreach(string i in serialService.GetSerialPortNames())
            {

                serialDevices.Add(new SerialDevice()
                {
                    name = i,
                });
            }

            if(serialDevices.Count != 0)
            {
                SelectedSerial = serialDevices[0];
            }
            
            foreach(int i in SerialBaudRate.BaudRate)
            {
                SerialBaudRates.Add(new SerialBaudRate()
                {
                    rate = i,
                });
            }
            SelectedBaudRate = SerialBaudRates[0];
            ConnectCommand = new RelayCommand(ConnectExecuteMethod);
        }

        private void ConnectExecuteMethod(object obj)
        {
            if(SelectedSerial != null)
            {
                if (serialService.TryConnect(SelectedSerial.name, SelectedBaudRate.rate))
                {
                    MessageBox.Show("연결이 완료되었습니다.");
                }

                else
                {
                    MessageBox.Show("다시 시도해 주세요.");
                }
            }
            
        }
    }
}
