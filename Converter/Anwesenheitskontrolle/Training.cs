using System;

namespace BCT.AWK.Converter.Anwesenheitskontrolle
{
    internal class Training
    {
        public Training(TrainingsTyp art, DateTime datum, DateTime? zeit, double dauer, string? ort)
        {
            Art = art;
            Datum = datum;
            Zeit = zeit;
            Dauer = dauer;
            Ort = ort;
        }

        public TrainingsTyp Art { get; }
        public DateTime Datum { get; }
        public DateTime? Zeit { get; }
        public double Dauer { get; }
        public string? Ort { get; }

        public enum TrainingsTyp
        {
            Training,
            Wettkampf,
            Trainingstag,
            Lagertag
        }

        public override string ToString()
        {
            return $"{Art} {Datum} {Zeit} {Dauer} {Ort}";
        }
    }
}
