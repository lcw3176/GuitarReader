using System.Collections.Generic;

namespace GuitarReader.Models
{
    public class Note
    {
        /// <summary>
        /// foreign key, Sheet Id 참조
        /// </summary>
        public int id { get; set; }

        private int _stringPos = -1;
        /// <summary>
        /// 기타 줄 종류
        /// </summary>
        public int stringPos 
        {
            get { return _stringPos; }
            set
            {
                _stringPos = value;
                //if(fretPos != -1)
                //{
                //    Generate();
                //}
            } 
        }

        private int _fretPos = -1;
        /// <summary>
        /// 기타 프렛 위치
        /// </summary>
        public int fretPos
        {
            get { return _fretPos; } 
            set
            {
                _fretPos = value;
                //if(stringPos != -1)
                //{
                //    Generate();
                //}
            }
        }
        
        /// <summary>
        /// 음표 박자, 길이
        /// </summary>
        public int beatLen { get; set; }

        /// <summary>
        /// 음표 문자열
        /// </summary>
        public string CodeStr { get { return codeStr; } }

        private string codeStr;

        public Dictionary<string, (int, int)> dict = new Dictionary<string, (int, int)>();
        public Dictionary<string, int> frequencyDict = new Dictionary<string, int>();

        private int[] c = { 16, 33, 65, 131, 262, 523, 1047, 2093, 4186 };
        private int[] cs = { 17, 35, 69, 139, 278, 554, 1109, 2218, 4435 };
        private int[] d = { 18, 37, 73, 147, 294, 587, 1175, 2349, 4699 };
        private int[] ds = { 20, 39, 78, 156, 311, 622, 1245, 2489, 4978 };
        private int[] e = { 21, 41, 82, 165, 330, 659, 1319, 2637, 5274 };
        private int[] f = { 22, 44, 87, 175, 355, 699, 1397, 2794, 5588 };
        private int[] fs = { 23, 46, 93, 185, 370, 740, 1475, 2960, 5920 };
        private int[] g = { 25, 49, 98, 196, 392, 784, 1568, 3136, 6272 };
        private int[] gs = { 26, 52, 104, 208, 425, 831, 1661, 3322, 6645 };
        private int[] a = { 28, 55, 110, 220, 450, 880, 1760, 3520, 7040 };
        private int[] asharp = { 29, 58, 117, 233, 466, 932, 1865 ,3729, 7459 };
        private int[] b = { 31 ,62 ,124 ,247 ,494 ,988 ,1976 ,3951 ,7902 };

        public Note()
        {

            for (int i = 2; i <= 4; i++)
            {
                if(i == 2)
                {
                    frequencyDict.Add("E" + i.ToString(), e[i]);
                    frequencyDict.Add("F" + i.ToString(), f[i]);
                    frequencyDict.Add("F#" + i.ToString(), fs[i]);
                    frequencyDict.Add("G" + i.ToString(), g[i]);
                    frequencyDict.Add("G#" + i.ToString(), gs[i]);

                    frequencyDict.Add("A" + i.ToString(), a[i]);
                    frequencyDict.Add("A#" + i.ToString(), asharp[i]);
                    frequencyDict.Add("B" + i.ToString(), b[i]);
                }

                else
                {
                    frequencyDict.Add("C" + i.ToString(), c[i]);
                    frequencyDict.Add("C#" + i.ToString(), cs[i]);
                    frequencyDict.Add("D" + i.ToString(), d[i]);
                    frequencyDict.Add("D#" + i.ToString(), ds[i]);
                    frequencyDict.Add("E" + i.ToString(), e[i]);
                    frequencyDict.Add("F" + i.ToString(), f[i]);
                    frequencyDict.Add("F#" + i.ToString(), fs[i]);
                    frequencyDict.Add("G" + i.ToString(), g[i]);
                    frequencyDict.Add("G#" + i.ToString(), gs[i]);

                    frequencyDict.Add("A" + i.ToString(), a[i]);
                    frequencyDict.Add("A#" + i.ToString(), asharp[i]);
                    frequencyDict.Add("B" + i.ToString(), b[i]);
                }
       
            }

            dict.Add("E2",  (6, 0));
            dict.Add("F2", (6, 1));
            dict.Add("F#2", (6, 2));
            dict.Add("G2", (6, 3));
            dict.Add("G#2", (6, 4));
            //dict.Add("A2", (6, 5));
            //dict.Add("A#2", (6, 6));

            dict.Add("A2",(5, 0));
            dict.Add("A#2", (5, 1));
            dict.Add("B2", (5, 2));
            dict.Add("C3",(5, 3));
            dict.Add("C#3",(5, 4));
            //dict.Add("D3",(5, 5));
            //dict.Add("D#3",(5, 6));

            dict.Add("D3",(4, 0));
            dict.Add("D#3",(4, 1));
            dict.Add("E3",(4, 2));
            dict.Add("F3",(4, 3));
            dict.Add("F#3",(4, 4));
            //dict.Add("G3",(4, 5));
            //dict.Add("G#3",(4, 6));

            dict.Add("G3",(3, 0));
            dict.Add("G#3",(3, 1));
            dict.Add("A3",(3, 2));
            dict.Add("A#3",(3, 3));
            //dict.Add("B3",(3, 4));
            //dict.Add("C4",(3, 5));
            //dict.Add("C#4",(3, 6));

            dict.Add("B3",(2, 0));
            dict.Add("C4",(2, 1));
            dict.Add("C#4",(2, 2));
            dict.Add("D4",(2, 3));
            dict.Add("D#4",(2, 4));
            //dict.Add("E4",(2, 5));
            //dict.Add("F4",(2, 6));

            dict.Add("E4",(1, 0));
            dict.Add("F4",(1, 1));
            dict.Add("F#4", (1, 2));
            dict.Add("G4",(1, 3));
            dict.Add("G#4",(1, 4));
            dict.Add("A4",(1, 5));
            dict.Add("A#4",(1, 6));
            dict.Add("B4",(1, 7));
        }
    }

    //E3,F3,G3,
    //A3,B3,C4,
    //D4,E4,F4,
    //G4,A4,
    //B4,C5,D5,
    //E5,F5,G5,A5
}
