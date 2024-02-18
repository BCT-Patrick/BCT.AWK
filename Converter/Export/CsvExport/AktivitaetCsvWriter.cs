using BCT.AWK.Converter.Anwesenheitskontrollen;
using System.IO;

namespace BCT.AWK.Converter.Export.CsvExport
{
    internal class AktivitaetCsvWriter
    {
        private readonly StreamWriter _writer;
        private readonly string _separator;

        public AktivitaetCsvWriter(StreamWriter writer, string separator)
        {
            _writer = writer;
            _separator = separator;
        }

        public void WriteKopfZeile()
        {
            _writer.Write("Aktivitaetstyp");
            _writer.Write(_separator);
            _writer.Write("Datum");
            _writer.Write(_separator);
            _writer.Write("Zeit");
            _writer.Write(_separator);
            _writer.Write("Dauer");
            _writer.Write(_separator);
            _writer.Write("Ort");
            _writer.Write(_separator);
            _writer.Write("Fokus");

            _writer.WriteLine();
        }

        public void WriteZeile(Aktivitaet aktivitaet)
        {
            _writer.Write(aktivitaet.Art);
            _writer.Write(_separator);
            _writer.Write(aktivitaet.Datum?.ToString("dd.MM.yyyy"));
            _writer.Write(_separator);
            _writer.Write(aktivitaet.Zeit?.ToString("HH:mm"));
            _writer.Write(_separator);
            _writer.Write(aktivitaet.Dauer);
            _writer.Write(_separator);
            _writer.Write(aktivitaet.Ort);
            _writer.Write(_separator);
            _writer.Write(aktivitaet.Fokus);

            _writer.WriteLine();
        }
    }
}
