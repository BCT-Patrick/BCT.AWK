using BCT.AWK.Converter.Anwesenheitskontrollen;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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

            _aktivitaetCsvWriter = new(_streamWriter, ";");
        }

        [TestMethod()]
        public void WriteKopfZeileTest()
        {
            _aktivitaetCsvWriter.WriteKopfZeile();

            _streamWriter.Flush();
            _memoryStream.Position = 0;
            string erwarteteZeile = "Aktivitaetstyp;Datum;Zeit;Dauer;Ort;Fokus" + Environment.NewLine;
            string zeile = _streamReader.ReadToEnd();

            zeile.Should().Be(erwarteteZeile);
        }

        [TestMethod()]
        public void WriteZeileTest()
        {
            Aktivitaet aktivitaet = new(Aktivitaet.AktivitaetsTyp.Training, new(2021, 6, 15), new(19, 30), 1.5, "Ort");
 
            _aktivitaetCsvWriter.WriteZeile(aktivitaet);

            _streamWriter.Flush();
            _memoryStream.Position = 0;
            string erwarteteZeile = "Training;15.06.2021;19:30;1.5;Ort;Fokus" + Environment.NewLine;
            string zeile = _streamReader.ReadToEnd();

            zeile.Should().Be(erwarteteZeile);
        }
    }
}
