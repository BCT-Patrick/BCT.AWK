using BCT.AWK.Converter.Anwesenheitskontrolle;
using System.Collections.Generic;

namespace BCT.AWK.Converter.Export
{
    internal interface IExportRepository
    {
        void Export(IEnumerable<Anwesenheit> anwesenheiten);
    }
}
