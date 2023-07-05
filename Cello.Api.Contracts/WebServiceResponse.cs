namespace Cello.Api.Contracts
{
    public class WebServiceResponse
    {
        public string ErrorCode { get; set; }
        public object Result { get; set; }

        public WebServiceResponse()
        {

        }

        public WebServiceResponse(string errorCode)
        {
            ErrorCode = errorCode;
        }

        public WebServiceResponse(string errorCode, object result)
        {
            ErrorCode = errorCode;
            Result = result;
        }
    }
}