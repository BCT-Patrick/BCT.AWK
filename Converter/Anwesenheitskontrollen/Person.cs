using System;

namespace BCT.AWK.Converter.Anwesenheitskontrollen
{
    internal class Person
    {
        public Person(
            string? nummer, 
            string? vorName, 
            string? nachName,
            DateTime? geburtstag, 
            GeschlechtTyp? geschlecht, 
            SpracheTyp? muttersprache, 
            string? ahvNr,
            string? peId, 
            string? strasse,
            string? hausnummer,
            string? plz, 
            string? ort, 
            string? land)
        {
            Nummer = nummer;
            VorName = vorName;
            NachName = nachName;
            Geburtstag = geburtstag;
            Geschlecht = geschlecht;
            Muttersprache = muttersprache;
            AhvNr = ahvNr;
            PeId = peId;
            Strasse = strasse;
            Hausnummer = hausnummer;
            Plz = plz;
            Ort = ort;
            Land = land;
        }

        public string? Nummer { get; }
        public string? VorName { get; }
        public string? NachName { get; }
        public DateTime? Geburtstag { get; }
        public GeschlechtTyp? Geschlecht { get; }
        public SpracheTyp? Muttersprache { get; }
        public string? AhvNr { get; }
        public string? PeId { get; }
        public string? Strasse { get; }
        public string? Hausnummer { get; }
        public string? Plz { get; }
        public string? Ort { get; }
        public string? Land { get; }

        public enum GeschlechtTyp
        {
            Maennlich,
            Weiblich
        }

        public enum SpracheTyp
        {
            Deutsch,
            Franzoesisch,
            Italienisch,
            Andere
        }

        public override string ToString()
        {
            return $"{Nummer} {VorName} {NachName} {Geburtstag} {Geschlecht}";
        }
    }
}
