using BCT.AWK.Converter.Anwesenheitskontrollen;
using System;
using System.Collections.Generic;
using System.IO;

namespace BCT.AWK.Converter.Export.CsvExport
{
    internal class CsvExport : IExport
    {
        private readonly ExportKonfiguration _konfiguration;

        public CsvExport(ExportKonfiguration konfiguration)
        {
            _konfiguration = konfiguration;
        }

        public int Export(Anwesenheitskontrolle anwesenheitskontrolle, FileInfo excelFile)
        {
            int anwesenheitenExport = 0;
            int personenExport = 0;
            int aktivitaetenExport = 0;

            if (_konfiguration.AnwesenheitenExport)
            {
                anwesenheitenExport =ExportAnwesenheiten(anwesenheitskontrolle.Anwesenheiten, excelFile);
            }

            if (_konfiguration.PersonenExport)
            {
                personenExport = ExportPersonen(anwesenheitskontrolle.Personen, excelFile);
            }

            if (_konfiguration.AnwesenheitenExport)
            {
                aktivitaetenExport = ExportAktivitaeten(anwesenheitskontrolle.Aktivitaeten, excelFile);
            }

            return anwesenheitenExport + personenExport + aktivitaetenExport;
        }

        public int ExportAnwesenheiten(IEnumerable<Anwesenheit> anwesenheiten, FileInfo excelFile)
        {
            DateTime jetzt = DateTime.Now;
            string zeitstempel = jetzt.ToString(_konfiguration.FileZeitstempelFormat);
            string fileName = $"{excelFile.FullName}_AWK_{zeitstempel}{_konfiguration.FileExtension}";

            FileStreamOptions options = new()
            {
                Mode = FileMode.CreateNew,
                Access = FileAccess.ReadWrite,
                Share = FileShare.Read
            };

            using StreamWriter writer = new(fileName, options);
            AnwesenheitCsvWriter csvWriter = new(writer, _konfiguration.Separator);

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

        public int ExportPersonen(IEnumerable<Person> personen, FileInfo excelFile)
        {
            DateTime jetzt = DateTime.Now;
            string zeitstempel = jetzt.ToString(_konfiguration.FileZeitstempelFormat);
            string fileName = $"{excelFile.FullName}_Personen_{zeitstempel}{_konfiguration.FileExtension}";

            FileStreamOptions options = new()
            {
                Mode = FileMode.CreateNew,
                Access = FileAccess.ReadWrite,
                Share = FileShare.Read
            };

            using StreamWriter writer = new(fileName, options);
            PersonCsvWriter csvWriter = new(writer, _konfiguration.Separator);

            csvWriter.WriteKopfZeile();

            int exportiert = 0;
            foreach (Person person in personen)
            {
                csvWriter.WriteZeile(person);
                exportiert++;
            }
            return exportiert;
        }

        public int ExportAktivitaeten(IEnumerable<Aktivitaet> aktivitaeten, FileInfo excelFile)
        {
            DateTime jetzt = DateTime.Now;
            string zeitstempel = jetzt.ToString(_konfiguration.FileZeitstempelFormat);
            string fileName = $"{excelFile.FullName}_Aktivitaeten_{zeitstempel}{_konfiguration.FileExtension}";

            FileStreamOptions options = new()
            {
                Mode = FileMode.CreateNew,
                Access = FileAccess.ReadWrite,
                Share = FileShare.Read
            };

            using StreamWriter writer = new(fileName, options);
            AktivitaetCsvWriter csvWriter = new(writer, _konfiguration.Separator);

            csvWriter.WriteKopfZeile();

            int exportiert = 0;
            foreach (Aktivitaet aktivitaet in aktivitaeten)
            {
                csvWriter.WriteZeile(aktivitaet);
                exportiert++;
            }
            return exportiert;
        }
    }
}
