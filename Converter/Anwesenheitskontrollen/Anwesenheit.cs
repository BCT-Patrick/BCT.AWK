namespace BCT.AWK.Converter.Anwesenheitskontrollen
{
    internal class Anwesenheit
    {
        public Anwesenheit(Teilnehmer teilnehmer, Training training, bool anwesend)
        {
            Teilnehmer = teilnehmer;
            Training = training;
            Anwesend = anwesend;
        }

        public Teilnehmer Teilnehmer { get; }
        public Training Training { get; }
        public bool Anwesend { get; }

        public override string ToString()
        {
            return $"{Teilnehmer} {Training} {Anwesend}";
        }
    }
}
