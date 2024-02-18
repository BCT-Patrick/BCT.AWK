using System;

namespace BCT.AWK.Converter.Import
{
    internal class ImportKonfigurationAktivitaet
    {
        public int ErsteAktivitaetSpalte { get; set; } = 16;
        public int LetzteAktivitaetSpalte { get; set; } = 50;

        public int ArtZeile { get; set; } = 2;
        public int DatumZeile { get; set; } = 3;
        public int ZeitZeile { get; set; } = 4;
        public int DauerZeile { get; set; } = 5;
        public int OrtZeile { get; set; } = 6;
        public int FokusZeile { get; set; } = 7;

        public override string ToString()
        {
            string ersteAktivitaetSpalte = $"{nameof(ErsteAktivitaetSpalte)}= {ErsteAktivitaetSpalte}";
            string letzteAktivitaetSpalte = $"{nameof(LetzteAktivitaetSpalte)}= {LetzteAktivitaetSpalte}";
            string artZeile = $"{nameof(ArtZeile)}= {ArtZeile}";
            string datumZeile = $"{nameof(DatumZeile)}= {DatumZeile}";
            string zeitZeile = $"{nameof(ZeitZeile)}= {ZeitZeile}";
            string dauerZeile = $"{nameof(DauerZeile)}= {DauerZeile}";
            string ortZeile = $"{nameof(OrtZeile)}= {OrtZeile}";
            string fokusZeile = $"{nameof(FokusZeile)}= {FokusZeile}";

            string separator = $"{Environment.NewLine}\t\t";
            string s = string.Join(separator, "Aktivitaet Konfiguration:",
                ersteAktivitaetSpalte, 
                letzteAktivitaetSpalte, 
                artZeile,
                datumZeile, 
                zeitZeile, 
                dauerZeile,
                ortZeile,
                fokusZeile);
            return s;
        }
    }
}
