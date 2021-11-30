using GuitarReader.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace GuitarReader.Util
{
    class AnimService
    {
        private Grid grid;
        private List<AnimationClock> controllers = new List<AnimationClock>();

        public AnimService(Grid targetGrid)
        {
            grid = targetGrid;
        }

        public void Pause()
        {
            foreach (var i in controllers)
            {
                i.Controller.Pause();
            }
        }

        public void Stop()
        {
            foreach (var i in controllers)
            {
                i.Controller.Remove();
            }
        }

        public void Resume()
        {
            foreach (var i in controllers)
            {
                i.Controller.Resume();
            }
        }

        public void AddTab(int stringPos, int fretPos)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
            {
                TextBlock tabBlock = new TextBlock();
                tabBlock.Text = fretPos.ToString();
                tabBlock.FontSize = 30;
                tabBlock.Foreground = Brushes.White;
                tabBlock.VerticalAlignment = VerticalAlignment.Center;
                tabBlock.RenderTransform = new TranslateTransform
                {
                    X = grid.ActualWidth,
                    Y = 0,
                };

                grid.Children.Add(tabBlock);
                Grid.SetRow(tabBlock, stringPos);

                TranslateTransform trans = new TranslateTransform();
                tabBlock.RenderTransform = trans;
                DoubleAnimation anim = new DoubleAnimation(grid.ActualWidth, 0, TimeSpan.FromSeconds(5));
                trans.BeginAnimation(TranslateTransform.XProperty, anim);

                var controller = anim.CreateClock();
                controller.Completed += (sender, e) =>
                {
                    controllers.RemoveAt(0);
                    grid.Children.RemoveAt(0);
                };
                controllers.Add(controller);
                trans.ApplyAnimationClock(TranslateTransform.XProperty, controller);
            }));

        }

    }
}
