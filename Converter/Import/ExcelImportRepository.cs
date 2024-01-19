using BCT.AWK.Converter.Anwesenheitskontrolle;
using BCT.AWK.Converter.Konfiguration;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace BCT.AWK.Converter.Import
{
    internal class ExcelImportRepository : IImportRepository
    {
        private readonly ImportKonfiguration _konfiguration;

        public ExcelImportRepository(ImportKonfiguration konfiguration)
        {
            _konfiguration = konfiguration;
        }

        public List<Anwesenheit> Laden(FileInfo excelFile)
        {
            FileStream fileStream = new(excelFile.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage package = new(fileStream);
            ExcelWorksheet worksheet = package.Workbook.Worksheets[_konfiguration.AwkBaltt];

            List<Anwesenheit> anwesenheiten = GetAnwesenheiten(worksheet);

            return anwesenheiten;
        }

        private List<Anwesenheit> GetAnwesenheiten(ExcelWorksheet worksheet)
        {
            List<Anwesenheit> anwesenheiten = new();

            for (int teilnehmerIndex = _konfiguration.Teilnemer.ErsteTeilnehmerZeile; teilnehmerIndex <= _konfiguration.Teilnemer.LetzteTeilnehmerZeile; teilnehmerIndex++)
            {
                Teilnehmer? teilnehmer = GetTeilnehmer(worksheet, teilnehmerIndex);
                if (teilnehmer is null)
                {
                    continue;
                }

                for (int treiningIndex = _konfiguration.Training.ErsteTrainingSpalte; treiningIndex <= _konfiguration.Training.LetzteTrainingSpalte; treiningIndex++)
                {
                    Training? training = GetTraining(worksheet, treiningIndex);
                    if (training is null)
                    {
                        continue;
                    }

                    bool anwesend = GetAnwesenheit(worksheet.Cells[teilnehmerIndex, treiningIndex].Value?.ToString());
                    Anwesenheit anwesenheit = new(teilnehmer, training, anwesend);
                    anwesenheiten.Add(anwesenheit);
                }
            }

            return anwesenheiten;
        }

        private static bool GetAnwesenheit(string? anwesenheit)
        {
            if (string.IsNullOrWhiteSpace(anwesenheit))
            {
                return false;
            }

            if (anwesenheit.Equals("ja", StringComparison.InvariantCultureIgnoreCase)
                || anwesenheit.Equals("true", StringComparison.InvariantCultureIgnoreCase)
                || anwesenheit.Equals("wahr", StringComparison.InvariantCultureIgnoreCase)
                || anwesenheit.Equals("x", StringComparison.InvariantCultureIgnoreCase)
                || anwesenheit.Equals("1", StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }

            return false;
        }

        private Training? GetTraining(ExcelWorksheet worksheet, int treiningIndex)
        {
            string? artString = worksheet.Cells[_konfiguration.Training.ArtZeile, treiningIndex].Value?.ToString();
            DateTime? datum = GetDateTime(worksheet.Cells[_konfiguration.Training.DatumZeile, treiningIndex].Value);
            double? dauer = (double?)worksheet.Cells[_konfiguration.Training.DauerZeile, treiningIndex].Value;
            if (artString is null || datum is null || dauer is null)
            {
                return null;
            }

            Training.TrainingsTyp art = GetArt(artString);
            DateTime? zeit = GetDateTime(worksheet.Cells[_konfiguration.Training.ZeitZeile, treiningIndex].Value);
            string? ort = worksheet.Cells[_konfiguration.Training.OrtZeile, treiningIndex].Value?.ToString();

            Training training = new(art, datum.Value, zeit, dauer.Value, ort);
            return training;
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

        private static Training.TrainingsTyp GetArt(string artString)
        {
            if (Enum.TryParse(artString.Trim(), true, out Training.TrainingsTyp art))
            {
                return art;
            }

            return Training.TrainingsTyp.Training;
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

            if(value is double doubleValue)
            {
                DateTime date = DateTime.FromOADate(doubleValue);
                return date;
            }

            if(value is string stringValue)
            {
                bool isDateTime = DateTime.TryParse(stringValue, CultureInfo.InvariantCulture, out DateTime dateTime);
                return isDateTime ? dateTime : null;
            }

            return null;
        }
    }
}
