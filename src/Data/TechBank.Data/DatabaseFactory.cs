using TechBank.DomainModel.Helpers;

namespace TechBank.Data
{
    public class DatabaseFactory 
    {
        public static Database? CreateDatabase(string connectionName = "DefaultConnection")
        {
            var configuration = ConfigurationInfo.GetConfiguration();
            var connectionString = configuration.GetSection("ConnectionStrings").GetSection(connectionName).Value;

            if (!String.IsNullOrEmpty(connectionString))
            {
                Database database = new Database(connectionString);
                return database;
            }
            else
            {
                return null;
            }
        }
    }
}
