using BCT.AWK.Converter.Anwesenheitskontrollen;
using OfficeOpenXml;
using System;
using System.Collections.Generic;

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

        private Person GetPerson(ExcelWorksheet worksheet, int personIndex)
        {
            string? nummer = worksheet.Cells[personIndex, _konfiguration.NummerSpalte].Value?.ToString();
            string? vorName = worksheet.Cells[personIndex, _konfiguration.VorNameSpalte].Value?.ToString();
            string? nachName = worksheet.Cells[personIndex, _konfiguration.NachNameSpalte].Value?.ToString();
            DateOnly? geburtstag = ExcelImportDateTime.GetDate(worksheet.Cells[personIndex, _konfiguration.GeburtstagSpalte].Value);
            Person.GeschlechtTyp? geschlecht = GetGeschlecht(worksheet.Cells[personIndex, _konfiguration.GeschlechtSpalte].Value?.ToString());
            string? ahvNr = worksheet.Cells[personIndex, _konfiguration.AhvNrSpalte].Value?.ToString();
            string? peId = worksheet.Cells[personIndex, _konfiguration.PeIdSpalte].Value?.ToString();
            Person.NationalitaetTyp? nationalitaet = GetNationalitaet(worksheet.Cells[personIndex, _konfiguration.NationalitaetSpalte].Value?.ToString());
            Person.SpracheTyp? muttersprache = GetSprache(worksheet.Cells[personIndex, _konfiguration.MutterspracheSpalte].Value?.ToString());
            string? strasse = worksheet.Cells[personIndex, _konfiguration.StrasseSpalte].Value?.ToString();
            string? hausnummer = worksheet.Cells[personIndex, _konfiguration.HausnummerSpalte].Value?.ToString();
            string? plz = worksheet.Cells[personIndex, _konfiguration.PlzSpalte].Value?.ToString();
            string? ort = worksheet.Cells[personIndex, _konfiguration.OrtSpalte].Value?.ToString();
            string? land = worksheet.Cells[personIndex, _konfiguration.LandSpalte].Value?.ToString();

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

        private static Person.GeschlechtTyp? GetGeschlecht(string? geschlechtString)
        {
            switch (geschlechtString?.ToLower())
            {
                case "m":
                case "männlich":
                    return Person.GeschlechtTyp.Maennlich;
                case "w":
                case "weiblich":
                    return Person.GeschlechtTyp.Weiblich;
            }

            if (Enum.TryParse(geschlechtString?.Trim(), true, out Person.GeschlechtTyp geschlecht))
            {
                return geschlecht;
            }

            return null;
        }

        private static Person.NationalitaetTyp? GetNationalitaet(string? nationalitaetString)
        {
            switch (nationalitaetString?.ToLower())
            {
                case "ch":
                    return Person.NationalitaetTyp.CH;
                case "fl":
                    return Person.NationalitaetTyp.FL;
            }

            if (Enum.TryParse(nationalitaetString?.Trim(), true, out Person.NationalitaetTyp nationalitaet))
            {
                return nationalitaet;
            }

            return null;
        }

        private static Person.SpracheTyp? GetSprache(string? spracheString)
        {
            switch (spracheString?.ToLower())
            {
                case "de":
                    return Person.SpracheTyp.Deutsch;
                case "fr":
                    return Person.SpracheTyp.Franzoesisch;
                case "it":
                    return Person.SpracheTyp.Italienisch;
            }

            if (Enum.TryParse(spracheString?.Trim(), true, out Person.SpracheTyp sprache))
            {
                return sprache;
            }

            return null;
        }
    }
}
