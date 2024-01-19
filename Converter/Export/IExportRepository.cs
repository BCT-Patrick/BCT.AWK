using BCT.AWK.Converter.Anwesenheitskontrolle;
using System.Collections.Generic;
using System.IO;

namespace BCT.AWK.Converter.Export
{
    internal interface IExportRepository
    {
        int Export(IEnumerable<Anwesenheit> anwesenheiten, FileInfo excelFile);
    }
}
