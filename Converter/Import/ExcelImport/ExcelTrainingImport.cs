using BCT.AWK.Converter.Anwesenheitskontrollen;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace BCT.AWK.Converter.Import.ExcelImport
{
    internal class ExcelTrainingImport
    {
        private readonly ImportKonfiguration _konfiguration;

        public ExcelTrainingImport(ImportKonfiguration konfiguration)
        {
            _konfiguration = konfiguration;
        }

        public Dictionary<int, Training> Laden(ExcelWorksheet worksheet)
        {
            Dictionary<int, Training> trainingDictionary = new();

            for (int trainingIndex = _konfiguration.Training.ErsteTrainingSpalte; trainingIndex <= _konfiguration.Training.LetzteTrainingSpalte; trainingIndex++)
            {
                Training? training = GetTraining(worksheet, trainingIndex);
                if (training is not null)
                {
                    trainingDictionary.Add(trainingIndex, training);
                }
            }

            return trainingDictionary;
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

        private static Training.TrainingsTyp GetArt(string artString)
        {
            if (Enum.TryParse(artString.Trim(), true, out Training.TrainingsTyp art))
            {
                return art;
            }

            return Training.TrainingsTyp.Training;
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
                bool isDateTime = DateTime.TryParse(stringValue, CultureInfo.InvariantCulture, out DateTime dateTime);
                return isDateTime ? dateTime : null;
            }

            return null;
        }
    }
}
