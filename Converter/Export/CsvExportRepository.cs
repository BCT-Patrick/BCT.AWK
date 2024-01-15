using BCT.AWK.Converter.Anwesenheitskontrolle;
using System;
using System.Collections.Generic;
using System.IO;

namespace BCT.AWK.Converter.Export
{
    internal class CsvExportRepository : IExportRepository
    {
        public void Export(IEnumerable<Anwesenheit> anwesenheiten)
        {
            DateTime jetzt = DateTime.Now;
            FileStreamOptions options = new FileStreamOptions
            {
                Mode = FileMode.CreateNew,
                Access = FileAccess.ReadWrite,
                Share = FileShare.Read
            };
            using StreamWriter writer = new($"Anwesenheitskontrolle_{jetzt:yyyy-MM-dd HHmmss}.csv", options);
            AnwesenheitCsvWriter csvWriter = new(writer, ",");

            csvWriter.WriteKopfZeile();

            foreach (Anwesenheit anwesenheit in anwesenheiten)
            {
                if (anwesenheit.Anwesend)
                {
                    csvWriter.WriteZeile(anwesenheit);
                }
            }
        }
    }
}
