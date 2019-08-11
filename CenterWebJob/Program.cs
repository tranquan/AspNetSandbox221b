using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;

namespace CenterWebJob
{
    // To learn more about Microsoft Azure WebJobs SDK, please see https://go.microsoft.com/fwlink/?LinkID=320976
    class Program
    {
        // Please set the following connection strings in app.config for this WebJob to run:
        // AzureWebJobsDashboard and AzureWebJobsStorage
        static void Main()
        {
            // Initialize();
            Console.WriteLine("CenterWebJob started");

            //var config = new JobHostConfiguration();

            //if (config.IsDevelopment)
            //{
            //    config.UseDevelopmentSettings();
            //}

            //config.DashboardConnectionString = "UseDevelopmentStorage=true;";
            //config.StorageConnectionString = "UseDevelopmentStorage=true;";

            // var host = new JobHost(config);
            //// The following code ensures that the WebJob will be running continuously
            // host.RunAndBlock();

            var host = new JobHost();
            host.RunAndBlock();
        }

        static void Initialize()
        {
            var appSettings = ConfigurationManager.AppSettings;
            var prefix = "environment-";
            var envKeys = appSettings.AllKeys.Where(s => s.StartsWith(prefix)).ToList();
            envKeys.ForEach(key => {
                var realKey = key.Substring(prefix.Length);
                var value = appSettings[key];
               //  value = ReadFromKeyVaultIfRequired(realKey, value);
                Environment.SetEnvironmentVariable(realKey, value);
            });
        }
    }
}
