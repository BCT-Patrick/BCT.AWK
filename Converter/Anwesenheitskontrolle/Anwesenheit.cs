namespace BCT.AWK.Converter.Anwesenheitskontrolle
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
        public bool Anwesend { get; set; }

        public override string ToString()
        {
            return $"{Teilnehmer} {Training} {Anwesend}";
        }
    }
}
