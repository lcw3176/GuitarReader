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
    class DisplayUtil
    {
        private Grid grid;

        public DisplayUtil(Grid targetGrid)
        {
            grid = targetGrid;
        }


        public void AddTabWithOffset(List<Note> notes)
        {
            const int offset = 281;
            int beforeBeatLen = notes[0].beatLen - 1;
            int index = 0;

            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
            {
                foreach (Note i in notes)
                {
                    if (beforeBeatLen > i.beatLen)
                    {
                        i.beatLen += 60;
                    }

                    int xPos = i.beatLen - beforeBeatLen;
                    index += xPos * offset;
                    TextBlock tabBlock = new TextBlock();
                    tabBlock.Text = i.fretPos.ToString();
                    tabBlock.FontSize = 30;
                    tabBlock.Foreground = Brushes.White;
                    tabBlock.VerticalAlignment = VerticalAlignment.Center;
                    tabBlock.RenderTransform = new TranslateTransform
                    {
                        X = index,
                        Y = 0,
                    };

                    grid.Children.Add(tabBlock);
                    Grid.SetRow(tabBlock, i.stringPos);
                    beforeBeatLen = i.beatLen;
                }

            }));

        }

        public void MoveBack()
        {
            foreach (TextBlock i in grid.Children)
            {
                TranslateTransform trans = new TranslateTransform
                {
                    X = i.RenderTransform.Value.OffsetX + 20,
                    Y = 0
                };

                i.RenderTransform = trans;
            }
        }

        public void MoveForward()
        {
            foreach (TextBlock i in grid.Children)
            {
                TranslateTransform trans = new TranslateTransform
                {
                    X = i.RenderTransform.Value.OffsetX - 20,
                    Y = 0
                };

                i.RenderTransform = trans;
            }
        }
    }
}
