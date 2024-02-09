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

        private readonly ExcelTeilnehmerImport _teilnehmerImporter;
        private readonly ExcelTrainingImport _trainingsImporter;

        public ExcelImport(ImportKonfiguration konfiguration)
        {
            _konfiguration = konfiguration;

            _teilnehmerImporter = new(konfiguration);
            _trainingsImporter = new(konfiguration);
        }

        public Anwesenheitskontrolle Laden(FileInfo excelFile)
        {
            using FileStream fileStream = new(excelFile.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage package = new(fileStream);
            ExcelWorksheet worksheet = package.Workbook.Worksheets[_konfiguration.AwkBaltt];

            Dictionary<int, Teilnehmer> teilnehmerDictionary = _teilnehmerImporter.Laden(worksheet);
            Dictionary<int, Training> trainingDictionary = _trainingsImporter.Laden(worksheet);
            List<Anwesenheit> anwesenheiten = ExcelAnwesenheitImport.Laden(teilnehmerDictionary, trainingDictionary, worksheet);

            Anwesenheitskontrolle anwesenheitskontrolle = new(teilnehmerDictionary.Values.ToList(), trainingDictionary.Values.ToList(), anwesenheiten);
            return anwesenheitskontrolle;
        }

    }
}
