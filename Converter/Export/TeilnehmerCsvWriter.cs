using BCT.AWK.Converter.Anwesenheitskontrolle;
using System.IO;

namespace BCT.AWK.Converter.Export
{
    internal class TeilnehmerCsvWriter
    {
        private readonly StreamWriter _writer;
        private readonly string _separator;

        public TeilnehmerCsvWriter(StreamWriter writer, string separator)
        {
            _writer = writer;
            _separator = separator;
        }

        public void WriteKopfZeile()
        {
            _writer.Write("Personennummer");
            _writer.Write(_separator);
            _writer.Write("Funktion");
        }

        public void WriteZeile(Teilnehmer teilnehmer)
        {
            _writer.Write(teilnehmer.Nummer);
            _writer.Write(_separator);
            string funktion = teilnehmer.Funktion == Teilnehmer.FunktionsTyp.Leiter ? "Leiter/in" : "Teilnehmer/in";
            _writer.Write(funktion);
        }
    }
}
