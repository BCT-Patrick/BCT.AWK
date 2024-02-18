using BCT.AWK.Converter.Anwesenheitskontrollen;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace BCT.AWK.Converter.Export.CsvExport
{
    [TestClass()]
    public class AnwesenheitCsvWriterTests
    {
        private readonly AnwesenheitCsvWriter _anwesenheitCsvWriter;

        private readonly MemoryStream _memoryStream;
        private readonly StreamWriter _streamWriter;
        private readonly StreamReader _streamReader;

        public AnwesenheitCsvWriterTests()
        {
            _memoryStream = new();
            _streamWriter = new(_memoryStream);
            _streamReader = new(_memoryStream);

            _anwesenheitCsvWriter = new(";");
        }

        [TestMethod]
        public void Bezeichnung()
        {
            _anwesenheitCsvWriter.Bezeichnung.Should().Be("Anwesenheiten");
        }

        [TestMethod]
        public void CanWrite_WennAnwesenheitenExportGesetztIst()
        {
            ExportKonfiguration exportKonfiguration = new() { AnwesenheitenExport = true };

            bool can = _anwesenheitCsvWriter.CanWrite(exportKonfiguration);

            can.Should().BeTrue();
        }

        [TestMethod]
        public void CanWrite_WennAnwesenheitenExportNichtGesetztIst()
        {
            ExportKonfiguration exportKonfiguration = new() { AnwesenheitenExport = false };

            bool can = _anwesenheitCsvWriter.CanWrite(exportKonfiguration);

            can.Should().BeFalse();
        }

        [TestMethod()]
        public void WriteKopfZeile()
        {
            _anwesenheitCsvWriter.WriteKopfZeile(_streamWriter);

            _streamWriter.Flush();
            _memoryStream.Position = 0;
            string erwarteteZeile = "Personennummer;Funktion;Datum;Aktivitaetstyp;Zeit;Dauer;Ort" + Environment.NewLine;
            string zeile = _streamReader.ReadToEnd();

            zeile.Should().Be(erwarteteZeile);
        }

        [TestMethod()]
        public void WriteKopfZeile_SetSeparator()
        {
            _anwesenheitCsvWriter.SetSeparator("+");
            _anwesenheitCsvWriter.WriteKopfZeile(_streamWriter);

            _streamWriter.Flush();
            _memoryStream.Position = 0;
            string erwarteteZeile = "Personennummer+Funktion+Datum+Aktivitaetstyp+Zeit+Dauer+Ort" + Environment.NewLine;
            string zeile = _streamReader.ReadToEnd();

            zeile.Should().Be(erwarteteZeile);
        }

        [TestMethod()]
        public void WriteZeile()
        {
            Person person = new("nummer", "vorname", "nachname", new(2020, 12, 31), Person.GeschlechtTyp.Maennlich, "ahvNr", "peId", Person.NationalitaetTyp.CH, Person.SpracheTyp.Deutsch, "strasse", "hausnummer", "plz", "ort", "land");
            Aktivitaet aktivitaet = new(Aktivitaet.AktivitaetsTyp.Training, new(2021,6,15), new(19,30), 1.5, "Ort", "Fokus");
            Anwesenheit anwesenheit1 = new(Anwesenheit.FunktionsTyp.Teilnehmer, person, aktivitaet, true);
            Anwesenheit anwesenheit2 = new(Anwesenheit.FunktionsTyp.Teilnehmer, person, aktivitaet, false);
            List<Anwesenheit> anwesenheiten = new() { anwesenheit1, anwesenheit2 };
            Anwesenheitskontrolle anwesenheitskontrolle = new(new List<Person>(), new List<Aktivitaet>() , anwesenheiten);

            int resultat = _anwesenheitCsvWriter.WriteZeilen(anwesenheitskontrolle, _streamWriter);

            _streamWriter.Flush();
            _memoryStream.Position = 0;
            string erwarteteZeile = "nummer;Teilnehmer/in;15.06.2021;Training;19:30;1.5;Ort" + Environment.NewLine;
            string zeile = _streamReader.ReadToEnd();

            resultat.Should().Be(1);
            zeile.Should().Be(erwarteteZeile);
        }

        [TestMethod()]
        public void WriteZeile_SetSeparator()
        {
            Person person = new("nummer", "vorname", "nachname", new(2020, 12, 31), Person.GeschlechtTyp.Maennlich, "ahvNr", "peId", Person.NationalitaetTyp.CH, Person.SpracheTyp.Deutsch, "strasse", "hausnummer", "plz", "ort", "land");
            Aktivitaet aktivitaet = new(Aktivitaet.AktivitaetsTyp.Training, new(2021, 6, 15), new(19, 30), 1.5, "Ort", "Fokus");
            Anwesenheit anwesenheit1 = new(Anwesenheit.FunktionsTyp.Teilnehmer, person, aktivitaet, true);
            Anwesenheit anwesenheit2 = new(Anwesenheit.FunktionsTyp.Teilnehmer, person, aktivitaet, false);
            List<Anwesenheit> anwesenheiten = new() { anwesenheit1, anwesenheit2 };
            Anwesenheitskontrolle anwesenheitskontrolle = new(new List<Person>(), new List<Aktivitaet>(), anwesenheiten);

            _anwesenheitCsvWriter.SetSeparator("+");
            int resultat = _anwesenheitCsvWriter.WriteZeilen(anwesenheitskontrolle, _streamWriter);

            _streamWriter.Flush();
            _memoryStream.Position = 0;
            string erwarteteZeile = "nummer+Teilnehmer/in+15.06.2021+Training+19:30+1.5+Ort" + Environment.NewLine;
            string zeile = _streamReader.ReadToEnd();

            resultat.Should().Be(1);
            zeile.Should().Be(erwarteteZeile);
        }
    }
}