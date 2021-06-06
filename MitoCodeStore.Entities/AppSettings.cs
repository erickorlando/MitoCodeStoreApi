namespace MitoCodeStore.Entities
{
    public class AppSettings
    {
        public StorageConfiguration StorageConfiguration { get; set; }
        public Jwt Jwt { get; set; }
    }

    public class Jwt
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SigningKey { get; set; }
    }

    public class StorageConfiguration
    {
        public string Path { get; set; }
        public string PublicUrl { get; set; }
    }
}