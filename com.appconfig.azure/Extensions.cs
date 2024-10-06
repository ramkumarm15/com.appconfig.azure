using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.NetworkInformation;

namespace com.appconfig.azure
{
    public static class Extensions
    {
        private static IConfigurationRefresher configRef;

        public static IConfigurationBuilder AddAzure(this IConfigurationBuilder config, AzureSettings settings)
        {
            config.AddAzureAppConfiguration(options =>
            {
                options.Connect(settings.ConnectionString)
                .Select("Testing:*", null)
                .ConfigureRefresh(refOpt =>
                {
                    refOpt.Register("Testing:Settings:Refresh", refreshAll: true);
                    refOpt.SetCacheExpiration(TimeSpan.FromSeconds(10));
                });
                options.ConfigureKeyVault(kv =>
                {
                    kv.SetCredential(new DefaultAzureCredential());
                });

                configRef = options.GetRefresher();

            }).Build();
            return config;
        }

        public static IApplicationBuilder UseAzureApp(this IApplicationBuilder app)
        {
            app.UseAzureAppConfiguration();
            return app;
        }
    }

}
