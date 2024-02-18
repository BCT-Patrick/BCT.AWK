using System;

namespace BCT.AWK.Converter.Export
{
    internal class ExportKonfiguration
    {
        public string FileZeitstempelFormat { get; set; } = "yyyy-MM-dd HHmmss";
        public string FileExtension { get; set; } = ".csv";
        public string Separator { get; set; } = ";";
        public bool PersonenExport { get; set; } = true;
        public bool AnwesenheitenExport { get; set; } = true;
        public bool AktivitaetenExport { get; set; } = true;

        public override string ToString()
        {
            string fileZeitstempelFormat = $"{nameof(FileZeitstempelFormat)}= {FileZeitstempelFormat}";
            string fileExtension = $"{nameof(FileExtension)}= {FileExtension}";
            string exportSeparator = $"{nameof(Separator)}= {Separator}";
            string personenExport = $"{nameof(PersonenExport)}= {PersonenExport}";
            string anwesenheitenExport = $"{nameof(AnwesenheitenExport)}= {AnwesenheitenExport}";
            string aktivitaetenExport = $"{nameof(AktivitaetenExport)}= {AktivitaetenExport}";

            string separator = $"{Environment.NewLine}\t";
            string s = string.Join(separator, "Export Konfiguration:", fileZeitstempelFormat, fileExtension, exportSeparator, personenExport, anwesenheitenExport, aktivitaetenExport);
            return s;
        }
    }
}
