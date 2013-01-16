using System;
using System.Collections.Generic;
using System.Linq;

namespace com.devjoy.black
{
    public class TableRow
    {
        private readonly List<TableCell> _cells = new List<TableCell>();

        public TableRow(IEnumerable<string> createFrom)
        {
            foreach(var cell in createFrom)
                _cells.Add(new TableCell(cell));
        }

        public List<TableCell> Cells { get { return _cells; } }

        public List<String> AsSlimRow()
        {
            return _cells.Select(cell => cell.AsSlimCell()).ToList();
        }
    }
}
