using GuitarReader.Models;
using System;
using System.Collections.Generic;
using System.Windows.Threading;

namespace GuitarReader.Services
{
    class RecordService
    {
        private int count = 0;
        private int[] arr = new int[] { 7,7,7,7,5,3,3,2,0,0,3,7};
        private List<Note> noteList = new List<Note>();
        DispatcherTimer testTimer = new DispatcherTimer();
        public delegate void RecordAddEvent(int stringPos, int fretPos);
        public event RecordAddEvent recordAddEvent;

        public RecordService()
        {
            testTimer.Interval = TimeSpan.FromSeconds(1);
            testTimer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            recordAddEvent(1, arr[count]);
            noteList.Add(new Note()
            {
                fretPos = arr[count],
                stringPos = 1,
                beatLen = DateTime.Now.Second,
            });

            count++;

            if (count >= arr.Length - 1)
            {
                count = 0;
            }
        }

        /// <summary>
        /// 녹음 시작
        /// </summary>
        /// <param name="obj"></param>
        public void StartRecord(object obj)
        {
            noteList.Clear();
            testTimer.Start();

        }

        /// <summary>
        /// 녹음 중지
        /// </summary>
        public void StopRecord()
        {
            testTimer.Stop();
            
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

            foreach(var i in noteList)
            {
                i.id = temp.id;
                noteService.Insert(i);
            }
        }

    }
}
