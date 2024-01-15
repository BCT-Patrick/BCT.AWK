using System.Collections.Generic;
using BCT.AWK.Converter.Anwesenheitskontrolle;

namespace BCT.AWK.Converter.Import
{
    internal interface IImportRepository
    {
        List<Anwesenheit> Laden();
    }
}
