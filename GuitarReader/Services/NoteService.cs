using GuitarReader.Models;
using System.Data.SQLite;

namespace GuitarReader.Services
{
    class NoteService
    {
        private DBService dBService = DBService.GetInstace();

        public void Insert(Note note)
        {
            SQLiteConnection conn = dBService.GetConnection();
            string cmd = string.Format("INSERT INTO NOTE(id, stringPos, fretPos, beatLen) VALUES('{0}', '{1}', '{2}', '{3}')", note.id, note.stringPos, note.fretPos, note.beatLen);

            using (SQLiteCommand command = new SQLiteCommand(cmd, conn))
            {
                command.ExecuteNonQuery();
            }
        }
    

        public Note ReadByName(string name)
        {
            return null;
        }
            
    }
}
