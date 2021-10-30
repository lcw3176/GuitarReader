using GuitarReader.Models;
using System;
using System.Collections.Generic;
using System.Windows.Threading;
using Toub.Sound.Midi;

namespace GuitarReader.Services
{
    /// <summary>
    /// 악보 재생 클래스
    /// </summary>
    class PlayService
    {
        public delegate void BeatPlayEvent(int stringPos, int fretPos);
        public event BeatPlayEvent beatPlayEvent;

        public delegate void BeatEndEvent();
        public event BeatEndEvent beatEndEvent;

        private NoteService noteService = new NoteService();
        private DispatcherTimer timer = new DispatcherTimer();
        private List<Note> lst;
        private int count = 0;
        private static PlayService instacne;

        public static PlayService GetInstacne()
        {
            if(instacne == null)
            {
                instacne = new PlayService();
            }

            return instacne;
        }
        

        private PlayService()
        {
            timer.Interval = TimeSpan.FromSeconds(0.5);
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            MidiPlayer.Play(new NoteOn(0, 0, lst[count].CodeStr, 127));
            beatPlayEvent(lst[count].stringPos, lst[count].fretPos);
            count++;

            if (count >= lst.Count)
            {
                count = 0;
                timer.Stop();
                beatEndEvent();
            }
        }

        public void Play(int songId)
        {
            lst = noteService.ReadById(songId);

            timer.Start();
        }

        public void Pause()
        {
            timer.Stop();
        }

        public void Stop()
        {
            count = 0;
            timer.Stop();
        }

        public void Open()
        {
            MidiPlayer.OpenMidi();
            MidiPlayer.Play(new ProgramChange(0, 0, GeneralMidiInstruments.SteelAcousticGuitar));
        }

        public void Close()
        {
            count = 0;
            timer.Stop();
            MidiPlayer.CloseMidi();
        }
    }
}
