using BCT.AWK.Converter.Anwesenheitskontrollen;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace BCT.AWK.Converter.Export.CsvExport
{
    [TestClass()]
    public class PersonCsvWriterTests
    {
        private readonly PersonCsvWriter _personCsvWriter;

        private readonly MemoryStream _memoryStream;
        private readonly StreamWriter _streamWriter;
        private readonly StreamReader _streamReader;

        public PersonCsvWriterTests()
        {
            _memoryStream = new();
            _streamWriter = new(_memoryStream);
            _streamReader = new(_memoryStream);

            _personCsvWriter = new(_streamWriter, ";");
        }

        [TestMethod()]
        public void WriteKopfZeileTest()
        {
            _personCsvWriter.WriteKopfZeile();

            _streamWriter.Flush();
            _memoryStream.Position = 0;
            string erwarteteZeile = "Personennummer;Name;Vorname;Geburtsdatum;Geschlecht;AHV_NR;PEID;Nationalitaet;Muttersprache;Strasse;Hausnummer;PLZ;Ort;Land" + Environment.NewLine;
            string zeile = _streamReader.ReadToEnd();

            zeile.Should().Be(erwarteteZeile);
        }

        [TestMethod()]
        public void WriteZeileTest()
        {
            Person person = new("nummer", "vorname", "nachname", new(2020, 12, 31), Person.GeschlechtTyp.Maennlich, "ahvNr", "peId", Person.NationalitaetTyp.CH, Person.SpracheTyp.Deutsch, "strasse", "hausnummer", "plz", "ort", "land");

            _personCsvWriter.WriteZeile(person);

            _streamWriter.Flush();
            _memoryStream.Position = 0;
            string erwarteteZeile = "nummer;nachname;vorname;31.12.2020;männlich;ahvNr;peId;CH;DE;strasse;hausnummer;plz;ort;land" + Environment.NewLine;
            string zeile = _streamReader.ReadToEnd();

            zeile.Should().Be(erwarteteZeile);
        }
    }
}