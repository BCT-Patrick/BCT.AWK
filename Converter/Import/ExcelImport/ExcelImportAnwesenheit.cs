using BCT.AWK.Converter.Anwesenheitskontrollen;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using System;
using System.Collections.Generic;

namespace BCT.AWK.Converter.Import.AnwesenheitImport
{
    internal class ExcelImportAnwesenheit
    {
        private readonly ImportKonfigurationAnwesenheit _konfiguration;

        public ExcelImportAnwesenheit(ImportKonfigurationAnwesenheit konfiguration)
        {
            _konfiguration = konfiguration;
        }

        public List<Anwesenheit> Laden(Dictionary<int, Person> teilnehmerDictionary, Dictionary<int, Training> trainingDictionary, ExcelWorksheet worksheet)
        {
            List<Anwesenheit> anwesenheiten = new();

            foreach (KeyValuePair<int, Training> trainingEintrag in trainingDictionary)
            {

                foreach (KeyValuePair<int, Person> teilnehmerEintrag in teilnehmerDictionary)
                {
                    Anwesenheit anwesenheit = GetAnwesenheit(worksheet, trainingEintrag, teilnehmerEintrag);
                    anwesenheiten.Add(anwesenheit);
                }
            }

            return anwesenheiten;
        }

        private Anwesenheit GetAnwesenheit(ExcelWorksheet worksheet, KeyValuePair<int, Training> trainingEintrag, KeyValuePair<int, Person> teilnehmerEintrag)
        {
            int treiningIndex = trainingEintrag.Key;
            Training training = trainingEintrag.Value;

            int teilnehmerIndex = teilnehmerEintrag.Key;
            Person teilnehmer = teilnehmerEintrag.Value;

            Anwesenheit.FunktionsTyp? funktion = GetFunktio(worksheet.Cells[teilnehmerIndex, _konfiguration.FunktionSpalte].Value?.ToString());
            bool anwesend = GetAnwesenheit(worksheet.Cells[teilnehmerIndex, treiningIndex].Value?.ToString());
            Anwesenheit anwesenheit = new(funktion, teilnehmer, training, anwesend);
            return anwesenheit;
        }

        private static Anwesenheit.FunktionsTyp? GetFunktio(string? funktion)
        {
            if (string.IsNullOrWhiteSpace(funktion))
            {
                return null;
            }

            if (funktion.Contains("leiter", StringComparison.InvariantCultureIgnoreCase))
            {
                return Anwesenheit.FunktionsTyp.Leiter;
            }

            return Anwesenheit.FunktionsTyp.Teilnehmer;
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
    }
}
