using System.Collections.Generic;

namespace com.devjoy.black
{
    public class SharedTables
    {
        private static SharedTables instance;
        private Dictionary<string, object> tables = new Dictionary<string, object>();

        private static SharedTables Instance
        {
            get
            {
                return instance ?? 
                    (instance = new SharedTables());
            }
        }

        public static List<T> Table<T>()
        {
            return Table<T>(typeof (T).FullName);
        }

        public static List<T> Table<T>(string tableName)
        {
            if (!Instance.tables.ContainsKey(tableName))
            {
                var table = new List<T>();
                Instance.tables.Add(tableName, table);                
            }

            return Instance.tables[tableName] as List<T>;
        }

        public static void DisposeOf<T>()
        {
            DisposeOf(typeof(T).FullName);
        }

        public static void DisposeOf(string tableName)
        {
            if (Instance.tables.ContainsKey(tableName))
                Instance.tables.Remove(tableName);
        }

        public static void DisposeOfAll()
        {
            Instance.tables = new Dictionary<string, object>();
        }
    }
}
