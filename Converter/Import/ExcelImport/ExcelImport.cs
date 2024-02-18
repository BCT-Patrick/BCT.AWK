using BCT.AWK.Converter.Anwesenheitskontrollen;
using BCT.AWK.Converter.Import.AnwesenheitImport;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BCT.AWK.Converter.Import.ExcelImport
{
    internal class ExcelImport : IImport
    {
        private readonly ImportKonfiguration _konfiguration;

        private readonly ExcelImportPerson _importPerson;
        private readonly ExcelImportAktivitaet _importAktivitaet;
        private readonly ExcelImportAnwesenheit _importAnwesenheit;

        public ExcelImport(ImportKonfiguration konfiguration)
        {
            _konfiguration = konfiguration;

            _importPerson = new(konfiguration.Personen);
            _importAktivitaet = new(konfiguration.Aktivitaeten);
            _importAnwesenheit = new(konfiguration.Anwesenheiten);
        }

        public Anwesenheitskontrolle Laden(FileInfo excelFile)
        {
            using FileStream fileStream = new(excelFile.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage package = new(fileStream);
            ExcelWorksheet worksheet = package.Workbook.Worksheets[_konfiguration.AwkBaltt];

            Dictionary<int, Person> personDictionary = _importPerson.Laden(worksheet);
            Dictionary<int, Aktivitaet> aktivitaetDictionary = _importAktivitaet.Laden(worksheet);
            List<Anwesenheit> anwesenheiten = _importAnwesenheit.Laden(personDictionary, aktivitaetDictionary, worksheet);

            Anwesenheitskontrolle anwesenheitskontrolle = new(personDictionary.Values.ToList(), aktivitaetDictionary.Values.ToList(), anwesenheiten);
            return anwesenheitskontrolle;
        }

    }
}
