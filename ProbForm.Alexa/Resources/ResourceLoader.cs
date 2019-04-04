using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace ProbForm.Alexa.Resources
{
    public class ResourceLoader
    {
        private readonly Dictionary<string, string> InternalDictionary;

        private ResourceLoader()
        {
            var assembly = typeof(ResourceLoader).GetTypeInfo().Assembly;
            var resourceName = assembly.GetName().Name + ".Resources.strings.json";

            using (var resourceStream = assembly.GetManifestResourceStream(resourceName))
            {
                using (var reader = new StreamReader(resourceStream))
                {
                    var json = reader.ReadToEnd();
                    this.InternalDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                }
            }
        }

        public string GetResource(string name)
        {
            if (!this.InternalDictionary.TryGetValue(name, out string value)) return null;

            return value;
        }

        private static ResourceLoader current;
        public static ResourceLoader Current
        {
            get
            {
                if (current == null)
                {
                    current = new ResourceLoader();
                }

                return current;
            }
        }
    }
}
 