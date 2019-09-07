using ConfigurationService.Data.Request.ConfigurationRequest;
using ConfigurationService.Library;
using ConfigurationService.Processor.Processes.Services;
using MediatR;
using System;

namespace ConfigurationService.Processor.Processes
{
    public class ConfigurationProcess : IConfigurationService
    {
        private readonly IMediator mediator;

        public ConfigurationProcess(IMediator mediator)
        {
            this.mediator = mediator;
        }
        
        public void Init()
        {

        }

        public void Process()
        {
            var result = this.mediator.Send(new GetUnProcessedConfigurationRequest() { });

            Console.WriteLine($"New Configuration Count : {result.Result.Operations.Count}");

            foreach (var item in result.Result.Operations)
            {
                ConfigurationHelper<string> configurationHelper = new ConfigurationHelper<string>(item.Description);

                object data = configurationHelper.GetValue(item.Key);

                if (data == null)
                {
                    configurationHelper.SetValue(item.Key, item.Value);
                }

                this.mediator.Send(new UpdateProcessedConfigurationRequest()
                {
                    Id = item.Id
                });
            }
        }
    }
}
