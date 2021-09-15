using System;
using System.Data.SQLite;
using System.IO;

namespace GuitarReader.Services
{
    class DBService
    {
        private string location = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "guitarReader.db");
        private static DBService instance;
        private static SQLiteConnection conn;

        public static DBService GetInstace()
        {
            if(instance == null)
            {
                instance = new DBService();
            }

            return instance;
        }

        private DBService()
        {
            string connStr = "Data Source=" + location;
            conn = new SQLiteConnection(connStr);
            conn.Open();
            
            if (!IsExist())
            {
                InitTables();
            }
        }

        public SQLiteConnection GetConnection()
        {
            return conn;
        }

        public void CloseConnection()
        {
            conn.Close();
            conn.Dispose();
        }

        private bool IsExist()
        {
            return new FileInfo(location).Exists;
        }


        private void InitTables()
        {
            string sheetDDL = "CREATE TABLE SHEET(" +
                "id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "name CHAR(100) UNIQUE NOT NULL," +
                "created CHAR(100) NOT NULL," +
                "lastModified CHAR(100) NOT NULL);";
            var cmd = new SQLiteCommand(sheetDDL, conn);
            cmd.ExecuteNonQuery();
            
            string noteDDL = "CREATE TABLE NOTE(" +
                "id INTEGER," +
                "stringPos INTEGER NOT NULL," +
                "fretPos INTEGER NOT NULL," +
                "beatLen INTEGER NOT NULL," +
                "FOREIGN KEY(id)" +
                "REFERENCES SHEET(id)" +
                "ON DELETE CASCADE " +
                "ON UPDATE CASCADE)";
            
            cmd = new SQLiteCommand(noteDDL, conn);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }

    }
}
