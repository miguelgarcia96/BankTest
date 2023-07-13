using Microsoft.Extensions.Configuration;
using System.IO;

namespace TechBank.DomainModel.Helpers
{
    public static class ConfigurationInfo
    {
        public static IConfigurationRoot GetConfiguration()
        {
            // https://stackoverflow.com/questions/46843367/how-to-setbasepath-in-configurationbuilder-in-core-2-0
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return configurationBuilder.Build();
        }
    }
}
