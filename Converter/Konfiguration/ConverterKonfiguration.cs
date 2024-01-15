using System;

namespace BCT.AWK.Converter.Konfiguration
{
    internal class ConverterKonfiguration
    {
        public ConverterKonfiguration()
        {
            Training = new();
            Teilnemer = new();
        }

        public string AwkFile { get; set; } = "Anwesenheitskontrolle.xlsx";
        public string AwkBaltt { get; set; } = "AWK";

        public TrainingKonfiguration Training { get; set; }
        public TeilnehmerKonfiguration Teilnemer { get; set; }

        public override string ToString()
        {
            string s = string.Join(Environment.NewLine, "Converter Konfiguration", AwkFile, AwkBaltt, Training, Teilnemer);
            return s;
        }
    }
}
