using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace GuitarReader.Models
{
    public class Sheet
    {
        /// <summary>
        /// 인덱스 primary key, auto increment
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 악보 이름
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 처음 만든 날짜
        /// </summary>
        public string created { get; set; }

        /// <summary>
        /// 마지막 수정 날짜
        /// </summary>
        public string lastModified { get; set; }

        /// <summary>
        /// 시트 클릭 시 재생
        /// </summary>
        public ICommand PlaySheetCommand { get; set; }
        
        /// <summary>
        /// 시트 클릭 시 편집
        /// </summary>
        public ICommand EditSheetCommand { get; set; }
    }
}
