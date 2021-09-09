using GuitarReader.Views;
using System;
using System.Linq;
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
        DispatcherTimer testTimer = new DispatcherTimer();

        public RecordService()
        {

            testTimer.Interval = TimeSpan.FromSeconds(1);
            testTimer.Tick += Timer_Tick;
            
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            AddTabToSheet(2.ToString());
        }

        public void StartRecord(object gridSheet)
        {
            sheet = gridSheet as Grid;
            testTimer.Start();

        }

        public void StopRecord()
        {

            
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
            Grid.SetRow(tabBlock, 1);
            remainingTabs += 1;

            TranslateTransform trans = new TranslateTransform();
            tabBlock.RenderTransform = trans;
            DoubleAnimation anim = new DoubleAnimation(650, 0, TimeSpan.FromSeconds(10));
            anim.Completed += (sender, e) => { sheet.Children.RemoveAt(0); };
            trans.BeginAnimation(TranslateTransform.XProperty, anim, HandoffBehavior.SnapshotAndReplace);
            
        }


        private void ObserveSound()
        {
            AddTabToSheet(2.ToString());
        }
    }
}
