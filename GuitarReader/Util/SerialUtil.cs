using System;
using System.IO.Ports;

namespace GuitarReader.Util
{
    static class SerialUtil
    {
        private static SerialPort serialPort;
        private static string owner = string.Empty;

        public delegate void DataReceiveEvent(string owner, int hz);
        public static event DataReceiveEvent dataReceiveEvent;

        public static void GetOwnership(string name)
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
                if (isOpen())
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

            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public static bool isOpen()
        {
            if (serialPort != null && serialPort.IsOpen)
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

            if (int.TryParse(serialPort.ReadLine(), out int hz))
            {
                if (hz >= 10)
                {
                    dataReceiveEvent(owner, hz);
                }
            }
        }

        /// <summary>
        /// 임시 연산 속도 테스트 코드
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void DataReceived_Test(object sender, SerialDataReceivedEventArgs e)
        {
            const int LENGTH = 256;
            const int sample_freq = 8919;
            int[] rawData = new int[LENGTH];
            int index = 0;

            while (index < LENGTH)
            {
                if (int.TryParse(serialPort.ReadLine(), out int data))
                {
                    rawData[index] = data;
                }

                else
                {
                    rawData[index] = 0;
                }

                index++;
            }
            

            long sum = 0;
            long sum_old = 0;
            int freq_per = 0;
            byte pd_state = 0;
            int thresh = 0;
            int period = 0;

            for (int i = 0; i < LENGTH; i++)
            {
                sum_old = sum;
                sum = 0;

                for (int k = 0; k < LENGTH - i; k++)
                {
                    sum += (rawData[k] - 512) * (rawData[k + i] - 512) / 1024;
                }


                // Peak Detect State Machine
                if (pd_state == 2 && (sum - sum_old) <= 0)
                {
                    period = i;
                    break;
                }

                if (pd_state == 1 && (sum > thresh) && (sum - sum_old) > 0)
                {
                    pd_state = 2;
                }

                if (i == 0)
                {
                    thresh = (int)(sum * 0.5);
                    pd_state = 1;
                }
            }

            // Frequency identified in Hz
            if (period != 0)
            {
                freq_per = sample_freq / period;
            }

            Console.Write(freq_per);
        }
    }
}
