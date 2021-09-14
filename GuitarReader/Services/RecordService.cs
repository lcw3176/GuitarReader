using GuitarReader.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace GuitarReader.Services
{
    class RecordService
    {
        private Grid grid;
        private int count = 1;
        private List<Note> noteList = new List<Note>();
        DispatcherTimer testTimer = new DispatcherTimer();

        public RecordService()
        {
            testTimer.Interval = TimeSpan.FromSeconds(1);
            testTimer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            AddTabToSheet(count.ToString());
            noteList.Add(new Note()
            {
                fretPos = count,
                stringPos = count,
                beatLen = DateTime.Now.Second,
            });

            count++;
            if (count > 6)
            {
                count = 1;
            }
        }

        /// <summary>
        /// 녹음 시작
        /// </summary>
        /// <param name="gridSheet">view 그리드 오브젝트</param>
        public void StartRecord(object gridSheet)
        {
            grid = gridSheet as Grid;
            noteList.Clear();
            testTimer.Start();

        }

        /// <summary>
        /// 녹음 중지
        /// </summary>
        public void StopRecord()
        {
            testTimer.Stop();
            Console.WriteLine("녹음 끝");
            foreach (var i in noteList)
            {
                Console.WriteLine(string.Format("{0} {1} {2}", i.fretPos, i.stringPos, i.beatLen));
            }            
        }

        /// <summary>
        /// 녹음 정보 저장
        /// </summary>
        /// <param name="sheetName">저장될 이름</param>
        public void SaveRecord(string sheetName)
        {
            Sheet sheet = new Sheet();
            sheet.created = DateTime.Now;
            sheet.lastModified = DateTime.Now;
            sheet.name = sheetName;
            
        }


        /// <summary>
        /// view에 음정 표시
        /// </summary>
        /// <param name="tab"></param>
        private void AddTabToSheet(string tab)
        {
            TextBlock tabBlock = new TextBlock();
            tabBlock.Text = tab;
            tabBlock.FontSize = 30;
            tabBlock.Foreground = Brushes.White;
            tabBlock.VerticalAlignment = VerticalAlignment.Center;
            tabBlock.RenderTransform = new TranslateTransform
            {
                X = 650,
                Y = 0,
            };

            grid.Children.Add(tabBlock);
            Grid.SetRow(tabBlock, count);

            TranslateTransform trans = new TranslateTransform();
            tabBlock.RenderTransform = trans;
            DoubleAnimation anim = new DoubleAnimation(650, 0, TimeSpan.FromSeconds(5));
            anim.Completed += (sender, e) => { grid.Children.RemoveAt(0); };
            trans.BeginAnimation(TranslateTransform.XProperty, anim);
        }


        private void ObserveSound()
        {
            AddTabToSheet(2.ToString());
        }
    }
}
