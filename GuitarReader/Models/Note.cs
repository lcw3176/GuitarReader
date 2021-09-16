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
        public string codeStr { get; set; }


        private void Generate()
        {
            switch (stringPos)
            {
                case 1:

            }
        }
    }

    public class CodeStr
    {
        //E3,F3,G3,
        //A3,B3,C4,
        //D4,E4,F4,
        //G4,A4,
        //B4,C5,D5,
        //E5,F5,G5,A5
    }
}
