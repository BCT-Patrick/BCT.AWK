using System;

namespace BCT.AWK.Converter.Anwesenheitskontrollen
{
    internal class Aktivitaet
    {
        public Aktivitaet(AktivitaetsTyp? art, DateOnly? datum, TimeOnly? zeit, double? dauer, string? ort, string? fokus)
        {
            Art = art;
            Datum = datum;
            Zeit = zeit;
            Dauer = dauer;
            Ort = ort;
            Fokus = fokus;
        }

        public AktivitaetsTyp? Art { get; }
        public DateOnly? Datum { get; }
        public TimeOnly? Zeit { get; }
        public double? Dauer { get; }
        public string? Ort { get; }
        public string? Fokus { get; }

        public enum AktivitaetsTyp
        {
            Training,
            Wettkampf,
            Trainingstag,
            Lagertag
        }

        public override string ToString()
        {
            return $"{Art} {Datum} {Zeit} {Dauer} {Ort} {Fokus}";
        }
    }
}
