﻿using System;
using System.Data.SQLite;
using System.IO;

namespace GuitarReader.Repository
{
    class BaseRepository
    {
        private string location = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "guitarReader.db");
        private static SQLiteConnection conn;

        protected BaseRepository()
        {
            if (conn == null)
            {
                string connStr = "Data Source=" + location;
                conn = new SQLiteConnection(connStr);
                conn.Open();
            }

            if (!IsExist())
            {
                InitTables();
            }
        }

        protected SQLiteConnection GetConnection()
        {
            return conn;
        }

        public static void CloseConnection()
        {
            if(conn != null)
            {
                conn.Close();
                conn.Dispose();
            }
        }

        protected bool IsExist()
        {
            string query = "SELECT COUNT(*) FROM sqlite_master WHERE NAME = 'SHEET'";
            int result = 0;

            using (var cmd = new SQLiteCommand(query, conn))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result = int.Parse(reader[0].ToString());
                    }

                }
            }

            return result == 1 ? true : false;

        }


        protected void InitTables()
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
