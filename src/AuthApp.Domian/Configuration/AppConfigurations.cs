using System.Collections.Concurrent;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace AuthApp.Configuration
{
    public static class AppConfigurations
    {
        private static readonly ConcurrentDictionary<string, IConfigurationRoot> _configurationCache;

        static AppConfigurations()
        {
            _configurationCache = new ConcurrentDictionary<string, IConfigurationRoot>();
        }

        public static IConfigurationRoot Get(string path, string environmentName = null, bool addUserSecrets = false)
        {
            var cacheKey = path + "#" + environmentName + "#" + addUserSecrets;
            return _configurationCache.GetOrAdd(
                cacheKey,
                _ => BuildConfiguration(path, environmentName, addUserSecrets)
            );
        }

        private static IConfigurationRoot BuildConfiguration(string path, string environmentName = null, bool addUserSecrets = false)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            if (!(environmentName == null || environmentName == string.Empty))
            {
                builder = builder.AddJsonFile($"appsettings.{environmentName}.json", optional: true);
            }

            if (addUserSecrets)
            {
                builder.AddUserSecrets(typeof(AppConfigurations).GetTypeInfo().Assembly);
            }

            builder = builder.AddEnvironmentVariables();
              

            return builder.Build();
        }
    }
}
