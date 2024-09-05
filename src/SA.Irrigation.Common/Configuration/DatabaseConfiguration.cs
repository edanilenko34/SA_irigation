using Microsoft.Extensions.Configuration;

namespace SA.Irrigation.Common.Configuration
{
    public class DatabaseConfiguration
    {
        [ConfigurationKeyName("DB_CONNECTION_STRING")]
        public string ConnectionString { get; set; }
    }
}
