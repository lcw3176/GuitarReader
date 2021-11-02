using GuitarReader.Models;
using GuitarReader.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Toub.Sound.Midi;

namespace GuitarReader.Util
{
    class PlayUtil
    {
        public delegate void BeatPlayEvent(int stringPos, int fretPos);
        public event BeatPlayEvent beatPlayEvent;

        public delegate void BeatEndEvent();
        public event BeatEndEvent beatEndEvent;

        private NoteService noteService = new NoteService();
        private bool isRun = false;
        private List<Note> lst;
        private int index = 0;

        private void PlayBeat()
        {
            
            MidiPlayer.Play(new NoteOn(0, 0, lst[index].CodeStr, 127));
            beatPlayEvent(lst[index].stringPos, lst[index].fretPos);
            index++;

            if (index >= lst.Count)
            {
                index = 0;
                isRun = false;
                beatEndEvent();
                
            }
        }

        public void Play(int songId)
        {
            lst = noteService.ReadById(songId);
            isRun = true;

            Task.Run(() =>
            {
                DateTime startTime = DateTime.Now;

                while (isRun)
                {
                    if(DateTime.Now - startTime >= TimeSpan.FromSeconds(0.5))
                    {
                        PlayBeat();
                        startTime = DateTime.Now;
                    }
                    
                }
            });
        }

        public void Pause()
        {
            isRun = true;
        }

        public void Stop()
        {
            isRun = false;
            index = 0;
        }
    }
}
