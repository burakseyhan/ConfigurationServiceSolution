using ConfigurationService.Data.Response.ConfigurationResponse;
using ConfigurationService.Data.Response.Response;
using MediatR;
using System;
using System.Data.SqlClient;
using Dapper;
using System.Threading;
using System.Threading.Tasks;
using ConfigurationService.Data.Response;

namespace ConfigurationService.Data.Request.ConfigurationRequest
{
    public class UpdateProcessedConfigurationRequest : IRequest<OperationResult<bool>>
    {
        public int Id { get; set; }
    }

    public class UpdateProcessedConfigurationRequestHandler : IRequestHandler<UpdateProcessedConfigurationRequest, OperationResult<bool>>
    {
        private readonly IConfigHelper _configHelper;
        public UpdateProcessedConfigurationRequestHandler(IConfigHelper configHelper)
        {
            _configHelper = configHelper;
        }

        public async Task<OperationResult<bool>> Handle(UpdateProcessedConfigurationRequest request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<bool>();

            var query = @"Update Configuration Set IsProcessed=1 Where Id=@Id ";

            using (var conn = new SqlConnection(_configHelper.ConfigurationServiceDb))
            {
                try
                {
                    var querResult = await conn.ExecuteAsync(query, new { Id = request.Id });

                    result.IsSuccess = querResult > 0 ? true : false;
                    result.Message = querResult > 0 ? "Success 😊" : "Failed 😢";
                }
                catch (Exception ex)
                {
                    result.IsSuccess = false;
                    result.Message = ex.Message;
                }
            }

            return result;
        }
    }
}
