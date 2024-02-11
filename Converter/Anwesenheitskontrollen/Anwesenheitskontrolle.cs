using System.Collections.Generic;

namespace BCT.AWK.Converter.Anwesenheitskontrollen
{
    internal class Anwesenheitskontrolle
    {
        public Anwesenheitskontrolle(IReadOnlyList<Person> personen, IReadOnlyList<Training> trainings, IReadOnlyList<Anwesenheit> anwesenheiten)
        {
            Personen = personen;
            Trainings = trainings;
            Anwesenheiten = anwesenheiten;
        }

        public IReadOnlyList<Person> Personen { get; }
        public IReadOnlyList<Training> Trainings { get; }
        public IReadOnlyList<Anwesenheit> Anwesenheiten { get; }
    }
}
