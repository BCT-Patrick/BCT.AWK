using System.Collections.Generic;

namespace BCT.AWK.Converter.Anwesenheitskontrollen
{
    internal class Anwesenheitskontrolle
    {
        public Anwesenheitskontrolle(IReadOnlyList<Teilnehmer> teilnehmer, IReadOnlyList<Training> trainings, IReadOnlyList<Anwesenheit> anwesenheiten)
        {
            Teilnehmer = teilnehmer;
            Trainings = trainings;
            Anwesenheiten = anwesenheiten;
        }

        public IReadOnlyList<Teilnehmer> Teilnehmer { get; }
        public IReadOnlyList<Training> Trainings { get; }
        public IReadOnlyList<Anwesenheit> Anwesenheiten { get; }
    }
}
