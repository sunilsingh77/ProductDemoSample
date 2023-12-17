using Microsoft.Extensions.Configuration;

namespace CommonLibrary
{
    public class ConfigManager : IConfigManager
    {
        private readonly IConfiguration _configuration;
        public ConfigManager(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public string ProductDB
        {
            get
            {
                return this._configuration["ConnectionStrings:ProductDB"];
            }
        }

        public string EmailID
        {
            get
            {
                return this._configuration["AppSeettings:EmailID"];
            }
        }

        public string AccountKey
        {
            get
            {
                return this._configuration["AppSeettings:AccountKey"];
            }
        }

        public IConfigurationSection GetConfigurationSection(string key)
        {
            return this._configuration.GetSection(key);
        }

        public string GetConnectionString(string connectionName)
        {
            return this._configuration.GetConnectionString(connectionName);
        }
    }
    public interface IConfigManager
    {
        string ProductDB { get; }

        string EmailID { get; }

        string AccountKey { get; }

        string GetConnectionString(string connectionName);

        IConfigurationSection GetConfigurationSection(string Key);
    }
}