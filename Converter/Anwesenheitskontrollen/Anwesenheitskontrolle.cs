using System.Collections.Generic;

namespace BCT.AWK.Converter.Anwesenheitskontrollen
{
    internal class Anwesenheitskontrolle
    {
        public Anwesenheitskontrolle(IReadOnlyList<Person> personen, IReadOnlyList<Aktivitaet> aktivitaeten, IReadOnlyList<Anwesenheit> anwesenheiten)
        {
            Personen = personen;
            Aktivitaeten = aktivitaeten;
            Anwesenheiten = anwesenheiten;
        }

        public IReadOnlyList<Person> Personen { get; }
        public IReadOnlyList<Aktivitaet> Aktivitaeten { get; }
        public IReadOnlyList<Anwesenheit> Anwesenheiten { get; }
    }
}
