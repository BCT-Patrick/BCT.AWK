using BCT.AWK.Converter.Anwesenheitskontrollen;
using System.Collections.Generic;
using System.IO;

namespace BCT.AWK.Converter.Export
{
    internal interface IExport
    {
        int Export(IEnumerable<Anwesenheit> anwesenheiten, FileInfo excelFile);
    }
}
