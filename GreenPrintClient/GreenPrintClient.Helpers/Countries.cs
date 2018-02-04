using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace GreenPrintClient.Helpers
{
    public class Nld
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Pap
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Native
    {
        public Nld nld { get; set; }
        public Pap pap { get; set; }
    }

    public class Name
    {
        public string common { get; set; }
        public string official { get; set; }
        public Native native { get; set; }
    }

    public class Languages
    {
        public string nld { get; set; }
        public string pap { get; set; }
    }

    public class Deu
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Fra
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Hrv
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Ita
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Jpn
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Nld2
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Por
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Rus
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Slk
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Spa
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Fin
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Est
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Zho
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Translations
    {
        public Deu deu { get; set; }
        public Fra fra { get; set; }
        public Hrv hrv { get; set; }
        public Ita ita { get; set; }
        public Jpn jpn { get; set; }
        public Nld2 nld { get; set; }
        public Por por { get; set; }
        public Rus rus { get; set; }
        public Slk slk { get; set; }
        public Spa spa { get; set; }
        public Fin fin { get; set; }
        public Est est { get; set; }
        public Zho zho { get; set; }
    }

    public class Country
    {
        public Name name { get; set; }
        public List<string> tld { get; set; }
        public string cca2 { get; set; }
        public string ccn3 { get; set; }
        public string cca3 { get; set; }
        public string cioc { get; set; }
        public bool? independent { get; set; }
        public string status { get; set; }
        public List<string> currency { get; set; }
        public List<string> callingCode { get; set; }
        public List<string> capital { get; set; }
        public List<string> altSpellings { get; set; }
        public string region { get; set; }
        public string subregion { get; set; }
        public Languages languages { get; set; }
        public Translations translations { get; set; }
        public List<double> latlng { get; set; }
        public string demonym { get; set; }
        public bool landlocked { get; set; }
        public List<object> borders { get; set; }
        public long area { get; set; }
        public string flag { get; set; }
    }

    public static class Countries
    {
        public static List<Country> GetDetailedData()
        {
            string _json = init("countries");

            List<Country> list = new List<Country>();

            list = JsonConvert.DeserializeObject<List<Country>>(_json);

            return list;
        }

        public static Dictionary<string, string> GetData()
        {
            string _json = init("countrycodes");

            Dictionary<string, string> dicCountries = JsonConvert.DeserializeObject<Dictionary<string, string>>(_json);

            return dicCountries;
        }

        private static string init(string jsonFile)
        {
            string result = string.Empty;
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = $"GreenPrintClient.Helpers.{jsonFile}.json";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }

            return result;
        }
    }

}
