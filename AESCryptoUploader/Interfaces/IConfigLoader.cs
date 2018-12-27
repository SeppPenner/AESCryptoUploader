using Models;

namespace Interfaces
{
    public interface IConfigLoader
    {
        Config LoadConfigFromXmlFile(string filename);
    }
}