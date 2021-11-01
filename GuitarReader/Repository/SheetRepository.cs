using GuitarReader.Models;
using System.Collections.Generic;
using System.Data.SQLite;

namespace GuitarReader.Repository
{
    class SheetRepository : BaseRepository
    {

        public void Insert(Sheet sheet)
        {
            SQLiteConnection conn = GetConnection();
            string cmd = string.Format("INSERT INTO SHEET(name, created, lastModified) VALUES('{0}', '{1}', '{2}')", sheet.name, sheet.created, sheet.lastModified);

            using (SQLiteCommand command = new SQLiteCommand(cmd, conn))
            {
                command.ExecuteNonQuery();
            }
        }


        public List<Sheet> ReadAll()
        {
            SQLiteConnection conn = GetConnection();
            string cmd = "SELECT * FROM SHEET";
            List<Sheet> lst = new List<Sheet>();

            using (SQLiteCommand command = new SQLiteCommand(cmd, conn))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Sheet temp = new Sheet();
                        temp.id = int.Parse(reader[nameof(temp.id)].ToString());
                        temp.created = reader[nameof(temp.created)].ToString();
                        temp.lastModified = reader[nameof(temp.lastModified)].ToString();
                        temp.name = reader[nameof(temp.name)].ToString();

                        lst.Add(temp);
                    }
                }

            }

            return lst;
        }

        public Sheet ReadByName(string sheetName)
        {
            SQLiteConnection conn = GetConnection();
            string cmd = string.Format("SELECT * FROM SHEET WHERE name = '{0}'", sheetName);
            Sheet temp = new Sheet();

            using (SQLiteCommand command = new SQLiteCommand(cmd, conn))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        temp.id = int.Parse(reader[nameof(temp.id)].ToString());
                        temp.created = reader[nameof(temp.created)].ToString();
                        temp.lastModified = reader[nameof(temp.lastModified)].ToString();
                        temp.name = reader[nameof(temp.name)].ToString();
                    }
                }
            }

            return temp;
        }

        public Sheet ReadMostRecent()
        {
            SQLiteConnection conn = GetConnection();
            string cmd = "SELECT * FROM SHEET ORDER BY id DESC LIMIT 1";
            Sheet temp = new Sheet();

            using (SQLiteCommand command = new SQLiteCommand(cmd, conn))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        temp.id = int.Parse(reader[nameof(temp.id)].ToString());
                        temp.created = reader[nameof(temp.created)].ToString();
                        temp.lastModified = reader[nameof(temp.lastModified)].ToString();
                        temp.name = reader[nameof(temp.name)].ToString();
                    }
                }
            }

            return temp;
        }
    }
}
