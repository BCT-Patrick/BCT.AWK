using BCT.AWK.Converter.Anwesenheitskontrollen;
using System.IO;

namespace BCT.AWK.Converter.Export
{
    internal interface ICsvExportWriter
    {
        string Bezeichnung {  get; }
        void SetSeparator(string separator);
        bool CanWrite(ExportKonfiguration konfiguration);
        void WriteKopfZeile(StreamWriter writer);
        int WriteZeilen(Anwesenheitskontrolle anwesenheitskontrolle, StreamWriter writer);
    }
}