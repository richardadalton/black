using System;
using System.Collections.Generic;
using System.Linq;

namespace com.devjoy.black
{
    public class TableTable
    {
        private readonly List<TableRow> rows = new List<TableRow>();

        public TableTable(IEnumerable<IEnumerable<string>> source)
        {
            foreach(var sourceRow in source)
                rows.Add(new TableRow(sourceRow));
        }

        public List<TableRow> Rows { get { return rows; } }

        public List<List<String>> AsSlimTable()
        {
            return rows.Select(row => row.AsSlimRow()).ToList();
        }

        public TableCell Cells(int row, int cell)
        {
            return rows[row].Cells[cell];
        }

        public string CellValues(int row, int cell)
        {
            return rows[row].Cells[cell].ToString();
        }
    }
}
