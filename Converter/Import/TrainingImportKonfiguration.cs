using System;

namespace BCT.AWK.Converter.Import
{
    internal class TrainingImportKonfiguration
    {
        public int ErsteTrainingSpalte { get; set; } = 8;
        public int LetzteTrainingSpalte { get; set; } = 17;
        public int ArtZeile { get; set; } = 2;
        public int DatumZeile { get; set; } = 3;
        public int ZeitZeile { get; set; } = 4;
        public int DauerZeile { get; set; } = 5;
        public int OrtZeile { get; set; } = 6;

        public override string ToString()
        {
            string ersteTrainingSpalte = $"{nameof(ErsteTrainingSpalte)}= {ErsteTrainingSpalte}";
            string letzteTrainingSpalte = $"{nameof(LetzteTrainingSpalte)}= {LetzteTrainingSpalte}";
            string artZeile = $"{nameof(ArtZeile)}= {ArtZeile}";
            string datumZeile = $"{nameof(DatumZeile)}= {DatumZeile}";
            string zeitZeile = $"{nameof(ZeitZeile)}= {ZeitZeile}";
            string dauerZeile = $"{nameof(DauerZeile)}= {DauerZeile}";
            string ortZeile = $"{nameof(OrtZeile)}= {OrtZeile}";

            string separator = $"{Environment.NewLine}\t\t";
            string s = string.Join(separator, "Training Konfiguration:", ersteTrainingSpalte, letzteTrainingSpalte, artZeile, datumZeile, zeitZeile, dauerZeile, ortZeile);
            return s;
        }
    }
}
