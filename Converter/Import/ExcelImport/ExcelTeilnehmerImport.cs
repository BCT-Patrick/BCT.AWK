using BCT.AWK.Converter.Anwesenheitskontrollen;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace BCT.AWK.Converter.Import.ExcelImport
{
    internal class ExcelTeilnehmerImport
    {
        private readonly ImportKonfiguration _konfiguration;

        public ExcelTeilnehmerImport(ImportKonfiguration konfiguration)
        {
            _konfiguration = konfiguration;
        }

        public Dictionary<int, Teilnehmer> Laden(ExcelWorksheet worksheet)
        {
            Dictionary<int, Teilnehmer> teilnehmerDictionary = new();

            for (int teilnehmerIndex = _konfiguration.Teilnemer.ErsteTeilnehmerZeile; teilnehmerIndex <= _konfiguration.Teilnemer.LetzteTeilnehmerZeile; teilnehmerIndex++)
            {
                Teilnehmer? teilnehmer = GetTeilnehmer(worksheet, teilnehmerIndex);
                if (teilnehmer is not null)
                {
                    teilnehmerDictionary.Add(teilnehmerIndex, teilnehmer);
                }
            }

            return teilnehmerDictionary;
        }

        private Teilnehmer? GetTeilnehmer(ExcelWorksheet worksheet, int teilnehmerIndex)
        {
            string? nummer = worksheet.Cells[teilnehmerIndex, _konfiguration.Teilnemer.NummerSpalte].Value?.ToString();
            string? funktionString = worksheet.Cells[teilnehmerIndex, _konfiguration.Teilnemer.FunktionSpalte].Value?.ToString();
            if (nummer is null || funktionString is null)
            {
                return null;
            }

            Teilnehmer.FunktionsTyp funktion = GetFunktio(funktionString);
            string? vorName = worksheet.Cells[teilnehmerIndex, _konfiguration.Teilnemer.VorNameSpalte].Value?.ToString();
            string? nachName = worksheet.Cells[teilnehmerIndex, _konfiguration.Teilnemer.NachNameSpalte].Value?.ToString();
            DateTime? geburtstag = GetDateTime(worksheet.Cells[teilnehmerIndex, _konfiguration.Teilnemer.GeburtstagSpalte].Value);

            Teilnehmer teilnehmer = new(nummer, funktion, vorName, nachName, geburtstag);
            return teilnehmer;
        }

        private static Teilnehmer.FunktionsTyp GetFunktio(string funktion)
        {
            if (funktion.Contains("leiter", StringComparison.InvariantCultureIgnoreCase))
            {
                return Teilnehmer.FunktionsTyp.Leiter;
            }

            return Teilnehmer.FunktionsTyp.Teilnehmer;
        }

        private static DateTime? GetDateTime(object? value)
        {
            if (value == null)
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
                bool isDateTime = DateTime.TryParse(stringValue, CultureInfo.InvariantCulture, out DateTime dateTime);
                return isDateTime ? dateTime : null;
            }

            return null;
        }
    }
}
