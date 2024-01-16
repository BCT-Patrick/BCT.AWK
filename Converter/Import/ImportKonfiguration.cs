using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCT.AWK.Converter.Import
{
    internal class ImportKonfiguration
    {
        public ImportKonfiguration()
        {
            Training = new();
            Teilnemer = new();
        }

        public string AwkFile { get; set; } = "Anwesenheitskontrolle.xlsx";
        public string AwkBaltt { get; set; } = "AWK";

        public TrainingImportKonfiguration Training { get; set; }
        public TeilnehmerImportKonfiguration Teilnemer { get; set; }

        public override string ToString()
        {
            string separator = $"{Environment.NewLine}\t";
            string s = string.Join(separator, "Import Konfiguration", AwkFile, AwkBaltt, Training, Teilnemer);
            return s;
        }
    }
}
