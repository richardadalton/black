using System;
using System.Collections.Generic;
using System.Linq;

namespace com.devjoy.black
{
    public class TableTable
    {
        private readonly List<TableRow> _rows = new List<TableRow>();

        public TableTable(IEnumerable<List<string>> createFrom)
        {
            foreach(var row in createFrom)
                _rows.Add(new TableRow(row));
        }

        public List<TableRow> Rows { get { return _rows; } }

        public List<List<String>> AsSlimTable()
        {
            return _rows.Select(row => row.AsSlimRow()).ToList();
        }
    }
}
