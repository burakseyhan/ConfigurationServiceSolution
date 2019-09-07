using ConfigurationService.Data.Response;
using System.Configuration;

namespace ConfigurationService.Processor
{
    public class ConfigHelper : IConfigHelper
    {
        public string ConfigurationServiceDb => ConfigurationManager.ConnectionStrings["ConfigurationServer"].ToString();
    }
}
