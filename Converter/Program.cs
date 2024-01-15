using BCT.AWK.Converter.Anwesenheitskontrolle;
using BCT.AWK.Converter.Export;
using BCT.AWK.Converter.Import;
using BCT.AWK.Converter.Konfiguration;
using System;
using System.Collections.Generic;
using System.IO;

namespace BCT.AWK.Converter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Konfiguration");
            ConverterKonfiguration konfiguration = GetKonfiguration();
            Console.WriteLine(konfiguration);
            Console.WriteLine();

            Console.WriteLine("Anwesenheiten");
            List<Anwesenheit> anwesenheiten = GetAnwesenheiten(konfiguration);
            string anwesenheitenString = String.Join(Environment.NewLine, anwesenheiten);
            Console.WriteLine(anwesenheitenString);
            Console.WriteLine();

            AnwesenheitenExportieren(anwesenheiten);
        }

        private static void AnwesenheitenExportieren(IEnumerable<Anwesenheit> anwesenheiten)
        {
            CsvExportRepository csvExportRepository = new CsvExportRepository();
            csvExportRepository.Export(anwesenheiten);
        }

        private static ConverterKonfiguration GetKonfiguration()
        {
            FileInfo konfigurationFile = GetKonfigurationFile();
            JsonKonfigurationRepository konfigurationRepository = new(konfigurationFile);

            bool konfigurationExistiert = konfigurationRepository.Existiert();
            if (konfigurationExistiert)
            {
                ConverterKonfiguration konfiguration = konfigurationRepository.Laden();
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
            FileInfo konfigurationFile = new FileInfo(filePath);
            return konfigurationFile;
        }

        private static List<Anwesenheit> GetAnwesenheiten(ConverterKonfiguration konfiguration)
        {
            ExcelImportRepository konfigurationRepository = new(konfiguration);
            List<Anwesenheit> anwesenheiten = konfigurationRepository.Laden();
            return anwesenheiten;
        }
    }
}