using BCT.AWK.Converter.Anwesenheitskontrollen;
using System.Collections.Generic;
using System.IO;

namespace BCT.AWK.Converter.Export
{
    internal interface IExport
    {
        List<string> Export(Anwesenheitskontrolle anwesenheitskontrolle, FileInfo excelFile);
    }
}
