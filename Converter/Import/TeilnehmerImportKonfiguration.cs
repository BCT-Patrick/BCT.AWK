using System;

namespace BCT.AWK.Converter.Import
{
    internal class TeilnehmerImportKonfiguration
    {
        public int ErsteTeilnehmerZeile { get; set; } = 9;
        public int LetzteTeilnehmerZeile { get; set; } = 28;
        public int NummerSpalte { get; set; } = 1;
        public int FunktionSpalte { get; set; } = 2;
        public int VorNameSpalte { get; set; } = 3;
        public int NachNameSpalte { get; set; } = 4;
        public int GeburtstagSpalte { get; set; } = 5;
        public override string ToString()
        {
            string ersteTeilnehmerZeile = $"{nameof(ErsteTeilnehmerZeile)}= {ErsteTeilnehmerZeile}";
            string letzteTeilnehmerZeile = $"{nameof(LetzteTeilnehmerZeile)}= {LetzteTeilnehmerZeile}";
            string nummerSpalte = $"{nameof(NummerSpalte)}= {NummerSpalte}";
            string funktionSpalte = $"{nameof(FunktionSpalte)}= {FunktionSpalte}";
            string vorNameSpalte = $"{nameof(VorNameSpalte)}= {VorNameSpalte}";
            string nchNameSpalte = $"{nameof(NachNameSpalte)}= {NachNameSpalte}";
            string geburtstag = $"{nameof(GeburtstagSpalte)}= {GeburtstagSpalte}";

            string separator = $"{Environment.NewLine}\t\t";
            string s = string.Join(separator, "Teilnehmer Konfiguration:", ersteTeilnehmerZeile, letzteTeilnehmerZeile, nummerSpalte, funktionSpalte, vorNameSpalte, nchNameSpalte, geburtstag);
            return s;
        }
    }
}
