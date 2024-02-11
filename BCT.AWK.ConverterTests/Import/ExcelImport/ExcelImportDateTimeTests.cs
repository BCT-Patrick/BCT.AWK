using BCT.AWK.Converter.Import.ExcelImport;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;

namespace BCT.AWK.ConverterTests.Import.ExcelImport
{
    [TestClass]
    public class ExcelImportDateTimeTests
    {
        [TestMethod]
        public void GetDate_WhenNull()
        {
            DateOnly? result = ExcelImportDateTime.GetDate(null);

            result.Should().BeNull();
        }

        [TestMethod]
        public void GetDate_FromDateTime()
        {
            DateTime value = new(2022, 7, 23, 15, 44, 51, DateTimeKind.Unspecified);

            DateOnly? result = ExcelImportDateTime.GetDate(value);

            DateOnly ExpectedResult = new(2022, 7, 23);
            result.Should().Be(ExpectedResult);
        }

        [TestMethod]
        public void GetDate_FromDouble()
        {
            double value = 40_000.785;

            DateOnly? result = ExcelImportDateTime.GetDate(value);

            DateOnly ExpectedResult = new(2009, 7, 6);
            result.Should().Be(ExpectedResult);
        }

        [TestMethod]
        public void GetDate_FromString()
        {
            string value = new DateTime(2022, 7, 23,0,0,0, DateTimeKind.Unspecified).ToString(CultureInfo.CurrentCulture);

            DateOnly? result = ExcelImportDateTime.GetDate(value);

            DateOnly ExpectedResult = new(2022, 7, 23);
            result.Should().Be(ExpectedResult);
        }

        [TestMethod]
        public void GetTime_WhenNull()
        {
            TimeOnly? result = ExcelImportDateTime.GetTime(null);

            result.Should().BeNull();
        }

        [TestMethod]
        public void GetTime_FromDateTime()
        {
            DateTime value = new(2022, 7, 23, 15, 44, 51, DateTimeKind.Unspecified);

            TimeOnly? result = ExcelImportDateTime.GetTime(value);

            TimeOnly ExpectedResult = new(15,44,51);
            result.Should().Be(ExpectedResult);
        }

        [TestMethod]
        public void GetTime_FromDouble()
        {
            double value = 40_000.785;

            TimeOnly? result = ExcelImportDateTime.GetTime(value);

            TimeOnly ExpectedResult = new(18, 50, 24);
            result.Should().Be(ExpectedResult);
        }

        [TestMethod]
        public void GetTime_FromString()
        {
            string value = "15:44:51";

            TimeOnly? result = ExcelImportDateTime.GetTime(value);

            TimeOnly ExpectedResult = new(15, 44, 51);
            result.Should().Be(ExpectedResult);
        }
    }
}
