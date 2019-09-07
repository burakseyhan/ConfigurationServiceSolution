using ConfigurationService.Processor.Configurations;
using Microsoft.Extensions.DependencyInjection;
using System;
using Topshelf;

namespace ConfigurationService.Processor
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceConfiguration = new ServiceConfiguration();
            var services = serviceConfiguration.ConfigureServices();

            HostFactory.Run(x =>
            {
                x.Service<ConfigurationService>(s =>
                {
                    s.ConstructUsing(settings =>
                    {
                        var provider = services.BuildServiceProvider();

                        return provider.GetService<ConfigurationService>();
                    });
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });

                x.SetDisplayName("Configuration Service");
                x.SetDescription("This service allows that manage list of configuration datas");
                x.SetServiceName("ConfigurationService");
                x.RunAsLocalService();
                x.StartAutomatically();
            });
        }
    }
}
