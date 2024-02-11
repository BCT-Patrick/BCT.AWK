using BCT.AWK.Converter.Anwesenheitskontrollen;
using OfficeOpenXml;
using System;
using System.Collections.Generic;

namespace BCT.AWK.Converter.Import.ExcelImport
{
    internal class ExcelImportTraining
    {
        private readonly ImportKonfigurationTraining _konfiguration;

        public ExcelImportTraining(ImportKonfigurationTraining konfiguration)
        {
            _konfiguration = konfiguration;
        }

        public Dictionary<int, Training> Laden(ExcelWorksheet worksheet)
        {
            Dictionary<int, Training> trainingDictionary = new();

            for (int trainingIndex = _konfiguration.ErsteTrainingSpalte; trainingIndex <= _konfiguration.LetzteTrainingSpalte; trainingIndex++)
            {
                Training training = GetTraining(worksheet, trainingIndex);
                if (training.Art is not null)
                {
                    trainingDictionary.Add(trainingIndex, training);
                }
            }

            return trainingDictionary;
        }

        private Training GetTraining(ExcelWorksheet worksheet, int treiningIndex)
        {
            Training.TrainingsTyp? art = GetArt(worksheet.Cells[_konfiguration.ArtZeile, treiningIndex].Value?.ToString());
            DateOnly? datum = ExcelImportDateTime.GetDate(worksheet.Cells[_konfiguration.DatumZeile, treiningIndex].Value);
            TimeOnly? zeit = ExcelImportDateTime.GetTime(worksheet.Cells[_konfiguration.ZeitZeile, treiningIndex].Value);
            double? dauer = (double?)worksheet.Cells[_konfiguration.DauerZeile, treiningIndex].Value;
            string? ort = worksheet.Cells[_konfiguration.OrtZeile, treiningIndex].Value?.ToString();

            Training training = new(art, datum, zeit, dauer, ort);
            return training;
        }

        private static Training.TrainingsTyp? GetArt(string? artString)
        {
            if (string.IsNullOrWhiteSpace(artString))
            {
                return null;
            }

            if (Enum.TryParse(artString.Trim(), true, out Training.TrainingsTyp art))
            {
                return art;
            }

            return Training.TrainingsTyp.Training;
        }
    }
}
