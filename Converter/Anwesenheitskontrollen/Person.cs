using System;

namespace BCT.AWK.Converter.Anwesenheitskontrollen
{
    internal class Person
    {
        public Person(
            string? nummer, 
            string? vorName, 
            string? nachName,
            DateOnly? geburtstag, 
            GeschlechtTyp? geschlecht,
            string? ahvNr,
            string? peId,
            NationalitaetTyp? nationalitaet,
            SpracheTyp? muttersprache, 
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
            AhvNr = ahvNr;
            PeId = peId;
            Nationalitaet = nationalitaet;
            Muttersprache = muttersprache;
            Strasse = strasse;
            Hausnummer = hausnummer;
            Plz = plz;
            Ort = ort;
            Land = land;
        }

        public string? Nummer { get; }
        public string? VorName { get; }
        public string? NachName { get; }
        public DateOnly? Geburtstag { get; }
        public GeschlechtTyp? Geschlecht { get; }
        public string? AhvNr { get; }
        public string? PeId { get; }
        public NationalitaetTyp? Nationalitaet { get; }
        public SpracheTyp? Muttersprache { get; }
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

        public enum NationalitaetTyp
        {
            CH,
            FL,
            Andere
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
