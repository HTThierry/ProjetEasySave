using Newtonsoft.Json;
using System.Globalization;

namespace ProjetEasySave.lib.ViewModels
{
    public class langue
    {
        public string LanguageCode { get; set; }
        public dynamic LanguageData { get; set; }

        public langue(string specifiedLanguageCode = "")
        {
            this.LanguageCode = GetLanguageCode(specifiedLanguageCode);
            string json = File.ReadAllText(@"..\..\..\..\ProjetOrienteObjet.lib\ViewModels\languages.json"); //C: \Users\cocac\Source\Repos\HTThierry\ProjetEasySave\ProjetOrienteObjet.lib\ViewModels\languages.json
            this.LanguageData = JsonConvert.DeserializeObject(json);
        }

        // Change le language code celon le choix de l'utilisateur, sinon retourne un langague code celon le système
        public string GetLanguageCode(string specifiedLanguageCode = "")
        {
            if (!string.IsNullOrEmpty(specifiedLanguageCode))
            {
                return specifiedLanguageCode;
            }

            return CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
        }
    }
}