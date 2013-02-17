using System;
using System.Collections.Generic;
using System.Linq;

namespace com.devjoy.black
{
    public class TableRow
    {
        private readonly List<TableCell> cells = new List<TableCell>();

        public TableRow(IEnumerable<string> createFrom)
        {
            foreach(var cell in createFrom)
                cells.Add(new TableCell(cell));
        }

        public List<TableCell> Cells { get { return cells; } }

        public List<String> AsSlimRow()
        {
            return cells.Select(cell => cell.ToString()).ToList();
        }

        public void Ignore()
        {
            foreach(var cell in cells)
                cell.Ignore();        
        }

        public void NoChange()
        {
            foreach (var cell in cells)
                cell.NoChange();
        }
    }
}
