using BCT.AWK.Converter.Anwesenheitskontrollen;
using System;
using System.Collections.Generic;
using System.IO;

namespace BCT.AWK.Converter.Export.CsvExport
{
    internal class CsvExport : IExport
    {
        private readonly ExportKonfiguration _exportKonfiguration;

        public CsvExport(ExportKonfiguration exportKonfiguration)
        {
            _exportKonfiguration = exportKonfiguration;
        }

        public int Export(IEnumerable<Anwesenheit> anwesenheiten, FileInfo excelFile)
        {
            DateTime jetzt = DateTime.Now;
            string zeitstempel = jetzt.ToString(_exportKonfiguration.FileZeitstempelFormat);
            string fileName = $"{excelFile.FullName}_{zeitstempel}{_exportKonfiguration.FileExtension}";

            FileStreamOptions options = new()
            {
                Mode = FileMode.CreateNew,
                Access = FileAccess.ReadWrite,
                Share = FileShare.Read
            };

            using StreamWriter writer = new(fileName, options);
            AnwesenheitCsvWriter csvWriter = new(writer, _exportKonfiguration.Separator);

            csvWriter.WriteKopfZeile();

            int exportiert = 0;
            foreach (Anwesenheit anwesenheit in anwesenheiten)
            {
                if (anwesenheit.Anwesend)
                {
                    csvWriter.WriteZeile(anwesenheit);
                    exportiert++;
                }
            }
            return exportiert;
        }
    }
}
