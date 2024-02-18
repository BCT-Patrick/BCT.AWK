using BCT.AWK.Converter.Anwesenheitskontrollen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BCT.AWK.Converter.Export.CsvExport
{
    internal class CsvExport : IExport
    {
        private readonly ExportKonfiguration _konfiguration;
        private readonly IList<ICsvExportWriter> _exportWriters;

        public CsvExport(ExportKonfiguration konfiguration, IList<ICsvExportWriter> exportWriters)
        {
            _konfiguration = konfiguration;
            _exportWriters = exportWriters;
        }

        public List<string> Export(Anwesenheitskontrolle anwesenheitskontrolle, FileInfo excelFile)
        {
            DateTime jetzt = DateTime.Now;

            List<string> resultate = _exportWriters.Select(exportWriter=>Export(jetzt, anwesenheitskontrolle, excelFile, exportWriter)).ToList();

            return resultate;
        }

        private string Export(DateTime jetzt, Anwesenheitskontrolle anwesenheitskontrolle, FileInfo excelFile, ICsvExportWriter exportWriter)
        {
            if (!exportWriter.CanWrite(_konfiguration))
            {
                return $"{exportWriter.Bezeichnung} = ausgelassen";
            }

            string zeitstempel = jetzt.ToString(_konfiguration.FileZeitstempelFormat);
            string fileName = $"{excelFile.FullName}_{exportWriter.Bezeichnung}_{zeitstempel}{_konfiguration.FileExtension}";

            FileStreamOptions options = new()
            {
                Mode = FileMode.CreateNew,
                Access = FileAccess.ReadWrite,
                Share = FileShare.Read
            };

            using StreamWriter writer = new(fileName, options);

            exportWriter.SetSeparator(_konfiguration.Separator);
            exportWriter.WriteKopfZeile(writer);
            int exportiert = exportWriter.WriteZeilen(anwesenheitskontrolle, writer);

            return $"{exportWriter.Bezeichnung} = {exportiert}";
        }
    }
}
