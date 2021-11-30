using GuitarReader.Models;
using GuitarReader.Repository;
using System;
using System.Collections.Generic;

namespace GuitarReader.Services
{
    class NoteService
    {
        private NoteRepository noteRepository = new NoteRepository();

        public void Insert(Note note)
        {
            noteRepository.Insert(note);
        }
    

        public List<Note> SelectById(int id)
        {
            return noteRepository.SelectById(id);
        }

        public void UpdateRecentData(List<Note> recentData)
        {
            List<Note> oldData = SelectById(recentData[0].id);


            for (int i = 0; i < recentData.Count; i++)
            {
                if (recentData[i].beatLen != oldData[i].beatLen)
                {
                    noteRepository.UpdateBeatLength(recentData[i]);
                }
            }

        }


    }
}
