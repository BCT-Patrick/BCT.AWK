using System.Collections.Generic;

namespace BCT.AWK.Converter.Anwesenheitskontrollen
{
    internal class Anwesenheitskontrolle
    {
        public Anwesenheitskontrolle(IReadOnlyList<Person> teilnehmer, IReadOnlyList<Training> trainings, IReadOnlyList<Anwesenheit> anwesenheiten)
        {
            Teilnehmer = teilnehmer;
            Trainings = trainings;
            Anwesenheiten = anwesenheiten;
        }

        public IReadOnlyList<Person> Teilnehmer { get; }
        public IReadOnlyList<Training> Trainings { get; }
        public IReadOnlyList<Anwesenheit> Anwesenheiten { get; }
    }
}
