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
        private string location = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "guitarReader.db");
        private string connStr;

        public DBService()
        {
            connStr = "Data Source=" + location;

            if (!IsExist())
            {
                InitTables();
            }
        }

        public bool IsExist()
        {
            return new FileInfo(location).Exists;
        }

        public void InitTables()
        {
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string sheetDDL = "CREATE TABLE SHEET(" +
                    "id INTEGER PRIMARY KEY AUTOINCREMENT," +
                    "name CHAR(100) not null," +
                    "created CHAR(100) not null," +
                    "lastModified CHAR(100) not null);";
                var cmd = new SQLiteCommand(sheetDDL, conn);
                cmd.ExecuteNonQuery();

                string noteDDL = "CREATE TABLE NOTE(" +
                    "id INTEGER," +
                    "stringPos INTEGER not null," +
                    "fretPos INTEGER not null," +
                    "beatLen INTEGER not null," +
                    "FOREIGN KEY(id)" +
                    "REFERENCES SHEET(id)" +
                    "ON DELETE CASCADE " +
                    "ON UPDATE CASCADE)";

                cmd = new SQLiteCommand(noteDDL, conn);
                cmd.ExecuteNonQuery();
            }
        }

        public object Write(string tableName, T value)
        {
            Type type = typeof(T);
            PropertyInfo[] propertyInfos = type.GetProperties();


            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO ");
            sb.Append(tableName);
            sb.Append("(");

            foreach (PropertyInfo i in propertyInfos)
            {
                if (i.Name.ToLower().Contains("key"))
                {
                    continue;
                }
                sb.Append(i.Name);
                sb.Append(",");
            }

            sb.Remove(sb.Length - 1, 1);
            sb.Append(")");

            sb.Append(" VALUES");
            sb.Append("(");

           
            foreach (PropertyInfo i in propertyInfos)
            {
                if (i.Name.ToLower().Contains("key"))
                {
                    continue;
                }
                sb.Append("'");
                sb.Append(i.GetValue(value));
                sb.Append("'");
                sb.Append(",");
            }

            sb.Remove(sb.Length - 1, 1);
            sb.Append(")");

            Console.WriteLine(sb.ToString());
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connStr))
                {
                    conn.Open();
                    SQLiteCommand cmd = new SQLiteCommand(sb.ToString(), conn);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();

                }

                return true;
            }

            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
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

            query.Append(" FROM ");
            query.Append(tableName);

            Type type = typeof(T);
            PropertyInfo[] propertyInfos = type.GetProperties();

            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand(query.ToString(), conn);
                SQLiteDataReader reader = cmd.ExecuteReader();
                

                while (reader.Read())
                {
                    object obj = Activator.CreateInstance(type);

                    foreach (PropertyInfo i in propertyInfos)
                    {
                        if (i.Name.ToLower().Contains("key"))
                        {
                            continue;
                        }

                        PropertyInfo propInfo = type.GetProperty(i.Name);
                        propInfo.SetValue(obj, reader[i.Name]);
                    }

                    lst.Add((T)obj);
                }

                cmd.Dispose();
                reader.Close();
            }

            return lst;
        }

    }
}
