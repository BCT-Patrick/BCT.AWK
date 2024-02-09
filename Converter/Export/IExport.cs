using BCT.AWK.Converter.Anwesenheitskontrollen;
using System.IO;

namespace BCT.AWK.Converter.Export
{
    internal interface IExport
    {
        int Export(Anwesenheitskontrolle anwesenheitskontrolle, FileInfo excelFile);
    }
}
