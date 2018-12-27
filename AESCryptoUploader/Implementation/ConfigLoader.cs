using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;
using Interfaces;
using Models;

namespace Implementation
{
    public class ConfigLoader : IConfigLoader
    {
        public Config LoadConfigFromXmlFile(string filename)
        {
            var xDocument = XDocument.Load(filename);
            return CreateObjectsFromString<Config>(xDocument);
        }

        private T CreateObjectsFromString<T>(XDocument xDocument)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            return (T) xmlSerializer.Deserialize(new StringReader(xDocument.ToString()));
        }
    }
}