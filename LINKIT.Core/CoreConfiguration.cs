using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using System;
using Azure.Identity;

namespace LINKIT.Core
{
    public class CoreConfiguration
    {
        private static IConfiguration _configuration;

        private static CoreConfiguration _current;

        /// <summary>
        /// The current configuration settings from the Azure App Configuration
        /// </summary>
        public static CoreConfiguration Current
        {
            get
            {
                if (_current == null)
                {
                    // Reference default configuration sources
                    var configurationBuilder = new ConfigurationBuilder()
                        .SetBasePath(Environment.CurrentDirectory)
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                        .AddEnvironmentVariables();

                    // Read configuration settings
                    _configuration = configurationBuilder.Build();


                    // Read App Configuration settings from configuration
                    var connectionString = _configuration["AppConfiguration_ConnectionString"];

                    // Add App Configuration if exists
                    if (!string.IsNullOrEmpty(connectionString)) 
                    {
                        configurationBuilder
                            .AddAzureAppConfiguration(options =>
                        {
                            options.Connect(connectionString);
                            options.Select(KeyFilter.Any, LabelFilter.Null);
                            options.Select(KeyFilter.Any, _configuration["AppConfiguration_Stage"]);
                        });

                        // Read App Configuration settings
                        _configuration = configurationBuilder.Build();
                    }

                    // Create singleton configuration 
                    _current = new CoreConfiguration();
                }

                return _current;
            }
        }

        /// <summary>
        /// Get a specific configuration setting value by providing the setting key
        /// </summary>
        /// <param name="key">The configuration setting name</param>
        /// <returns></returns>
        public string GetSectionValue(string key)
        {
            var setting = _configuration.GetSection(key);

            return (setting != null) ?
                setting.Value :
                null;
        }
    }
}