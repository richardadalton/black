using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace com.devjoy.black
{
    public class QueryTable<T>
    {
        private String _dateTimeFormat = "dd-MMM-yyyy HH:mm:ss";

        private readonly Dictionary<string, string> _renamedColumns = new Dictionary<string, string>();
        private readonly List<string> _hiddenColumns = new List<string>();

        public List<Object> For(IEnumerable<T> items)
        {
            return items.Select(CreateRow).Cast<Object>().ToList();
        }

        public void UseDateTimeFormat(string format)
        {
            _dateTimeFormat = format;
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

        public void HideColumns(string columnNames)
        {
            var columns = columnNames.Split(',');
            foreach (var column in columns)
                _hiddenColumns.Add(column);
        }

        public void OnlyShowColumns(string columnNames)
        {
            _hiddenColumns.Clear();

            var properties = typeof(T).GetProperties();

            var columns = columnNames.Split(',');
            foreach (var propertyInfo in properties)
            {
                if(!columns.Contains(GetColumnName(propertyInfo.Name)))
                _hiddenColumns.Add(GetColumnName(propertyInfo.Name));                
            }
        }

        private List<Object> CreateRow(T item)
        {
            var properties = typeof(T).GetProperties();

            return properties
                .Where(ShowColumn)
                .Select(property => CreateColumn(item, property)).ToList();
        }

        private object CreateColumn(Object item, PropertyInfo property)
        {
            var name = GetColumnName(property.Name);
            var value = GetColumnValue(item, property.Name);
            return new List<Object> { name, value };
        }

        private bool ShowColumn(PropertyInfo property)
        {
            var name = GetColumnName(property.Name);
            return !_hiddenColumns.Contains(name);
        }

        private string GetColumnName(string propertyName)
        {
            var columnName = SplitOnUpperCase(propertyName);
            if (_renamedColumns.ContainsKey(columnName))
                columnName = _renamedColumns[columnName];
            return columnName;
        }

        private string GetColumnValue(object source, string property)
        {
            var value = source.GetType()
                                 .GetProperty(property)
                                 .GetValue(source, null);
            if(value == null)
            {
                return null;
            }

            if (value.GetType().Name == "DateTime")
            {
                var valueAsDateTime = (DateTime) value;
                return FormatDateTimeColumn(valueAsDateTime);
            }
                
            return value.ToString();
        }

        private string FormatDateTimeColumn(DateTime value)
        {
            return value.ToString(_dateTimeFormat);
        }

        public static string SplitOnUpperCase(string value)
        {
            var splitText = System.Text.RegularExpressions.Regex
                .Replace(value, "([A-Z])", " $1").Trim();
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(splitText);
        }
    }
}
