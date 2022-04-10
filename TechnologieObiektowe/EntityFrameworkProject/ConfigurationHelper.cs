using Microsoft.Extensions.Configuration;

namespace EntityFrameworkProject
{
    internal class ConfigurationHelper
    {
        public static IConfiguration GetConfiguration()
        {
            string workingDirectory = Environment.CurrentDirectory;

            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(workingDirectory).Parent.Parent.FullName)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            return configuration;
        }
    }
}
