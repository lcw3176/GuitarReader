using System;
using System.Collections.Generic;

namespace GuitarReader.Models
{
    public class Sheet
    {
        /// <summary>
        /// 인덱스 primary key, auto increment
        /// </summary>
        public int id { get; set; }
        public List<Note> notes { get; set; }

        /// <summary>
        /// 악보 이름
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 처음 만든 날짜
        /// </summary>
        public DateTime created { get; set; }

        /// <summary>
        /// 마지막 수정 날짜
        /// </summary>
        public DateTime lastModified { get; set; }
    }
}
