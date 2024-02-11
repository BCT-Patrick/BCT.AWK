using System;
using System.Globalization;

namespace BCT.AWK.Converter.Import.ExcelImport
{
    internal static class ExcelImportDateTime
    {

        public static DateOnly? GetDate(object? value)
        {
            DateTime? dateTime = GetDateTime(value);
            DateOnly? date = dateTime.HasValue ? DateOnly.FromDateTime(dateTime.Value) : null;
            return date;
        }

        public static TimeOnly? GetTime(object? value)
        {
            DateTime? dateTime = GetDateTime(value);
            TimeOnly? time = dateTime.HasValue ? TimeOnly.FromDateTime(dateTime.Value) : null;
            return time;
        }

        private static DateTime? GetDateTime(object? value)
        {
            if (value is null)
            {
                return null;
            }

            if (value is DateTime dateTimeValue)
            {
                return dateTimeValue;
            }

            if (value is double doubleValue)
            {
                DateTime date = DateTime.FromOADate(doubleValue);
                return date;
            }

            if (value is string stringValue)
            {
                bool isDateTime = DateTime.TryParse(stringValue, CultureInfo.CurrentCulture, out DateTime dateTime);
                return isDateTime ? dateTime : null;
            }

            return null;
        }
    }
}
