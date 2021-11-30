using GuitarReader.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace GuitarReader.Util
{
    class DisplayService
    {
        private Grid grid;
        private const int offset = 281;
        private List<Note> noteList;

        public DisplayService(Grid targetGrid)
        {
            grid = targetGrid;
        }

        private void ClearGrid()
        {
            grid.Children.Clear();
        }


        public void AddTabWithOffset(List<Note> notes)
        {
            ClearGrid();
            noteList = notes.ConvertAll(i => new Note()
            {
                stringPos = i.stringPos,
                fretPos = i.fretPos,
                beatLen = i.beatLen,
                id = i.id
            });

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

                    tabBlock.MouseDown += TabBlock_MouseDown;
                    grid.Children.Add(tabBlock);
                    Grid.SetRow(tabBlock, i.stringPos);
                    beforeBeatLen = i.beatLen;
                }

            }));

        }


        private void TabBlock_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TextBlock tabBlock = (sender as TextBlock);

            if (e.ButtonState == e.LeftButton && ValidationCheck(tabBlock.RenderTransform.Value.OffsetX - offset))
            {
                tabBlock.RenderTransform = new TranslateTransform
                {
                    X = tabBlock.RenderTransform.Value.OffsetX - offset,
                    Y = 0
                };


                foreach (var i in noteList)
                {
                    if (i.fretPos.ToString() == tabBlock.Text && i.stringPos == Grid.GetRow(tabBlock))
                    {
                        i.beatLen--;
                        return; ;
                    }
                }

            }

            else if(e.ButtonState == e.RightButton && ValidationCheck(tabBlock.RenderTransform.Value.OffsetX + offset))
            {
                tabBlock.RenderTransform = new TranslateTransform
                {
                    X = tabBlock.RenderTransform.Value.OffsetX + offset,
                    Y = 0
                };


                foreach (var i in noteList)
                {
                    if (i.fretPos.ToString() == tabBlock.Text && i.stringPos == Grid.GetRow(tabBlock))
                    {
                        i.beatLen++;
                        return; ;
                    }
                }
            }

        }

        private bool ValidationCheck(double movePosition)
        {
            foreach(TextBlock i in grid.Children)
            {

                if (i.RenderTransform.Value.OffsetX == movePosition || movePosition <= offset)
                {
                    return false;
                }
            }

            return true;
        }

        public void MoveBack()
        {
            if(grid.Children[0].RenderTransform.Value.OffsetX >= offset)
            {
                return;
            }

            foreach (TextBlock i in grid.Children)
            {
                TranslateTransform trans = new TranslateTransform
                {
                    X = i.RenderTransform.Value.OffsetX + offset / 2,
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
                    X = i.RenderTransform.Value.OffsetX - offset / 2,
                    Y = 0
                };

                i.RenderTransform = trans;
            }
        }

        public List<Note> GetCurrentNotes()
        {
            return noteList;
        }

    }
}
