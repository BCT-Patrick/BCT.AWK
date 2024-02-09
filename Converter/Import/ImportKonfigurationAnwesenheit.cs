using System;

namespace BCT.AWK.Converter.Import
{
    internal class ImportKonfigurationAnwesenheit
    {
        public int FunktionSpalte { get; set; } = 15;

        public override string ToString()
        {
            string funktionSpalte = $"{nameof(FunktionSpalte)}= {FunktionSpalte}";

            string separator = $"{Environment.NewLine}\t\t";
            string s = string.Join(separator, "Anwesenheit Konfiguration:", funktionSpalte);
            return s;
        }
    }
}
