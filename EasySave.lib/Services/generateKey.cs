using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySave.lib.Services
{
    public class generateKey
    {
        public bool Generate()
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
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
