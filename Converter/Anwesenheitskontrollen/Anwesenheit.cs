namespace BCT.AWK.Converter.Anwesenheitskontrollen
{
    internal class Anwesenheit
    {
        public Anwesenheit(FunktionsTyp? funktion, Person teilnehmer, Training training, bool anwesend)
        {
            Funktion = funktion;
            Teilnehmer = teilnehmer;
            Training = training;
            Anwesend = anwesend;
        }

        public Person Teilnehmer { get; }
        public Training Training { get; }
        public FunktionsTyp? Funktion { get; }
        public bool Anwesend { get; }

        public enum FunktionsTyp
        {
            Teilnehmer,
            Leiter
        }

        public override string ToString()
        {
            return $"{Funktion} {Teilnehmer} {Training} {Anwesend}";
        }
    }
}
