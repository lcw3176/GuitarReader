using System;
using System.Collections;
using System.IO.Ports;
using System.Text;

namespace GuitarReader.Services
{
    static class SerialService
    {
        private static SerialPort serialPort;
        private static string owner = string.Empty;

        public delegate void DataReceiveEvent(string owner, int hz);
        public static event DataReceiveEvent dataReceiveEvent;

        public static void CheckIn(string name)
        {
            owner = name;
        }

        /// <summary>
        /// 현재 연결된 포트 이름 가져오기
        /// </summary>
        /// <returns></returns>
        public static string[] GetSerialPortNames()
        {
            return SerialPort.GetPortNames();
        }

        /// <summary>
        /// 포트 연결
        /// </summary>
        /// <param name="portName"></param>
        /// <param name="baudRate"></param>
        /// <returns></returns>
        public static bool TryConnect(string portName, int baudRate)
        {
            try
            {
                if(isOpen())
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

        public static bool isOpen()
        {
            if(serialPort != null && serialPort.IsOpen)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 데이터 리시브 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

            if(int.TryParse(serialPort.ReadLine(), out int hz))
            {
                if (hz >= 10)
                {
                    dataReceiveEvent(owner, hz);
                }
            }
        }
    }
}
