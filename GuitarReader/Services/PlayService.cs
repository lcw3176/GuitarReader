using GuitarReader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using Toub.Sound.Midi;

namespace GuitarReader.Services
{
    class PlayService
    {
        private Grid grid;
        private NoteService noteService = new NoteService();
        private DispatcherTimer timer = new DispatcherTimer();
        private List<Note> lst;
        private int count = 0;

        public PlayService()
        {
            timer.Interval = TimeSpan.FromSeconds(0.5);
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            AddTabToSheet(lst[count].stringPos, lst[count].fretPos);
            MidiPlayer.Play(new NoteOn(0, 0, lst[count].codeStr, 127));
            count++;

            if(count >= lst.Count)
            {
                timer.Stop();
            }
        }

        public void Play(object _grid, int songId)
        {
            grid = _grid as Grid;
            lst = noteService.ReadById(songId);
            timer.Start();
        }

        private void AddTabToSheet(int stringPos, int fretPos)
        {
            TextBlock tabBlock = new TextBlock();
            tabBlock.Text = fretPos.ToString();
            tabBlock.FontSize = 30;
            tabBlock.Foreground = Brushes.White;
            tabBlock.VerticalAlignment = VerticalAlignment.Center;
            tabBlock.RenderTransform = new TranslateTransform
            {
                X = 650,
                Y = 0,
            };

            grid.Children.Add(tabBlock);
            Grid.SetRow(tabBlock, stringPos);

            TranslateTransform trans = new TranslateTransform();
            tabBlock.RenderTransform = trans;
            DoubleAnimation anim = new DoubleAnimation(650, 0, TimeSpan.FromSeconds(5));
            anim.Completed += (sender, e) => { grid.Children.RemoveAt(0); };
            trans.BeginAnimation(TranslateTransform.XProperty, anim);
        }
    }
}
