using ConfigurationService.Data.Response.ConfigurationResponse;
using MediatR;
using System.Collections.Generic;
using Dapper;
using System.Threading;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Linq;
using System;
using ConfigurationService.Data.Response.Response;
using ConfigurationService.Data.Response;

namespace ConfigurationService.Data.Request.ConfigurationRequest
{
    public class GetUnProcessedConfigurationRequest : IRequest<OperationResult<List<Configuration>>>
    {
    }

    public class GetUnProcessedConfigurationRequestHandler : IRequestHandler<GetUnProcessedConfigurationRequest, OperationResult<List<Configuration>>>
    {
        private IConfigHelper _configHelper;

        public GetUnProcessedConfigurationRequestHandler(IConfigHelper configHelper)
        {
            _configHelper = configHelper;
        }

        public async Task<OperationResult<List<Configuration>>> Handle(GetUnProcessedConfigurationRequest request, CancellationToken cancellationToken)
        {

            var resultList = new OperationResult<List<Configuration>>();
            resultList.Operations = new List<Configuration>();

            var query = @"SELECT c.[Id], 
                                 a.[Name] ,
                                 a.[Description],
                                 c.[ApplicationId], 
                                 c.[DataType],
                                 c.[Key] ,
                                 c.[Value] ,
                                 c.[IsActive] as ConfigurationState,
                                 a.[IsActive],
                                 c.[IsNew],
                                 c.[CreatedDate]
                                 FROM [ConfigurationServer].[dbo].[Configuration] as c  
                          JOIN [ConfigurationServer].[dbo].[Application] as a 
                          ON a.Id = c.ApplicationId Where c.IsActive=1 and c.IsNew=1 and c.IsProcessed=0";

            using (var conn = new SqlConnection(_configHelper.ConfigurationServiceDb))
            {
                try
                {
                    var result = await conn.QueryAsync<Configuration>(query);

                    resultList.Operations = result.ToList();
                    resultList.IsSuccess = result.Count() > 0 ? true : false;
                    resultList.Message = result.Count() > 0 ? "Success 😊" : "Failed 😢";
                }
                catch (Exception ex)
                {
                    resultList.IsSuccess = false;
                    resultList.Message = ex.Message;
                }
            }

            return resultList;
        }
    }
}
