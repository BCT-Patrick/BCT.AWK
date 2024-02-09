using BCT.AWK.Converter.Anwesenheitskontrollen;
using OfficeOpenXml;
using System;
using System.Collections.Generic;

namespace BCT.AWK.Converter.Import.AnwesenheitImport
{
    internal static class ExcelAnwesenheitImport
    {
        public static List<Anwesenheit> Laden(Dictionary<int, Teilnehmer> teilnehmerDictionary, Dictionary<int, Training> trainingDictionary, ExcelWorksheet worksheet)
        {
            List<Anwesenheit> anwesenheiten = new();

            foreach (KeyValuePair<int, Training> trainingEintrag in trainingDictionary)
            {
                int treiningIndex = trainingEintrag.Key;
                Training training = trainingEintrag.Value;

                foreach (KeyValuePair<int, Teilnehmer> teilnehmerEintrag in teilnehmerDictionary)
                {
                    int teilnehmerIndex = teilnehmerEintrag.Key;
                    Teilnehmer teilnehmer = teilnehmerEintrag.Value;

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
    }
}
