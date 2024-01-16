using System;

namespace BCT.AWK.Converter.Export
{
    public class ExportKonfiguration
    {
        public string FileName { get; set; } = "Anwesenheitskontrolle";
        public string FileZeitstempelFormat { get; set; } = "yyyy-MM-dd HHmmss";
        public string FileExtension { get; set; } = ".csv";
        public string Separator { get; set; } = ";";

        public override string ToString()
        {
            string separator = $"{Environment.NewLine}\t";
            string s = string.Join(separator, "Export Konfiguration:", FileName, FileZeitstempelFormat, FileExtension, Separator);
            return s;
        }
    }
}
