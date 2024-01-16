using BCT.AWK.Converter.Anwesenheitskontrolle;
using System.Collections.Generic;

namespace BCT.AWK.Converter.Export
{
    internal interface IExportRepository
    {
        int Export(IEnumerable<Anwesenheit> anwesenheiten);
    }
}
