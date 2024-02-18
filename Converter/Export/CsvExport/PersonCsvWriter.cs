using BCT.AWK.Converter.Anwesenheitskontrollen;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace BCT.AWK.Converter.Export.CsvExport
{
    internal class PersonCsvWriter : ICsvExportWriter
    {
        private string _separator;

        public PersonCsvWriter(string separator)
        {
            SetSeparator(separator);
        }

        public string Bezeichnung => "Personen";

        [MemberNotNull(nameof(_separator))]
        public void SetSeparator(string separator)
        {
            _separator = separator;
        }

        public bool CanWrite(ExportKonfiguration konfiguration)
        {
            return konfiguration.PersonenExport;
        }

        public void WriteKopfZeile(StreamWriter writer)
        {
            writer.Write("Personennummer");
            writer.Write(_separator);
            writer.Write("Name");
            writer.Write(_separator);
            writer.Write("Vorname");
            writer.Write(_separator);
            writer.Write("Geburtsdatum");
            writer.Write(_separator);
            writer.Write("Geschlecht");
            writer.Write(_separator);
            writer.Write("AHV_NR");
            writer.Write(_separator);
            writer.Write("PEID");
            writer.Write(_separator);
            writer.Write("Nationalitaet");
            writer.Write(_separator);
            writer.Write("Muttersprache");
            writer.Write(_separator);
            writer.Write("Strasse");
            writer.Write(_separator);
            writer.Write("Hausnummer");
            writer.Write(_separator);
            writer.Write("PLZ");
            writer.Write(_separator);
            writer.Write("Ort");
            writer.Write(_separator);
            writer.Write("Land");

            writer.WriteLine();
        }

        public int WriteZeilen(Anwesenheitskontrolle anwesenheitskontrolle, StreamWriter writer)
        {
            int exportiert = 0;
            foreach (Person person in anwesenheitskontrolle.Personen)
            {
                WriteZeile(person, writer);
                exportiert++;
            }

            return exportiert;
        }

        private void WriteZeile(Person person, StreamWriter writer)
        {
            writer.Write(person.Nummer);
            writer.Write(_separator);
            writer.Write(person.NachName);
            writer.Write(_separator);
            writer.Write(person.VorName);
            writer.Write(_separator);
            writer.Write(person.Geburtstag?.ToString("dd.MM.yyyy"));
            writer.Write(_separator);
            writer.Write(ToString(person.Geschlecht));
            writer.Write(_separator);
            writer.Write(person.AhvNr);
            writer.Write(_separator);
            writer.Write(person.PeId);
            writer.Write(_separator);
            writer.Write(ToString(person.Nationalitaet));
            writer.Write(_separator);
            writer.Write(ToString(person.Muttersprache));
            writer.Write(_separator);
            writer.Write(person.Strasse);
            writer.Write(_separator);
            writer.Write(person.Hausnummer);
            writer.Write(_separator);
            writer.Write(person.Plz);
            writer.Write(_separator);
            writer.Write(person.Ort);
            writer.Write(_separator);
            writer.Write(person.Land);

            writer.WriteLine();
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
