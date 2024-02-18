using BCT.AWK.Converter.Anwesenheitskontrollen;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace BCT.AWK.Converter.Export.CsvExport
{
    internal class AktivitaetCsvWriter : ICsvExportWriter
    {
        private string _separator;

        public AktivitaetCsvWriter(string separator)
        {
            SetSeparator(separator);
        }

        public string Bezeichnung => "Aktivitaeten";

        [MemberNotNull(nameof(_separator))]
        public void SetSeparator(string separator)
        {
            _separator = separator;
        }

        public bool CanWrite(ExportKonfiguration konfiguration)
        {
            return konfiguration.AktivitaetenExport;
        }

        public void WriteKopfZeile(StreamWriter writer)
        {
            writer.Write("Aktivitaetstyp");
            writer.Write(_separator);
            writer.Write("Datum");
            writer.Write(_separator);
            writer.Write("Zeit");
            writer.Write(_separator);
            writer.Write("Dauer");
            writer.Write(_separator);
            writer.Write("Ort");
            writer.Write(_separator);
            writer.Write("Fokus");

            writer.WriteLine();
        }

        public int WriteZeilen(Anwesenheitskontrolle anwesenheitskontrolle, StreamWriter writer)
        {
            int exportiert = 0;
            foreach (Aktivitaet aktivitaet in anwesenheitskontrolle.Aktivitaeten)
            {
                WriteZeile(aktivitaet, writer);
                exportiert++;
            }

            return exportiert;
        }

        private void WriteZeile(Aktivitaet aktivitaet, StreamWriter writer)
        {
            writer.Write(aktivitaet.Art);
            writer.Write(_separator);
            writer.Write(aktivitaet.Datum?.ToString("dd.MM.yyyy"));
            writer.Write(_separator);
            writer.Write(aktivitaet.Zeit?.ToString("HH:mm"));
            writer.Write(_separator);
            writer.Write(aktivitaet.Dauer);
            writer.Write(_separator);
            writer.Write(aktivitaet.Ort);
            writer.Write(_separator);
            writer.Write(aktivitaet.Fokus);

            writer.WriteLine();
        }
    }
}
