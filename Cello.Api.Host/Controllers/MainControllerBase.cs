using Cello.Api.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Cello.Api.Host.Controllers
{
    public abstract class MainControllerBase : ControllerBase
    {

        protected IPAddress remoteIp;

        public MainControllerBase()
        {
            remoteIp = GetRemoteIp(Request);
        }

        protected IPAddress GetRemoteIp(HttpRequest request)
        {
            if (request == null)
                return null;

            IPAddress ip = null;

            try
            {
                var values = request.Headers["X-Forwarded-For"];

                if (values == StringValues.Empty)
                {
                    return request.HttpContext?.Connection?.RemoteIpAddress;
                }
                else if (values.Count > 0)
                {
                    if (IPAddress.TryParse(values[0], out ip))
                        return ip;

                    return null;
                }

                return null;
            }
            catch (Exception ex)
            {
                //Logger.Error(ex);
            }

            return ip;
        }

        protected async Task SetResponseJSONAsync(WebServiceResponse response)
        {
            var bResponse = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response));

            await Response.Body.WriteAsync(bResponse, 0, bResponse.Length);
        }

        protected async Task SetResponseJSONAsync(string errorCode = null, object result = null, Exception ex = null)
        {
            if (errorCode == null)
                errorCode = "OK";

            Response.ContentType = "application/json";

            var response = new WebServiceResponse();

            response.ErrorCode = errorCode;

            response.Result = result;

            await SetResponseJSONAsync(response);
        }

        protected async Task SetResponseJSONAsync(string errorCode = null, Exception ex = null)
        {
            await SetResponseJSONAsync(errorCode, null, ex);
        }

        protected async Task SetResponseJSONAsync(string errorCode = null)
        {
            await SetResponseJSONAsync(errorCode, null, null);
        }

        protected async Task SetSuccessResponseJSONAsync(object result = null)
        {
            await SetResponseJSONAsync("OK", result);
        }
    }
}
