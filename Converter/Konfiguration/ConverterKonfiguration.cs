using System;
using BCT.AWK.Converter.Export;
using BCT.AWK.Converter.Import;

namespace BCT.AWK.Converter.Konfiguration
{
    internal class ConverterKonfiguration
    {
        public ConverterKonfiguration()
        {
            Import = new();
            Export = new();
        }

        public ImportKonfiguration Import { get; set; }
        public ExportKonfiguration Export { get; set; }

        public override string ToString()
        {
            string s = string.Join(Environment.NewLine, "Converter Konfiguration", Import, Export);
            return s;
        }
    }
}
