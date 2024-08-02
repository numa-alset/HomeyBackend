namespace HomeyBackend.Core.Models
{
    public class PhotoSettings
    {
        public int MaxBytes { get; set; }
        public String[] AcceptedFileTypes { get; set; }

        public bool IsSupported(string fileName)
        {
            return AcceptedFileTypes.Any(s => s == Path.GetExtension(fileName).ToLower());
        }
    }
}
