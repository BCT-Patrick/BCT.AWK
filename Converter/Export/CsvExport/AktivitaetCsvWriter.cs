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

        }

        public void WriteZeile(Aktivitaet aktivitaet)
        {

        }
    }
}
