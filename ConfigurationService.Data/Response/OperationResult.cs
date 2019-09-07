
namespace ConfigurationService.Data.Response.Response
{
    public class OperationResult
    {
        public string Message { get; set; }

        public bool IsSuccess { get; set; }

    }

    public class OperationResult<T> : OperationResult
    {
        public T Operations { get; set; }
    }
}
