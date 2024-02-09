using BCT.AWK.Converter.Anwesenheitskontrollen;
using System.IO;

namespace BCT.AWK.Converter.Export.CsvExport
{
    internal class TrainingCsvFormatierer
    {
        private readonly StreamWriter _writer;
        private readonly string _separator;

        public TrainingCsvFormatierer(StreamWriter writer, string separator)
        {
            _writer = writer;
            _separator = separator;
        }

        public void WriteKopfZeile()
        {
            _writer.Write("Datum");
            _writer.Write(_separator);
            _writer.Write("Aktivitaetstyp");
            _writer.Write(_separator);
            _writer.Write("Zeit");
            _writer.Write(_separator);
            _writer.Write("Dauer");
            _writer.Write(_separator);
            _writer.Write("Ort");
        }

        public void WriteZeile(Training training)
        {
            _writer.Write(training.Datum.ToString("dd.MM.yyyy"));
            _writer.Write(_separator);
            _writer.Write(training.Art);
            _writer.Write(_separator);
            _writer.Write(training.Zeit?.ToString("HH:mm"));
            _writer.Write(_separator);
            _writer.Write(training.Dauer);
            _writer.Write(_separator);
            _writer.Write(training.Ort);
        }
    }
}
