using BCT.AWK.Converter.Anwesenheitskontrollen;
using System.IO;

namespace BCT.AWK.Converter.Export.CsvExport
{
    internal class TeilnehmerCsvWriter
    {
        private readonly StreamWriter _writer;

        public TeilnehmerCsvWriter(StreamWriter writer)
        {
            _writer = writer;
        }

        public void WriteKopfZeile()
        {
            _writer.Write("Personennummer");
        }

        public void WriteZeile(Person teilnehmer)
        {
            _writer.Write(teilnehmer.Nummer);
        }
    }
}
