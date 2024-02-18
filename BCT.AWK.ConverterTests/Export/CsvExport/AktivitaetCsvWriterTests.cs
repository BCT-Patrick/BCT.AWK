using BCT.AWK.Converter.Anwesenheitskontrollen;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace BCT.AWK.Converter.Export.CsvExport
{
    [TestClass]
    public class AktivitaetCsvWriterTests
    {
        private readonly AktivitaetCsvWriter _aktivitaetCsvWriter;

        private readonly MemoryStream _memoryStream;
        private readonly StreamWriter _streamWriter;
        private readonly StreamReader _streamReader;

        public AktivitaetCsvWriterTests()
        {
            _memoryStream = new();
            _streamWriter = new(_memoryStream);
            _streamReader = new(_memoryStream);

            _aktivitaetCsvWriter = new(";");
        }

        [TestMethod]
        public void Bezeichnung()
        {
            _aktivitaetCsvWriter.Bezeichnung.Should().Be("Aktivitaeten");
        }

        [TestMethod]
        public void CanWrite_WennAktivitaetenExportGesetztIst()
        {
            ExportKonfiguration exportKonfiguration = new() { AktivitaetenExport = true };

            bool can = _aktivitaetCsvWriter.CanWrite(exportKonfiguration);

            can.Should().BeTrue();
        }

        [TestMethod]
        public void CanWrite_WennAktivitaetenExportNichtGesetztIst()
        {
            ExportKonfiguration exportKonfiguration = new() { AktivitaetenExport = false };

            bool can = _aktivitaetCsvWriter.CanWrite(exportKonfiguration);

            can.Should().BeFalse();
        }

        [TestMethod()]
        public void WriteKopfZeile()
        {
            _aktivitaetCsvWriter.WriteKopfZeile(_streamWriter);

            _streamWriter.Flush();
            _memoryStream.Position = 0;
            string erwarteteZeile = "Aktivitaetstyp;Datum;Zeit;Dauer;Ort;Fokus" + Environment.NewLine;
            string zeile = _streamReader.ReadToEnd();

            zeile.Should().Be(erwarteteZeile);
        }

        [TestMethod()]
        public void WriteKopfZeile_SetSeparator()
        {
            _aktivitaetCsvWriter.SetSeparator("+");
            _aktivitaetCsvWriter.WriteKopfZeile(_streamWriter);

            _streamWriter.Flush();
            _memoryStream.Position = 0;
            string erwarteteZeile = "Aktivitaetstyp+Datum+Zeit+Dauer+Ort+Fokus" + Environment.NewLine;
            string zeile = _streamReader.ReadToEnd();

            zeile.Should().Be(erwarteteZeile);
        }

        [TestMethod()]
        public void WriteZeile()
        {
            Aktivitaet aktivitaet = new(Aktivitaet.AktivitaetsTyp.Training, new(2021, 6, 15), new(19, 30), 1.5, "Ort", "Fokus");
            List<Aktivitaet> aktivitaeten = new() { aktivitaet };
            Anwesenheitskontrolle anwesenheitskontrolle = new(new List<Person>(), aktivitaeten, new List<Anwesenheit>());

            int resultat = _aktivitaetCsvWriter.WriteZeilen(anwesenheitskontrolle, _streamWriter);

            _streamWriter.Flush();
            _memoryStream.Position = 0;
            string erwarteteZeile = "Training;15.06.2021;19:30;1.5;Ort;Fokus" + Environment.NewLine;
            string zeile = _streamReader.ReadToEnd();

            resultat.Should().Be(1);
            zeile.Should().Be(erwarteteZeile);
        }

        [TestMethod()]
        public void WriteZeile_SetSeparator()
        {
            Aktivitaet aktivitaet = new(Aktivitaet.AktivitaetsTyp.Training, new(2021, 6, 15), new(19, 30), 1.5, "Ort", "Fokus");
            List<Aktivitaet> aktivitaeten = new() { aktivitaet };
            Anwesenheitskontrolle anwesenheitskontrolle = new(new List<Person>(), aktivitaeten, new List<Anwesenheit>());

            _aktivitaetCsvWriter.SetSeparator("+");
            int resultat = _aktivitaetCsvWriter.WriteZeilen(anwesenheitskontrolle, _streamWriter);

            _streamWriter.Flush();
            _memoryStream.Position = 0;
            string erwarteteZeile = "Training+15.06.2021+19:30+1.5+Ort+Fokus" + Environment.NewLine;
            string zeile = _streamReader.ReadToEnd();

            resultat.Should().Be(1);
            zeile.Should().Be(erwarteteZeile);
        }
    }
}
