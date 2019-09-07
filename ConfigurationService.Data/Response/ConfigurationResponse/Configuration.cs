
namespace ConfigurationService.Data.Response.ConfigurationResponse
{
    public class Configuration
    {
        public int Id { get; set; }

        public int ApplicationId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public bool IsProcessed { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }

        public string DataType { get; set; }
    }
}
