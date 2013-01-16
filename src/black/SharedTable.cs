using System.Collections.Generic;

namespace com.devjoy.black
{
    public class SharedTable
    {
        private static SharedTable _factory;
        private Dictionary<string, object> _tables = new Dictionary<string, object>();
        private Dictionary<string, object> _keyedTables = new Dictionary<string, object>();

        public static SharedTable Factory
        {
            get
            {
                return _factory ?? 
                    (_factory = new SharedTable());
            }
        }

        public List<T> GetTable<T>(string tableName)
        {
            if (!_tables.ContainsKey(tableName))
            {
                var table = new List<T>();
                _tables.Add(tableName, table);                
            }

            return _tables[tableName] as List<T>;
        }

        public void DisposeOfTable(string tableName)
        {
            if (_tables.ContainsKey(tableName))
                _tables.Remove(tableName);
        }

        public void DisposeOfKeyedTable(string tableName)
        {
            if (_keyedTables.ContainsKey(tableName))
                _keyedTables.Remove(tableName);
        }

        public void DisposeOfAllTables()
        {
            _tables = new Dictionary<string, object>();
            _keyedTables = new Dictionary<string, object>();
        }
    }
}
