using BCT.AWK.Converter.Anwesenheitskontrollen;
using OfficeOpenXml;
using System;
using System.Collections.Generic;

namespace BCT.AWK.Converter.Import.ExcelImport
{
    internal class ExcelImportAktivitaet
    {
        private readonly ImportKonfigurationAktivitaet _konfiguration;

        public ExcelImportAktivitaet(ImportKonfigurationAktivitaet konfiguration)
        {
            _konfiguration = konfiguration;
        }

        public Dictionary<int, Aktivitaet> Laden(ExcelWorksheet worksheet)
        {
            Dictionary<int, Aktivitaet> aktivitaetDictionary = new();

            for (int aktivitaetIndex = _konfiguration.ErsteAktivitaetSpalte; aktivitaetIndex <= _konfiguration.LetzteAktivitaetSpalte; aktivitaetIndex++)
            {
                Aktivitaet aktivitaet = GetAktivitaet(worksheet, aktivitaetIndex);
                if (aktivitaet.Art is not null)
                {
                    aktivitaetDictionary.Add(aktivitaetIndex, aktivitaet);
                }
            }

            return aktivitaetDictionary;
        }

        private Aktivitaet GetAktivitaet(ExcelWorksheet worksheet, int treiningIndex)
        {
            Aktivitaet.AktivitaetsTyp? art = GetArt(worksheet.Cells[_konfiguration.ArtZeile, treiningIndex].Value?.ToString());
            DateOnly? datum = ExcelImportDateTime.GetDate(worksheet.Cells[_konfiguration.DatumZeile, treiningIndex].Value);
            TimeOnly? zeit = ExcelImportDateTime.GetTime(worksheet.Cells[_konfiguration.ZeitZeile, treiningIndex].Value);
            double? dauer = (double?)worksheet.Cells[_konfiguration.DauerZeile, treiningIndex].Value;
            string? ort = worksheet.Cells[_konfiguration.OrtZeile, treiningIndex].Value?.ToString();

            Aktivitaet aktivitaet = new(art, datum, zeit, dauer, ort);
            return aktivitaet;
        }

        private static Aktivitaet.AktivitaetsTyp? GetArt(string? artString)
        {
            if (string.IsNullOrWhiteSpace(artString))
            {
                return null;
            }

            if (Enum.TryParse(artString.Trim(), true, out Aktivitaet.AktivitaetsTyp art))
            {
                return art;
            }

            return Aktivitaet.AktivitaetsTyp.Training;
        }
    }
}
