namespace POSApi.Enviroments.Keys
{
    public class Llaves
    {
        // Cifrado AES (tienen que ser las mismas de backoffice)
        //public readonly string AES_KEYs = _configuration.GetValue<string>("AesKeys:AES_KEY");
        public readonly string AES_KEY = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build().GetValue<string>("AesKeys:AES_KEY");


        public readonly string AES_VECTOR_INITIAL = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build().GetValue<string>("AesKeys:AES_VECTOR_INITIAL");
    }
}
