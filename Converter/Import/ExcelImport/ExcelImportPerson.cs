using BCT.AWK.Converter.Anwesenheitskontrollen;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace BCT.AWK.Converter.Import.ExcelImport
{
    internal class ExcelImportPerson
    {
        private readonly ImportKonfigurationPerson _konfiguration;

        public ExcelImportPerson(ImportKonfigurationPerson konfiguration)
        {
            _konfiguration = konfiguration;
        }

        public Dictionary<int, Person> Laden(ExcelWorksheet worksheet)
        {
            Dictionary<int, Person> personDictionary = new();

            for (int personIndex = _konfiguration.ErstePersonZeile; personIndex <= _konfiguration.LetztePersonZeile; personIndex++)
            {
                Person person = GetPerson(worksheet, personIndex);
                if (person.VorName is not null || person.NachName is not null)
                {
                    personDictionary.Add(personIndex, person);
                }
            }

            return personDictionary;
        }

        private Person GetPerson(ExcelWorksheet worksheet, int teilnehmerIndex)
        {
            string? nummer = worksheet.Cells[teilnehmerIndex, _konfiguration.NummerSpalte].Value?.ToString();
            string? vorName = worksheet.Cells[teilnehmerIndex, _konfiguration.VorNameSpalte].Value?.ToString();
            string? nachName = worksheet.Cells[teilnehmerIndex, _konfiguration.NachNameSpalte].Value?.ToString();
            DateTime? geburtstag = GetDateTime(worksheet.Cells[teilnehmerIndex, _konfiguration.GeburtstagSpalte].Value);
            Person.GeschlechtTyp? geschlecht = GetGeschlecht(worksheet.Cells[teilnehmerIndex, _konfiguration.GeschlechtSpalte].Value?.ToString());
            string? ahvNr = worksheet.Cells[teilnehmerIndex, _konfiguration.AhvNrSpalte].Value?.ToString();
            string? peId = worksheet.Cells[teilnehmerIndex, _konfiguration.PeIdSpalte].Value?.ToString();
            Person.NationalitaetTyp nationalitaet = GetNationalitaet(worksheet.Cells[teilnehmerIndex, _konfiguration.NationalitaetSpalte].Value?.ToString());
            Person.SpracheTyp muttersprache = GetSprache(worksheet.Cells[teilnehmerIndex, _konfiguration.MutterspracheSpalte].Value?.ToString());
            string? strasse = worksheet.Cells[teilnehmerIndex, _konfiguration.StrasseSpalte].Value?.ToString();
            string? hausnummer = worksheet.Cells[teilnehmerIndex, _konfiguration.HausnummerSpalte].Value?.ToString();
            string? plz = worksheet.Cells[teilnehmerIndex, _konfiguration.PlzSpalte].Value?.ToString();
            string? ort = worksheet.Cells[teilnehmerIndex, _konfiguration.OrtSpalte].Value?.ToString();
            string? land = worksheet.Cells[teilnehmerIndex, _konfiguration.LandSpalte].Value?.ToString();

            Person person = new(
                nummer,
                vorName,
                nachName,
                geburtstag,
                geschlecht,
                ahvNr,
                peId,
                nationalitaet,
                muttersprache,
                strasse,
                hausnummer,
                plz,
                ort,
                land);

            return person;
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

        private static Person.GeschlechtTyp? GetGeschlecht(string? geschlechtString)
        {
            if (Enum.TryParse(geschlechtString?.Trim(), true, out Person.GeschlechtTyp geschlecht))
            {
                return geschlecht;
            }

            return null;
        }

        private static Person.NationalitaetTyp GetNationalitaet(string? nationalitaetString)
        {
            if (Enum.TryParse(nationalitaetString?.Trim(), true, out Person.NationalitaetTyp nationalitaet))
            {
                return nationalitaet;
            }

            return Person.NationalitaetTyp.Andere;
        }

        private static Person.SpracheTyp GetSprache(string? spracheString)
        {
            if (Enum.TryParse(spracheString?.Trim(), true, out Person.SpracheTyp sprache))
            {
                return sprache;
            }

            return Person.SpracheTyp.Andere;
        }
    }
}
