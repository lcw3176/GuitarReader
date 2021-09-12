using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Reflection;
using System.Text;

namespace GuitarReader.Services
{
    class DBService<T>
    {
        private string location = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sheet.db");
        private string connStr;

        public DBService()
        {
            connStr = "Data Source=" + location; 
        }

        public bool IsExist()
        {
            return new FileInfo(location).Exists;
        }

        public void InitTables()
        {
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                string sheetDDL = "CREATE TABLE SHEET(" +
                    "id INTEGER PRIMARY KEY AUTOINCREMENT," +
                    "name CHAR(100) not null," +
                    "created DATE not null," +
                    "lastModified DATE not null);";
                var cmd = new SQLiteCommand(sheetDDL, conn);
                cmd.ExecuteNonQuery();
            }
        }

        public bool Write<T>(T value)
        {
            return false;
        }

        public List<T> Read(string tableName, params string[] readElements)
        {
            List<T> lst = new List<T>();
            StringBuilder query = new StringBuilder();
            query.Append("SELECT ");

            foreach (string i in readElements)
            {
                query.Append(i);
                query.Append(",");
            }

            query.Remove(query.Length - 1, 1);

            query.Append("FROM ");
            query.Append(tableName);

            Type type = typeof(T);
            PropertyInfo[] propertyInfos = type.GetProperties();

            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                
                SQLiteCommand cmd = new SQLiteCommand(query.ToString(), conn);
                SQLiteDataReader reader = cmd.ExecuteReader();
                

                while (reader.Read())
                {
                    object obj = Activator.CreateInstance(type);

                    foreach (PropertyInfo i in propertyInfos)
                    {
                        PropertyInfo propInfo = type.GetProperty(i.Name);
                        propInfo.SetValue(obj, reader[i.Name]);
                    }

                    lst.Add((T)obj);
                }
    
            }

            return lst;
        }

    }
}
