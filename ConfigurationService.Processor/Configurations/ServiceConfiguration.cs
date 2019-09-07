using ConfigurationService.Data.Response;
using ConfigurationService.Processor.Processes;
using ConfigurationService.Processor.Processes.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace ConfigurationService.Processor.Configurations
{
    public class ServiceConfiguration
    {
        public IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<ConfigurationService>();

            services.AddSingleton<IConfigurationService, ConfigurationProcess>();

            services.AddSingleton<IConfigHelper, ConfigHelper>();

            var assemblies = Assembly.GetEntryAssembly().GetReferencedAssemblies().Select(Assembly.Load).ToArray();

            services.AddMediatR(assemblies);

            return services;
        }
    }
}
