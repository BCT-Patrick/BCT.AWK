using System;

namespace BCT.AWK.Converter.Import
{
    internal class ImportKonfigurationPerson
    {
        public int ErstePersonZeile { get; set; } = 9;
        public int LetztePersonZeile { get; set; } = 50;

        public int NummerSpalte { get; set; } = 1;
        public int VorNameSpalte { get; set; } = 2;
        public int NachNameSpalte { get; set; } = 3;
        public int GeburtstagSpalte { get; set; } = 4;
        public int GeschlechtSpalte { get; set; } = 5;
        public int AhvNrSpalte { get; set; } = 6;
        public int PeIdSpalte { get; set; } = 7;
        public int NationalitaetSpalte { get; set; } = 8;
        public int MutterspracheSpalte { get; set; } = 9;
        public int StrasseSpalte { get; set; } = 10;
        public int HausnummerSpalte { get; set; } = 11;
        public int PlzSpalte { get; set; } = 12;
        public int OrtSpalte { get; set; } = 13;
        public int LandSpalte { get; set; } = 14;

        public override string ToString()
        {
            string ersteTeilnehmerZeile = $"{nameof(ErstePersonZeile)}= {ErstePersonZeile}";
            string letzteTeilnehmerZeile = $"{nameof(LetztePersonZeile)}= {LetztePersonZeile}";
            string nummerSpalte = $"{nameof(NummerSpalte)}= {NummerSpalte}";
            string vorNameSpalte = $"{nameof(VorNameSpalte)}= {VorNameSpalte}";
            string nchNameSpalte = $"{nameof(NachNameSpalte)}= {NachNameSpalte}";
            string geburtstagSpalte = $"{nameof(GeburtstagSpalte)}= {GeburtstagSpalte}";
            string geschlechtSpalte = $"{nameof(GeschlechtSpalte)}= {GeschlechtSpalte}";
            string ahvNrSpalte = $"{nameof(AhvNrSpalte)}= {AhvNrSpalte}";
            string pPeIdSpalte = $"{nameof(PeIdSpalte)}= {PeIdSpalte}";
            string nationalitaetSpalte = $"{nameof(NationalitaetSpalte)}= {NationalitaetSpalte}";
            string mutterspracheSpalte = $"{nameof(MutterspracheSpalte)}= {MutterspracheSpalte}";
            string strasseSpalte = $"{nameof(StrasseSpalte)}= {StrasseSpalte}";
            string hausnummerSpalte = $"{nameof(HausnummerSpalte)}= {HausnummerSpalte}";
            string plzSpalte = $"{nameof(PlzSpalte)}= {PlzSpalte}";
            string ortSpalte = $"{nameof(OrtSpalte)}= {OrtSpalte}";
            string landSpalte = $"{nameof(LandSpalte)}= {LandSpalte}";

            string separator = $"{Environment.NewLine}\t\t";
            string s = string.Join(separator, "Teilnehmer Konfiguration:", 
                ersteTeilnehmerZeile, 
                letzteTeilnehmerZeile,
                nummerSpalte, 
                vorNameSpalte, 
                nchNameSpalte,
                geburtstagSpalte,
                geschlechtSpalte,
                ahvNrSpalte,
                pPeIdSpalte,
                nationalitaetSpalte,
                mutterspracheSpalte,
                strasseSpalte,
                hausnummerSpalte,
                plzSpalte,
                ortSpalte,
                landSpalte);
            return s;
        }
    }
}
