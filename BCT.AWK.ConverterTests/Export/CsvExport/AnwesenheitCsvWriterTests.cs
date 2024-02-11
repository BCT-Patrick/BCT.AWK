using BCT.AWK.Converter.Anwesenheitskontrollen;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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

            _anwesenheitCsvWriter = new(_streamWriter, ";");
        }

        [TestMethod()]
        public void WriteKopfZeileTest()
        {
            _anwesenheitCsvWriter.WriteKopfZeile();

            _streamWriter.Flush();
            _memoryStream.Position = 0;
            string erwarteteZeile = "Personennummer;Funktion;Datum;Aktivitaetstyp;Zeit;Dauer;Ort" + Environment.NewLine;
            string zeile = _streamReader.ReadToEnd();

            zeile.Should().Be(erwarteteZeile);
        }

        [TestMethod()]
        public void WriteZeileTest()
        {
            Person person = new("nummer", "vorname", "nachname", new(2020, 12, 31), Person.GeschlechtTyp.Maennlich, "ahvNr", "peId", Person.NationalitaetTyp.CH, Person.SpracheTyp.Deutsch, "strasse", "hausnummer", "plz", "ort", "land");
            Training training = new(Training.TrainingsTyp.Training, new(2021,6,15), new(19,30), 1.5, "Ort");
            Anwesenheit anwesenheit = new(Anwesenheit.FunktionsTyp.Teilnehmer, person, training, true);

            _anwesenheitCsvWriter.WriteZeile(anwesenheit);

            _streamWriter.Flush();
            _memoryStream.Position = 0;
            string erwarteteZeile = "nummer;Teilnehmer/in;15.06.2021;Training;19:30;1.5;Ort" + Environment.NewLine;
            string zeile = _streamReader.ReadToEnd();

            zeile.Should().Be(erwarteteZeile);
        }
    }
}