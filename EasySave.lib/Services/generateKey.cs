using System.Configuration;

namespace EasySave.lib.Services
{
    public class GenerateKey
    {
        public int Generate()
        {
            var random = new Random();
            ulong key = (ulong)random.Next() << 32 | (ulong)random.Next();
            string keyPath = $@"{ConfigurationManager.AppSettings["CryptKeyPath"]}";
            try
            {
                using (StreamWriter sw = new StreamWriter(keyPath))
                {
                    sw.WriteLine(key.ToString());
                }
                return 0;
            }
            catch
            {
                return 1;
            }
        }
    }
}