using BCT.AWK.Converter.Anwesenheitskontrollen;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace BCT.AWK.Converter.Export.CsvExport
{
    internal class AnwesenheitCsvWriter : ICsvExportWriter
    {
        private string _separator;

        public AnwesenheitCsvWriter(string separator)
        {
            SetSeparator(separator);
        }

        public string Bezeichnung => "Anwesenheiten";

        [MemberNotNull(nameof(_separator))]
        public void SetSeparator(string separator)
        {
            _separator = separator;
        }

        public bool CanWrite(ExportKonfiguration konfiguration)
        {
            return konfiguration.AnwesenheitenExport;
        }

        public void WriteKopfZeile(StreamWriter writer)
        {
            writer.Write("Personennummer");
            writer.Write(_separator);
            writer.Write("Funktion");
            writer.Write(_separator);
            writer.Write("Datum");
            writer.Write(_separator);
            writer.Write("Aktivitaetstyp");
            writer.Write(_separator);
            writer.Write("Zeit");
            writer.Write(_separator);
            writer.Write("Dauer");
            writer.Write(_separator);
            writer.Write("Ort");

            writer.WriteLine();
        }

        public int WriteZeilen(Anwesenheitskontrolle anwesenheitskontrolle, StreamWriter writer)
        {
            int exportiert = 0;
            foreach (Anwesenheit anwesenheit in anwesenheitskontrolle.Anwesenheiten)
            {
                if (anwesenheit.Anwesend)
                {
                    WriteZeile(anwesenheit, writer);
                    exportiert++;
                }
            }

            return exportiert;
        }

        private void WriteZeile(Anwesenheit anwesenheit, StreamWriter writer)
        {
            writer.Write(anwesenheit.Person.Nummer);
            writer.Write(_separator);
            string funktion = anwesenheit.Funktion == Anwesenheit.FunktionsTyp.Leiter ? "Leiter/in" : "Teilnehmer/in";
            writer.Write(funktion);
            writer.Write(_separator);
            writer.Write(anwesenheit.Aktivitaet.Datum?.ToString("dd.MM.yyyy"));
            writer.Write(_separator);
            writer.Write(anwesenheit.Aktivitaet.Art);
            writer.Write(_separator);
            writer.Write(anwesenheit.Aktivitaet.Zeit?.ToString("HH:mm"));
            writer.Write(_separator);
            writer.Write(anwesenheit.Aktivitaet.Dauer);
            writer.Write(_separator);
            writer.Write(anwesenheit.Aktivitaet.Ort);

            writer.WriteLine();
        }
    }
}
