namespace BCT.AWK.Converter.Anwesenheitskontrollen
{
    internal class Anwesenheit
    {
        public Anwesenheit(FunktionsTyp? funktion, Person person, Aktivitaet aktivitaet, bool anwesend)
        {
            Funktion = funktion;
            Person = person;
            Aktivitaet = aktivitaet;
            Anwesend = anwesend;
        }

        public Person Person { get; }
        public Aktivitaet Aktivitaet { get; }
        public FunktionsTyp? Funktion { get; }
        public bool Anwesend { get; }

        public enum FunktionsTyp
        {
            Teilnehmer,
            Leiter
        }

        public override string ToString()
        {
            return $"{Funktion} {Person} {Aktivitaet} {Anwesend}";
        }
    }
}
