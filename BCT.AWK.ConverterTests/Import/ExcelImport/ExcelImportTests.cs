using Microsoft.VisualStudio.TestTools.UnitTesting;
using BCT.AWK.Converter.Import.ExcelImport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCT.AWK.Converter.Import.AnwesenheitImport;
using BCT.AWK.Converter.Anwesenheitskontrollen;
using FluentAssertions;

namespace BCT.AWK.Converter.Import.ExcelImport
{
    [TestClass()]
    public class ExcelImportTests
    {
        private readonly ExcelImport _excelImport;

        public ExcelImportTests()
        {
            ImportKonfiguration importKonfiguration = new();
            _excelImport = new(importKonfiguration);
        }

        [TestMethod()]
        public void Laden()
        {
            System.IO.FileInfo importFileInfo = new("./Awk/Anwesenheitskontrolle.xlsx");
            Anwesenheitskontrolle anwesenheitskontrolle = _excelImport.Laden(importFileInfo);

            List<Person> erwartetePersonen = GetErwartetePersonen();
            anwesenheitskontrolle.Personen.Should().BeEquivalentTo(erwartetePersonen);

            List<Training> erwarteteTrainings = GetTrainings();
            anwesenheitskontrolle.Trainings.Should().BeEquivalentTo(erwarteteTrainings);

            int erwarteteAnzahlAnwesenheiten = erwartetePersonen.Count * erwarteteTrainings.Count;
            anwesenheitskontrolle.Anwesenheiten.Should().HaveCount(erwarteteAnzahlAnwesenheiten);
        }

        private List<Person> GetErwartetePersonen()
        {
            List<Person> personen = new()
            {
                new("123456001", "Vorname1", "Nachname1", new(2000,1,1), Person.GeschlechtTyp.Maennlich, null, null, Person.NationalitaetTyp.CH, Person.SpracheTyp.Deutsch, null, null, null, null, "ch"),
                new("123456002", "Vorname2", "Nachname2", new(2000,1,2), Person.GeschlechtTyp.Weiblich, null, null, Person.NationalitaetTyp.FL, Person.SpracheTyp.Franzoesisch, null, null, null, null, "li"),
                new("123456003", "Vorname3", "Nachname3", new(2000,1,3), Person.GeschlechtTyp.Maennlich, null, null, Person.NationalitaetTyp.Andere, Person.SpracheTyp.Italienisch, null, null, null, null, "de"),
                new("123456004", "Vorname4", "Nachname4", new(2000,1,4), Person.GeschlechtTyp.Weiblich, null, null, Person.NationalitaetTyp.CH, Person.SpracheTyp.Andere, null, null, null, null, "fr"),
                new("123456005", "Vorname5", "Nachname5", new(2000,1,5), Person.GeschlechtTyp.Maennlich, null, null, Person.NationalitaetTyp.FL, Person.SpracheTyp.Deutsch, null, null, null, null, "it"),
                new("123456006", "Vorname6", "Nachname6", new(2000,1,6), Person.GeschlechtTyp.Weiblich, null, null, Person.NationalitaetTyp.Andere, Person.SpracheTyp.Franzoesisch, null, null, null, null, "at"),
                new("123456007", "Vorname7", "Nachname7", new(2000,1,7), Person.GeschlechtTyp.Maennlich, null, null, Person.NationalitaetTyp.CH, Person.SpracheTyp.Italienisch, null, null, null, null, "CH"),
                new("123456008", "Vorname8", "Nachname8", new(2000,1,8), Person.GeschlechtTyp.Weiblich, null, null, Person.NationalitaetTyp.FL, Person.SpracheTyp.Andere, null, null, null, null, "LI"),
                new("123456009", "Vorname9", "Nachname9", new(2000,1,9), Person.GeschlechtTyp.Maennlich, null, null, Person.NationalitaetTyp.Andere, Person.SpracheTyp.Deutsch, null, null, null, null, "DE"),
                new("123456010", "Vorname10", "Nachname10", new(2000,1,10), Person.GeschlechtTyp.Weiblich, null, null, Person.NationalitaetTyp.CH, Person.SpracheTyp.Franzoesisch, null, null, null, null, "FR"),
                new("123456011", "Vorname11", "Nachname11", new(2000,1,11), Person.GeschlechtTyp.Maennlich, null, null, Person.NationalitaetTyp.FL, Person.SpracheTyp.Italienisch, null, null, null, null, "IT"),
                new("123456012", "Vorname12", "Nachname12", new(2000,1,12), Person.GeschlechtTyp.Weiblich, null, null, Person.NationalitaetTyp.Andere, Person.SpracheTyp.Andere, null, null, null, null, "AT"),
                new("123456013", "Vorname13", "Nachname13", new(2000,1,13), Person.GeschlechtTyp.Maennlich, null, null, Person.NationalitaetTyp.CH, Person.SpracheTyp.Deutsch, null, null, null, null, "ch"),
                new("123456014", "Vorname14", "Nachname14", new(2000,1,14), Person.GeschlechtTyp.Weiblich, null, null, Person.NationalitaetTyp.FL, Person.SpracheTyp.Franzoesisch, null, null, null, null, "li"),
                new("123456015", "Vorname15", "Nachname15", new(2000,1,15), Person.GeschlechtTyp.Maennlich, null, null, Person.NationalitaetTyp.Andere, Person.SpracheTyp.Italienisch, null, null, null, null, "de"),
                new("123456016", "Vorname16", "Nachname16", new(2000,1,16), Person.GeschlechtTyp.Weiblich, null, null, Person.NationalitaetTyp.CH, Person.SpracheTyp.Andere, null, null, null, null, "fr"),
                new("123456017", "Vorname17", "Nachname17", new(2000,1,17), Person.GeschlechtTyp.Maennlich, null, null, Person.NationalitaetTyp.FL, null, null, null, null, null, "it"),
                new("123456018", "Vorname18", "Nachname18", new(2000,1,18), Person.GeschlechtTyp.Weiblich, null, null, Person.NationalitaetTyp.Andere, null, null, null, null, null, "at"),
                new(null, "Vorname19", null, null, null, null, null, null, null, null, null, null, null, null),
                new(null, null, "Nachname20", null, null, null, null, null, null, null, null, null, null, null)
            };

            return personen;
        }

        private List<Training> GetTrainings()
        {
            List<Training> training = new()
            {
                new(Training.TrainingsTyp.Training, new(2022,8,18), new(20,0,0), 90, null),
                new(Training.TrainingsTyp.Training, new(2022,8,19), new(20,0,0), 90, null),
                new(Training.TrainingsTyp.Training, new(2022,8,25), new(20,0,0), 90, null),
                new(Training.TrainingsTyp.Training, new(2022,9,1), new(20,0,0), 90, null),
                new(Training.TrainingsTyp.Wettkampf, new(2022,9,8), null, null, null),
                new(Training.TrainingsTyp.Training, new(2022,9,15), new(20,0,0), 90, null),
                new(Training.TrainingsTyp.Training, new(2022,9,22), new(20,0,0), 90, null),
                new(Training.TrainingsTyp.Training, new(2022,9,29), new(20,0,0), 90, null),
                new(Training.TrainingsTyp.Training, new(2022,10,6), new(20,0,0), 90, null),
                new(Training.TrainingsTyp.Wettkampf, new(2022,10,13), null, null, null)
            };

            return training;
        }
    }
}