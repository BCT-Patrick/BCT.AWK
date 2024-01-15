namespace BCT.AWK.Converter.Konfiguration
{
    internal interface IKonfigurationRepository
    {
        bool Existiert();
        ConverterKonfiguration? Laden();
        void Speicheren(ConverterKonfiguration konfiguration);
        ConverterKonfiguration StandardLaden();
    }
}