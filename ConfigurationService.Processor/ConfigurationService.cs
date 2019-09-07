using ConfigurationService.Processor.Processes.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConfigurationService.Processor
{
    public class ConfigurationService
    {
        private readonly IConfigurationService configurationService;
        private CancellationTokenSource _tokenSource;
        private CancellationToken _token;

        private Task _processConfigurationTask;

        public ConfigurationService(IConfigurationService configurationService)
        {
            this.configurationService = configurationService;

            _tokenSource = new CancellationTokenSource();
            _token = _tokenSource.Token;
        }

        public void Start()
        {
            this._processConfigurationTask = Task.Run(() =>
            {
                this.configurationService.Init();

                while (!_token.IsCancellationRequested)
                {
                    try
                    {
                        this.configurationService.Process();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    finally
                    {
                        Thread.Sleep(5000);
                    }
                }
            }, _token);
        }

        public void Stop()
        {
            _tokenSource.Cancel(true);

            if (this._processConfigurationTask != null)
            {
                this._processConfigurationTask.Wait();
            }
        }
    }
}
