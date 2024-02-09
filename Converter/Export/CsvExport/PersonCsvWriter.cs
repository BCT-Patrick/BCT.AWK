using BCT.AWK.Converter.Anwesenheitskontrollen;
using System.IO;

namespace BCT.AWK.Converter.Export.CsvExport
{
    internal class PersonCsvWriter
    {
        private readonly StreamWriter _writer;
        private readonly string _separator;

        public PersonCsvWriter(StreamWriter writer, string separator)
        {
            _writer = writer;
            _separator = separator;
        }

        public void WriteKopfZeile()
        {
            _writer.Write("Personennummer");
            _writer.Write(_separator);
            _writer.Write("Name");
            _writer.Write(_separator);
            _writer.Write("Vorname");
            _writer.Write(_separator);
            _writer.Write("Geburtsdatum");
            _writer.Write(_separator);
            _writer.Write("Geschlecht");
            _writer.Write(_separator);
            _writer.Write("AHV_NR");
            _writer.Write(_separator);
            _writer.Write("PEID");
            _writer.Write(_separator);
            _writer.Write("Nationalitaet");
            _writer.Write(_separator);
            _writer.Write("Muttersprache");
            _writer.Write(_separator);
            _writer.Write("Strasse");
            _writer.Write(_separator);
            _writer.Write("Hausnummer");
            _writer.Write(_separator);
            _writer.Write("PLZ");
            _writer.Write(_separator);
            _writer.Write("Ort");
            _writer.Write(_separator);
            _writer.Write("Land");

            _writer.WriteLine();
        }

        public void WriteZeile(Person person)
        {
            _writer.Write(person.Nummer);
            _writer.Write(_separator);
            _writer.Write(person.NachName);
            _writer.Write(_separator);
            _writer.Write(person.VorName);
            _writer.Write(_separator);
            _writer.Write(person.Geburtstag?.ToString("dd.MM.yyyy"));
            _writer.Write(_separator);
            _writer.Write(ToString(person.Geschlecht));
            _writer.Write(_separator);
            _writer.Write(person.AhvNr);
            _writer.Write(_separator);
            _writer.Write(person.PeId);
            _writer.Write(_separator);
            _writer.Write(ToString(person.Nationalitaet));
            _writer.Write(_separator);
            _writer.Write(ToString(person.Muttersprache));
            _writer.Write(_separator);
            _writer.Write(person.Strasse);
            _writer.Write(_separator);
            _writer.Write(person.Hausnummer);
            _writer.Write(_separator);
            _writer.Write(person.Plz);
            _writer.Write(_separator);
            _writer.Write(person.Ort);
            _writer.Write(_separator);
            _writer.Write(person.Land);

            _writer.WriteLine();
        }

        private static string? ToString(Person.GeschlechtTyp? geschlecht)
        {
            return geschlecht switch
            {
                Person.GeschlechtTyp.Maennlich => "männlich",
                Person.GeschlechtTyp.Weiblich => "weiblich",
                _ => null,
            };
        }

        private static string? ToString(Person.NationalitaetTyp? nationalitaet)
        {
            return nationalitaet switch
            {
                Person.NationalitaetTyp.CH => "CH",
                Person.NationalitaetTyp.FL => "FL",
                _ => "Andere",
            };
        }

        private static string? ToString(Person.SpracheTyp? sprache)
        {
            return sprache switch
            {
                Person.SpracheTyp.Deutsch => "DE",
                Person.SpracheTyp.Franzoesisch => "FR",
                Person.SpracheTyp.Italienisch => "IT",
                _ => "Andere",
            };
        }
    }
}
