using BCT.AWK.Converter.Anwesenheitskontrolle;
using BCT.AWK.Converter.Export;
using BCT.AWK.Converter.Import;
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
            Console.WriteLine("Konfiguration Laden");
            ConverterKonfiguration konfiguration = GetKonfiguration();
            Console.WriteLine(konfiguration);
            Console.WriteLine();

            Console.WriteLine("Excel Importieren");
            List<Anwesenheit> anwesenheiten = GetAnwesenheiten(konfiguration.Import);
            Console.WriteLine($"Anwesenheiten ({anwesenheiten.Count}):");
            string anwesenheitenString = String.Join(Environment.NewLine, anwesenheiten);
            Console.WriteLine(anwesenheitenString);
            Console.WriteLine();

            Console.WriteLine("CSV Exportieren");
            int exportiert = AnwesenheitenExportieren(anwesenheiten, konfiguration.Export);
            Console.WriteLine($"{exportiert} Anwesenheiten exportiert");
        }

        private static int AnwesenheitenExportieren(IEnumerable<Anwesenheit> anwesenheiten, ExportKonfiguration konfiguration)
        {
            CsvExportRepository csvExportRepository = new(konfiguration);
            int exportiert = csvExportRepository.Export(anwesenheiten);
            return exportiert;
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

        private static List<Anwesenheit> GetAnwesenheiten(ImportKonfiguration konfiguration)
        {
            ExcelImportRepository konfigurationRepository = new(konfiguration);
            List<Anwesenheit> anwesenheiten = konfigurationRepository.Laden();
            return anwesenheiten;
        }
    }
}