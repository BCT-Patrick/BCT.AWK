using System;

namespace BCT.AWK.Converter.Import
{
    internal class ImportKonfiguration
    {
        public ImportKonfiguration()
        {
            Aktivitaeten = new();
            Personen = new();
            Anwesenheiten = new();
        }

        public string AwkPfad { get; set; } = "Awk";
        public string AwkBaltt { get; set; } = "AWK";

        public ImportKonfigurationAktivitaet Aktivitaeten { get; set; }
        public ImportKonfigurationPerson Personen { get; set; }
        public ImportKonfigurationAnwesenheit Anwesenheiten { get; set; }

        public override string ToString()
        {
            string awkPfad = $"{nameof(AwkPfad)}= {AwkPfad}";
            string awkBaltt = $"{nameof(AwkBaltt)}= {AwkBaltt}";

            string separator = $"{Environment.NewLine}\t";
            string s = string.Join(separator, "Import Konfiguration", awkPfad, awkBaltt, Aktivitaeten, Personen, Anwesenheiten);
            return s;
        }
    }
}
