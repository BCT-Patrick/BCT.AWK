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
        private readonly ExcelImportTraining _importTraining;
        private readonly ExcelImportAnwesenheit _importAnwesenheit;

        public ExcelImport(ImportKonfiguration konfiguration)
        {
            _konfiguration = konfiguration;

            _importPerson = new(konfiguration.Personen);
            _importTraining = new(konfiguration.Training);
            _importAnwesenheit = new(konfiguration.Anwesenheit);
        }

        public Anwesenheitskontrolle Laden(FileInfo excelFile)
        {
            using FileStream fileStream = new(excelFile.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage package = new(fileStream);
            ExcelWorksheet worksheet = package.Workbook.Worksheets[_konfiguration.AwkBaltt];

            Dictionary<int, Person> teilnehmerDictionary = _importPerson.Laden(worksheet);
            Dictionary<int, Training> trainingDictionary = _importTraining.Laden(worksheet);
            List<Anwesenheit> anwesenheiten = _importAnwesenheit.Laden(teilnehmerDictionary, trainingDictionary, worksheet);

            Anwesenheitskontrolle anwesenheitskontrolle = new(teilnehmerDictionary.Values.ToList(), trainingDictionary.Values.ToList(), anwesenheiten);
            return anwesenheitskontrolle;
        }

    }
}
