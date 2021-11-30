using GuitarReader.Models;
using System.Collections.Generic;
using System.Data.SQLite;

namespace GuitarReader.Repository
{
    class NoteRepository : BaseRepository
    {

        public void Insert(Note note)
        {
            SQLiteConnection conn = GetConnection();
            string cmd = string.Format("INSERT INTO NOTE(id, stringPos, fretPos, beatLen) VALUES('{0}', '{1}', '{2}', '{3}')", note.id, note.stringPos, note.fretPos, note.beatLen);

            using (SQLiteCommand command = new SQLiteCommand(cmd, conn))
            {
                command.ExecuteNonQuery();
            }
        }

        public void UpdateBeatLength(Note newNote)
        {
            SQLiteConnection conn = GetConnection();
            string cmd = string.Format("UPDATE NOTE SET beatLen = '{0}' WHERE id = '{1}' AND stringPos = '{2}' AND fretPos = '{3}'", 
                newNote.beatLen, newNote.id, newNote.stringPos, newNote.fretPos);

            using (SQLiteCommand command = new SQLiteCommand(cmd, conn))
            {
                command.ExecuteNonQuery();
            }
        }


        public List<Note> SelectById(int id)
        {
            SQLiteConnection conn = GetConnection();
            string cmd = string.Format("SELECT * FROM NOTE WHERE id = '{0}'", id);
            List<Note> lst = new List<Note>();

            using (SQLiteCommand command = new SQLiteCommand(cmd, conn))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Note temp = new Note();
                        temp.fretPos = int.Parse(reader[nameof(temp.fretPos)].ToString());
                        temp.stringPos = int.Parse(reader[nameof(temp.stringPos)].ToString());
                        temp.beatLen = int.Parse(reader[nameof(temp.beatLen)].ToString());
                        temp.id = int.Parse(reader[nameof(temp.id)].ToString());
                        lst.Add(temp);
                    }
                }

            }

            return lst;
        }

    }
}
