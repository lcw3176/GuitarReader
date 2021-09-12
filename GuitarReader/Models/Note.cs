namespace GuitarReader.Models
{
    public class Note
    {
        /// <summary>
        /// 기타 줄 종류
        /// </summary>
        public int stringPos { get; set; }
        
        /// <summary>
        /// 기타 프렛 위치
        /// </summary>
        public int fretPos { get; set; }
        
        /// <summary>
        /// 음표 박자, 길이
        /// </summary>
        public int beatLen { get; set; }
    }
}
