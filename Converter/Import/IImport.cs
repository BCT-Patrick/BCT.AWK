using System.IO;
using BCT.AWK.Converter.Anwesenheitskontrollen;

namespace BCT.AWK.Converter.Import
{
    internal interface IImport
    {
        Anwesenheitskontrolle Laden(FileInfo excelFile);
    }
}
