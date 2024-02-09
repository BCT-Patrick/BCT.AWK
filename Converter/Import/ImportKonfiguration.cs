using System;

namespace BCT.AWK.Converter.Import
{
    internal class ImportKonfiguration
    {
        public ImportKonfiguration()
        {
            Training = new();
            Teilnemer = new();
            Anwesenheit = new();
        }

        public string AwkPfad { get; set; } = "Awk";
        public string AwkBaltt { get; set; } = "AWK";

        public ImportKonfigurationTraining Training { get; set; }
        public ImportKonfigurationPerson Teilnemer { get; set; }
        public ImportKonfigurationAnwesenheit Anwesenheit { get; set; }

        public override string ToString()
        {
            string awkPfad = $"{nameof(AwkPfad)}= {AwkPfad}";
            string awkBaltt = $"{nameof(AwkBaltt)}= {AwkBaltt}";

            string separator = $"{Environment.NewLine}\t";
            string s = string.Join(separator, "Import Konfiguration", awkPfad, awkBaltt, Training, Teilnemer, Anwesenheit);
            return s;
        }
    }
}
