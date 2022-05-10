using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LocutaEngine.Languages
{
    public static class Translator
    {
        private static Dictionary<string, string> dictionary = new Dictionary<string, string>();

        public static void LoadJson(string fileName)
        {
            string jsonString = File.ReadAllText(fileName);
            dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);
        }

        public static string Translate(string code)
        {
            return dictionary.ContainsKey(code) ? dictionary[code] : code;
        }

        public static string Translate(string code, Dictionary<string, string> variables)
        {
            if (!dictionary.ContainsKey(code))
                return code;

            List<string> splittedText = dictionary[code].Split(' ').ToList();

            string result = "";

            splittedText.ForEach(split =>
            {
                if (split[0] == ':')
                    split = Translate(variables[split.Remove(0, 1)]);
                result += $"{split} ";
            });

            return result;
        }

    }
}
