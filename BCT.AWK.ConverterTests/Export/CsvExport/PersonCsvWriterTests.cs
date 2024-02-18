using BCT.AWK.Converter.Anwesenheitskontrollen;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
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

            _personCsvWriter = new(";");
        }

        [TestMethod]
        public void Bezeichnung()
        {
            _personCsvWriter.Bezeichnung.Should().Be("Personen");
        }

        [TestMethod]
        public void CanWrite_WennPersonenExportGesetztIst()
        {
            ExportKonfiguration exportKonfiguration = new() { PersonenExport = true };

            bool can = _personCsvWriter.CanWrite(exportKonfiguration);

            can.Should().BeTrue();
        }

        [TestMethod]
        public void CanWrite_WennPersonenExportNichtGesetztIst()
        {
            ExportKonfiguration exportKonfiguration = new() { PersonenExport = false };

            bool can = _personCsvWriter.CanWrite(exportKonfiguration);

            can.Should().BeFalse();
        }

        [TestMethod()]
        public void WriteKopfZeile()
        {
            _personCsvWriter.WriteKopfZeile(_streamWriter);

            _streamWriter.Flush();
            _memoryStream.Position = 0;
            string erwarteteZeile = "Personennummer;Name;Vorname;Geburtsdatum;Geschlecht;AHV_NR;PEID;Nationalitaet;Muttersprache;Strasse;Hausnummer;PLZ;Ort;Land" + Environment.NewLine;
            string zeile = _streamReader.ReadToEnd();

            zeile.Should().Be(erwarteteZeile);
        }

        [TestMethod()]
        public void WriteKopfZeile_SetSeparator()
        {
            _personCsvWriter.SetSeparator("+");
            _personCsvWriter.WriteKopfZeile(_streamWriter);

            _streamWriter.Flush();
            _memoryStream.Position = 0;
            string erwarteteZeile = "Personennummer+Name+Vorname+Geburtsdatum+Geschlecht+AHV_NR+PEID+Nationalitaet+Muttersprache+Strasse+Hausnummer+PLZ+Ort+Land" + Environment.NewLine;
            string zeile = _streamReader.ReadToEnd();

            zeile.Should().Be(erwarteteZeile);
        }

        [TestMethod()]
        public void WriteZeile()
        {
            Person person = new("nummer", "vorname", "nachname", new(2020, 12, 31), Person.GeschlechtTyp.Maennlich, "ahvNr", "peId", Person.NationalitaetTyp.CH, Person.SpracheTyp.Deutsch, "strasse", "hausnummer", "plz", "ort", "land");
            List<Person> personen = new() { person };
            Anwesenheitskontrolle anwesenheitskontrolle = new(personen, new List<Aktivitaet>(), new List<Anwesenheit>());

            int resultat = _personCsvWriter.WriteZeilen(anwesenheitskontrolle, _streamWriter);

            _streamWriter.Flush();
            _memoryStream.Position = 0;
            string erwarteteZeile = "nummer;nachname;vorname;31.12.2020;männlich;ahvNr;peId;CH;DE;strasse;hausnummer;plz;ort;land" + Environment.NewLine;
            string zeile = _streamReader.ReadToEnd();

            resultat.Should().Be(1);
            zeile.Should().Be(erwarteteZeile);
        }

        [TestMethod()]
        public void WriteZeile_SetSeparator()
        {
            Person person = new("nummer", "vorname", "nachname", new(2020, 12, 31), Person.GeschlechtTyp.Maennlich, "ahvNr", "peId", Person.NationalitaetTyp.CH, Person.SpracheTyp.Deutsch, "strasse", "hausnummer", "plz", "ort", "land");
            List<Person> personen = new() { person };
            Anwesenheitskontrolle anwesenheitskontrolle = new(personen, new List<Aktivitaet>(), new List<Anwesenheit>());

            _personCsvWriter.SetSeparator("+");
            int resultat = _personCsvWriter.WriteZeilen(anwesenheitskontrolle, _streamWriter);

            _streamWriter.Flush();
            _memoryStream.Position = 0;
            string erwarteteZeile = "nummer+nachname+vorname+31.12.2020+männlich+ahvNr+peId+CH+DE+strasse+hausnummer+plz+ort+land" + Environment.NewLine;
            string zeile = _streamReader.ReadToEnd();

            resultat.Should().Be(1);
            zeile.Should().Be(erwarteteZeile);
        }
    }
}