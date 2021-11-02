using GuitarReader.Models;
using GuitarReader.Services;
using System;
using System.Collections.Generic;

namespace GuitarReader.Util
{
    class RecordUtil
    {

        private List<Note> noteList = new List<Note>();
        private Note note = new Note();
        private ParseFrequencyUtil parseFrequencyUtil = new ParseFrequencyUtil();

        public delegate void RecordAddEvent(int stringPos, int fretPos);
        public event RecordAddEvent recordAddEvent;

        public RecordUtil()
        {
            if (SerialUtil.isOpen())
            {
                SerialUtil.GetOwnership(this.GetType().Name);
                SerialUtil.dataReceiveEvent += SerialService_dataReceiveEvent;
            }
        }

        private void SerialService_dataReceiveEvent(string owner, int hz)
        {
            if (owner != this.GetType().Name)
            {
                return;
            }

            string codeStr = parseFrequencyUtil.Parse(hz);
            if (!string.IsNullOrEmpty(codeStr))
            {
                recordAddEvent(note.dict[codeStr].Item1, note.dict[codeStr].Item2);

                noteList.Add(new Note()
                {
                    stringPos = note.dict[codeStr].Item1,
                    fretPos = note.dict[codeStr].Item2,
                    beatLen = DateTime.Now.Second,
                });
            }

        }


        /// <summary>
        /// 녹음 시작
        /// </summary>
        /// <param name="obj"></param>
        public void StartRecord(object obj)
        {
            noteList.Clear();

        }

        /// <summary>
        /// 녹음 중지
        /// </summary>
        public void StopRecord()
        {

        }

        /// <summary>
        /// 녹음 정보 저장
        /// </summary>
        /// <param name="sheetName">저장될 이름</param>
        public void SaveRecord(string sheetName)
        {
            Sheet sheet = new Sheet();
            sheet.created = DateTime.Now.ToString();
            sheet.lastModified = DateTime.Now.ToString();
            sheet.name = sheetName;

            SheetService sheetService = new SheetService();
            sheetService.Insert(sheet);
            Sheet temp = sheetService.ReadMostRecent();

            NoteService noteService = new NoteService();

            foreach (var i in noteList)
            {
                i.id = temp.id;
                noteService.Insert(i);
            }
        }
    }
}
