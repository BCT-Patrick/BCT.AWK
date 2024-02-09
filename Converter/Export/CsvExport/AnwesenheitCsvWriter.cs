using BCT.AWK.Converter.Anwesenheitskontrollen;
using System.IO;

namespace BCT.AWK.Converter.Export.CsvExport
{
    internal class AnwesenheitCsvWriter
    {
        private readonly StreamWriter _writer;
        private readonly string _separator;

        private readonly TeilnehmerCsvWriter _teilnehmerCsvFormatierer;
        private readonly TrainingCsvFormatierer _trainingCsvFormatierer;

        public AnwesenheitCsvWriter(StreamWriter writer, string separator)
        {
            _writer = writer;
            _separator = separator;

            _teilnehmerCsvFormatierer = new(writer);
            _trainingCsvFormatierer = new(writer, separator);
        }

        public void WriteKopfZeile()
        {
            _teilnehmerCsvFormatierer.WriteKopfZeile();
            _writer.Write(_separator);
            _writer.Write("Funktion");
            _writer.Write(_separator);
            _trainingCsvFormatierer.WriteKopfZeile();
            _writer.WriteLine();
        }

        public void WriteZeile(Anwesenheit anwesenheit)
        {
            _teilnehmerCsvFormatierer.WriteZeile(anwesenheit.Teilnehmer);
            _writer.Write(_separator);
            string funktion = anwesenheit.Funktion == Anwesenheit.FunktionsTyp.Leiter ? "Leiter/in" : "Teilnehmer/in";
            _writer.Write(funktion);
            _writer.Write(_separator);
            _trainingCsvFormatierer.WriteZeile(anwesenheit.Training);
            _writer.WriteLine();
        }
    }
}
