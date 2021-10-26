using System;
using System.IO.Ports;
using System.Text;

namespace GuitarReader.Services
{
    class SerialService
    {
        private static SerialPort serialPort;
        public delegate void DataReceiveEvent(double hz, double volume);
        public event DataReceiveEvent dataReceiveEvent;


        /// <summary>
        /// 현재 연결된 포트 이름 가져오기
        /// </summary>
        /// <returns></returns>
        public string[] GetSerialPortNames()
        {
            return SerialPort.GetPortNames();
        }

        /// <summary>
        /// 포트 연결
        /// </summary>
        /// <param name="portName"></param>
        /// <param name="baudRate"></param>
        /// <returns></returns>
        public bool ConnectSerialPort(string portName, int baudRate)
        {
            try
            {
                if(serialPort.IsOpen)
                {
                    serialPort.DataReceived -= SerialPort_DataReceived;
                    serialPort.Close();                    
                }

                serialPort = new SerialPort(portName);
                serialPort.BaudRate = baudRate;
                serialPort.DataBits = 8;
                serialPort.StopBits = StopBits.One;
                serialPort.Parity = Parity.None;

                serialPort.Open();
                serialPort.DataReceived += SerialPort_DataReceived;

                return true;
            }

            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        /// <summary>
        /// 데이터 리시브 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] buffer = new byte[11];

            int offset = 0;

            while (offset <= buffer.Length)
            {
                offset = serialPort.Read(buffer, offset, buffer.Length);
            }

            string[] data = Encoding.ASCII.GetString(buffer).Split(',');
            double hz = double.Parse(data[0]);
            double volume = double.Parse(data[1]);

            dataReceiveEvent(hz, volume);
        }
    }
}
