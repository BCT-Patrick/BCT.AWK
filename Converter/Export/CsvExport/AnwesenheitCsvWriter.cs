using BCT.AWK.Converter.Anwesenheitskontrollen;
using System.IO;

namespace BCT.AWK.Converter.Export.CsvExport
{
    internal class AnwesenheitCsvWriter
    {
        private readonly StreamWriter _writer;
        private readonly string _separator;

        public AnwesenheitCsvWriter(StreamWriter writer, string separator)
        {
            _writer = writer;
            _separator = separator;
        }

        public void WriteKopfZeile()
        {
            _writer.Write("Personennummer");
            _writer.Write(_separator);
            _writer.Write("Funktion");
            _writer.Write(_separator);
            _writer.Write("Datum");
            _writer.Write(_separator);
            _writer.Write("Aktivitaetstyp");
            _writer.Write(_separator);
            _writer.Write("Zeit");
            _writer.Write(_separator);
            _writer.Write("Dauer");
            _writer.Write(_separator);
            _writer.Write("Ort");

            _writer.WriteLine();
        }

        public void WriteZeile(Anwesenheit anwesenheit)
        {
            _writer.Write(anwesenheit.Teilnehmer.Nummer);
            _writer.Write(_separator);
            string funktion = anwesenheit.Funktion == Anwesenheit.FunktionsTyp.Leiter ? "Leiter/in" : "Teilnehmer/in";
            _writer.Write(funktion);
            _writer.Write(_separator);
            _writer.Write(anwesenheit.Training.Datum?.ToString("dd.MM.yyyy"));
            _writer.Write(_separator);
            _writer.Write(anwesenheit.Training.Art);
            _writer.Write(_separator);
            _writer.Write(anwesenheit.Training.Zeit?.ToString("HH:mm"));
            _writer.Write(_separator);
            _writer.Write(anwesenheit.Training.Dauer);
            _writer.Write(_separator);
            _writer.Write(anwesenheit.Training.Ort);

            _writer.WriteLine();
        }
    }
}
