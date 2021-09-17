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
                if(fretPos != -1)
                {
                    Generate();
                }
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
                if(stringPos != -1)
                {
                    Generate();
                }
            }
        }
        
        /// <summary>
        /// 음표 박자, 길이
        /// </summary>
        public int beatLen { get; set; }

        /// <summary>
        /// 음표 문자열
        /// </summary>
        public string codeStr { get; private set; }

        private Dictionary<(int, int), string> dict = new Dictionary<(int, int), string>();

        public Note()
        {
            dict.Add((6, 0), "E3");
            dict.Add((6, 1), "F3");
            dict.Add((6, 2), "F#3");
            dict.Add((6, 3), "G3");
            dict.Add((6, 4), "G#3");
            dict.Add((6, 5), "A3");
            dict.Add((6, 6), "A#3");

            dict.Add((5, 0), "A3");
            dict.Add((5, 1), "A#3");
            dict.Add((5, 2), "B3");
            dict.Add((5, 3), "C4");
            dict.Add((5, 4), "C#4");
            dict.Add((5, 5), "D4");

            dict.Add((4, 0), "D4");
            dict.Add((4, 1), "D#4");
            dict.Add((4, 2), "E4");
            dict.Add((4, 3), "F4");
            dict.Add((4, 4), "F#4");

            dict.Add((3, 0), "G4");
            dict.Add((3, 1), "G#4");
            dict.Add((3, 2), "A4");
            dict.Add((3, 3), "A#4");

            dict.Add((2, 0), "B4");
            dict.Add((2, 1), "C5");
            dict.Add((2, 2), "C#5");
            dict.Add((2, 3), "D5");
            dict.Add((2, 4), "D#5");

            dict.Add((1, 0), "E5");
            dict.Add((1, 1), "F5");
            dict.Add((1, 2), "F#5");
            dict.Add((1, 3), "G5");
            dict.Add((1, 4), "G#5");
            dict.Add((1, 5), "A5");
            dict.Add((1, 6), "A#5");
            dict.Add((1, 7), "B5");
        }

        private void Generate()
        {
            codeStr = dict[(stringPos, fretPos)];
        }
    }

    //E3,F3,G3,
    //A3,B3,C4,
    //D4,E4,F4,
    //G4,A4,
    //B4,C5,D5,
    //E5,F5,G5,A5
}
