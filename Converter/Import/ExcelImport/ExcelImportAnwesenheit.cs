using BCT.AWK.Converter.Anwesenheitskontrollen;
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

        public List<Anwesenheit> Laden(Dictionary<int, Person> personDictionary, Dictionary<int, Aktivitaet> aktivitaetDictionary, ExcelWorksheet worksheet)
        {
            List<Anwesenheit> anwesenheiten = new();

            foreach (KeyValuePair<int, Aktivitaet> aktivitaetEintrag in aktivitaetDictionary)
            {

                foreach (KeyValuePair<int, Person> personEintrag in personDictionary)
                {
                    Anwesenheit anwesenheit = GetAnwesenheit(worksheet, aktivitaetEintrag, personEintrag);
                    anwesenheiten.Add(anwesenheit);
                }
            }

            return anwesenheiten;
        }

        private Anwesenheit GetAnwesenheit(ExcelWorksheet worksheet, KeyValuePair<int, Aktivitaet> aktivitaetEintrag, KeyValuePair<int, Person> PersonEintrag)
        {
            int aktivitaetIndex = aktivitaetEintrag.Key;
            Aktivitaet aktivitaet = aktivitaetEintrag.Value;

            int personIndex = PersonEintrag.Key;
            Person person = PersonEintrag.Value;

            Anwesenheit.FunktionsTyp? funktion = GetFunktio(worksheet.Cells[personIndex, _konfiguration.FunktionSpalte].Value?.ToString());
            bool anwesend = GetAnwesenheit(worksheet.Cells[personIndex, aktivitaetIndex].Value?.ToString());
            Anwesenheit anwesenheit = new(funktion, person, aktivitaet, anwesend);
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
