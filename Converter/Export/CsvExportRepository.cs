using BCT.AWK.Converter.Anwesenheitskontrolle;
using System;
using System.Collections.Generic;
using System.IO;

namespace BCT.AWK.Converter.Export
{
    internal class CsvExportRepository : IExportRepository
    {
        private readonly ExportKonfiguration _exportKonfiguration;

        public CsvExportRepository(ExportKonfiguration exportKonfiguration)
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
