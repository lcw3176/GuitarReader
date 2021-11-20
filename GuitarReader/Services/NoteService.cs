using GuitarReader.Models;
using GuitarReader.Repository;
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
    

        public List<Note> ReadById(int id)
        {
            return noteRepository.ReadById(id);
        }

        public void UpdateBeatLength(Note beforeNote, Note updateNote)
        {
            noteRepository.UpdateBeatLength(beforeNote, updateNote);
        }


    }
}
