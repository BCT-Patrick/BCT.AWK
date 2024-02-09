using System;

namespace BCT.AWK.Converter.Anwesenheitskontrollen
{
    internal class Teilnehmer
    {
        public Teilnehmer(string nummer, FunktionsTyp funktion, string? vorName, string? nachName, DateTime? geburtstag)
        {
            Nummer = nummer;
            Funktion = funktion;
            VorName = vorName;
            NachName = nachName;
            Geburtstag = geburtstag;
        }

        public string Nummer { get; }
        public FunktionsTyp Funktion { get; }
        public string? VorName { get; }
        public string? NachName { get; }
        public DateTime? Geburtstag { get; }

        public enum FunktionsTyp
        {
            Teilnehmer,
            Leiter
        }

        public override string ToString()
        {
            return $"{Nummer} {Funktion} {VorName} {NachName} {Geburtstag}";
        }
    }
}
