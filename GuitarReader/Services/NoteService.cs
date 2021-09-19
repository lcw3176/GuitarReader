using GuitarReader.Models;
using System.Collections.Generic;
using System.Data.SQLite;

namespace GuitarReader.Services
{
    class NoteService : DBService
    {
        //private DBService dBService = DBService.GetInstace();

        public void Insert(Note note)
        {
            SQLiteConnection conn = GetConnection();
            string cmd = string.Format("INSERT INTO NOTE(id, stringPos, fretPos, beatLen) VALUES('{0}', '{1}', '{2}', '{3}')", note.id, note.stringPos, note.fretPos, note.beatLen);

            using (SQLiteCommand command = new SQLiteCommand(cmd, conn))
            {
                command.ExecuteNonQuery();
            }
        }
    

        public List<Note> ReadById(int id)
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
                        lst.Add(temp);
                    }
                }

            }

            return lst;
        }
            
    }
}
