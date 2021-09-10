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
        private Grid sheet;
        private int count = 0;
        DispatcherTimer testTimer = new DispatcherTimer();

        public RecordService()
        {
            testTimer.Interval = TimeSpan.FromSeconds(1);
            testTimer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            AddTabToSheet(count++.ToString());
            
            if(count >= 6)
            {
                count = 0;
            }
        }

        public void StartRecord(object gridSheet)
        {
            sheet = gridSheet as Grid;
            testTimer.Start();

        }

        public void StopRecord()
        {
            testTimer.Stop();
        }


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

            sheet.Children.Add(tabBlock);
            Grid.SetRow(tabBlock, count);

            TranslateTransform trans = new TranslateTransform();
            tabBlock.RenderTransform = trans;
            DoubleAnimation anim = new DoubleAnimation(650, 0, TimeSpan.FromSeconds(5));
            anim.Completed += (sender, e) => { sheet.Children.RemoveAt(0); };
            trans.BeginAnimation(TranslateTransform.XProperty, anim);
 
        }


        private void ObserveSound()
        {
            AddTabToSheet(2.ToString());
        }
    }
}
