using BCT.AWK.Converter.Anwesenheitskontrollen;
using BCT.AWK.Converter.Export;
using BCT.AWK.Converter.Export.CsvExport;
using BCT.AWK.Converter.Import;
using BCT.AWK.Converter.Import.ExcelImport;
using BCT.AWK.Converter.Konfiguration;
using System;
using System.Collections.Generic;
using System.IO;

namespace BCT.AWK.Converter
{
    internal static class Program
    {
        static void Main()
        {
            try
            {
                Console.WriteLine("Konfiguration Laden");
                ConverterKonfiguration konfiguration = GetKonfiguration();
                Console.WriteLine(konfiguration);
                Console.WriteLine();

                Console.WriteLine("Excel Files Konvertieren");
                List<FileInfo> excelFiles = GetExcelFiles(konfiguration.Import);
                Console.WriteLine($"Excel Files ({excelFiles.Count}):");
                string anwesenheitenString = string.Join(Environment.NewLine, excelFiles);
                Console.WriteLine(anwesenheitenString);
                Console.WriteLine();

                foreach (FileInfo excelFile in excelFiles)
                {

                    ExcelFileKonvertieren(excelFile, konfiguration);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unerwarteter Fehler");
                Console.WriteLine(ex.ToString());
                Console.WriteLine("Zum Schliessen eine beliebige Taste drücken...");
                Console.ReadKey();
            }
        }

        private static void ExcelFileKonvertieren(FileInfo excelFile, ConverterKonfiguration konfiguration)
        {
            try
            {
                Console.WriteLine("Excel File: " + excelFile);
                Console.WriteLine("Excel Importieren");
                Anwesenheitskontrolle anwesenheitskontrolle = GetAnwesenheiten(excelFile, konfiguration.Import);
                Console.WriteLine($"Anwesenheiten ({anwesenheitskontrolle.Anwesenheiten.Count}):");
                string anwesenheitenString = string.Join(Environment.NewLine, anwesenheitskontrolle.Anwesenheiten);
                Console.WriteLine(anwesenheitenString);
                Console.WriteLine();

                Console.WriteLine("CSV Exportieren");
                int exportiert = AnwesenheitenExportieren(anwesenheitskontrolle.Anwesenheiten, excelFile, konfiguration.Export);
                Console.WriteLine($"{exportiert} Anwesenheiten exportiert");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unerwarteter Fehler");
                Console.WriteLine(ex.ToString());
            }
        }

        private static List<FileInfo> GetExcelFiles(ImportKonfiguration konfiguration)
        {
            FileInfo excelFile = new(konfiguration.AwkPfad);
            if (excelFile.Exists)
            {
                return new() { excelFile };
            }

            DirectoryInfo ordner = new(konfiguration.AwkPfad);
            if (ordner.Exists)
            {
                FileInfo[] excelFiles = ordner.GetFiles("*.xlsx", SearchOption.TopDirectoryOnly);
                return new(excelFiles);
            }

            return new();
        }

        private static ConverterKonfiguration GetKonfiguration()
        {
            FileInfo konfigurationFile = GetKonfigurationFile();
            JsonKonfigurationRepository konfigurationRepository = new(konfigurationFile);

            bool konfigurationExistiert = konfigurationRepository.Existiert();
            if (konfigurationExistiert)
            {
                ConverterKonfiguration? konfiguration = konfigurationRepository.Laden();
                if (konfiguration != null)
                {
                    return konfiguration;
                }
            }

            ConverterKonfiguration standartKonfiguration = konfigurationRepository.StandardLaden();
            konfigurationRepository.Speicheren(standartKonfiguration);
            return standartKonfiguration;
        }

        private static FileInfo GetKonfigurationFile()
        {
            string ordner = Directory.GetCurrentDirectory();
            string file = nameof(ConverterKonfiguration) + ".json";
            string filePath = Path.Combine(ordner, file);
            FileInfo konfigurationFile = new(filePath);
            return konfigurationFile;
        }

        private static Anwesenheitskontrolle GetAnwesenheiten(FileInfo excelFile, ImportKonfiguration konfiguration)
        {
            ExcelImport excelImportRepository = new(konfiguration);
            Anwesenheitskontrolle anwesenheitskontrolle = excelImportRepository.Laden(excelFile);
            return anwesenheitskontrolle;
        }

        private static int AnwesenheitenExportieren(IEnumerable<Anwesenheit> anwesenheiten, FileInfo excelFile, ExportKonfiguration konfiguration)
        {
            CsvExport csvExportRepository = new(konfiguration);
            int exportiert = csvExportRepository.Export(anwesenheiten, excelFile);
            return exportiert;
        }
    }
}