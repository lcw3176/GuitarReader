using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GuitarReader.Services
{
    class DBService<T>
    {
        private static DBService<T> instance;
        private SqliteConnection connection;
        public bool isOpen = false;

        public static DBService<T> GetInstace()
        {
            if(instance == null)
            {
                instance = new DBService<T>();
            }

            return instance;
        }


        public bool Open()
        {
            string connStr = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sheet.db");
            try
            {
                if (connection == null)
                {
                    connection = new SqliteConnection(connStr);
                    connection.Open();
                    isOpen = true;
                }

                return true;
            }

            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            
        }

        public void Close()
        {
            connection.Close();
            connection.Dispose();
            isOpen = false;
        }

        public bool Write(object obj)
        {
            return false;
        }

        public List<T> Read(string table, params string[] elements)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT ");

            foreach(string i in elements)
            {
                query.Append(i);
                query.Append(",");
            }

            query.Remove(query.Length - 1, 1);

            query.Append("FROM ");
            query.Append(table);

            SqliteCommand cmd = new SqliteCommand(query.ToString(), connection);
            SqliteDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {

            }

            return null;
        }
    }
}
