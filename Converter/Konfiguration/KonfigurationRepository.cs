using System.IO;
using System.Text.Json;

namespace BCT.AWK.Converter.Konfiguration
{
    internal class JsonKonfigurationRepository : IKonfigurationRepository
    {
        private readonly FileInfo _konfigurationFile;

        public JsonKonfigurationRepository(FileInfo konfigurationFile)
        {
            _konfigurationFile = konfigurationFile;
        }

        public bool Existiert()
        {
            return _konfigurationFile.Exists;
        }

        public ConverterKonfiguration? Laden()
        {
            string path = _konfigurationFile.FullName;
            FileStreamOptions streamOptions = new()
            {
                Access = FileAccess.Read,
                Mode = FileMode.Open
            };

            using FileStream stream = new(path, streamOptions);

            JsonSerializerOptions? jsonOptions = new()
            {
                WriteIndented = true
            };

            ConverterKonfiguration? konfiguration = JsonSerializer.Deserialize<ConverterKonfiguration>(stream, jsonOptions);
            return konfiguration;
        }

        public void Speicheren(ConverterKonfiguration konfiguration)
        {
            string path = _konfigurationFile.FullName;
            FileStreamOptions options = new()
            {
                Access = FileAccess.ReadWrite,
                Mode = FileMode.Create
            };

            using FileStream stream = new(path, options);

            JsonSerializerOptions? jsonOptions = new()
            {
                WriteIndented = true
            };

            JsonSerializer.Serialize(stream, konfiguration, jsonOptions);
        }

        public ConverterKonfiguration StandardLaden()
        {
            ConverterKonfiguration konfigureuration = new();
            return konfigureuration;
        }
    }
}
