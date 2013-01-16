using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace com.devjoy.black
{
    public class TableAdaptor<T>
    {
        private readonly Dictionary<string, string> _renamedColumns = new Dictionary<string, string>();
        private readonly List<string> _hiddenColumns = new List<string>();

        public List<Object> FromObjects(IEnumerable<T> items)
        {
            return CreateTable(items);
        }

        public void OverrideColumnNames(string nameChanges)
        {
            var changes = nameChanges.Split(',');
            foreach (var change in changes)
                OverrideColumnName(change);
        }

        public void OverrideColumnName(string nameChange)
        {
            var change = nameChange.Split('|');
            OverrideColumnName(change[0], change[1]);
        }

        public void OverrideColumnName(string from, string to)
        {
            _renamedColumns.Add(from, to);
        }

        public void HideColumn(string columnName)
        {
            _hiddenColumns.Add(columnName);
        }

        public void HideColumns(string columnNames)
        {
            var columns = columnNames.Split(',');
            foreach (var column in columns)
                _hiddenColumns.Add(column);
        }

        private List<Object> CreateTable(IEnumerable<T> items)
        {
            return items.Select(CreateRow).Cast<Object>().ToList();
        }

        private List<Object> CreateRow(T item)
        {
            IEnumerable<PropertyInfo> properties = GetProperties();

            return properties
                .Where(ShowColumn)
                .Select(property => CreateColumn(item, property)).ToList();
        }

        private object CreateColumn(Object item, PropertyInfo property)
        {
            string name = GetColumnName(property.Name);
            string value = GetColumnValue(item, property.Name);
            return new List<Object> { name, value };
        }

        private bool ShowColumn(PropertyInfo property)
        {
            string name = GetColumnName(property.Name);
            return !_hiddenColumns.Contains(name);
        }


        private string GetColumnName(string propertyName)
        {
            string columnName = SplitCamelCase(propertyName);
            if (_renamedColumns.ContainsKey(columnName))
                columnName = _renamedColumns[columnName];
            return columnName;
        }

        private IEnumerable<PropertyInfo> GetProperties()
        {
            return typeof(T).GetProperties();
        }

        private string GetColumnValue(object source, string property)
        {
            object value = source.GetType()
                                 .GetProperty(property)
                                 .GetValue(source, null);

            string valueAsString = value.ToString();

            if (value.GetType().Name == "DateTime")
                valueAsString = FixDate(value);

            return valueAsString;
        }

        private string SplitCamelCase(string value)
        {
            return System.Text.RegularExpressions.Regex
                .Replace(value, "([A-Z])", " $1").Trim();
        }

        private string FixDate(Object value)
        {
            var date = (DateTime)value;

            if (date.TimeOfDay.Ticks == 0)
                return date.ToString("dd-MMM-yyyy");

            return value.ToString();
        }
    }
}
