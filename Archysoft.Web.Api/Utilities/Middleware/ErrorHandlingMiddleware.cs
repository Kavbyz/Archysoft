using System;
using System.Net;
using System.Threading.Tasks;
using Archysoft.Domain.Model.Enums;
using Archysoft.Domain.Model.Exceptions;
using Archysoft.Web.Api.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Archysoft.Web.Api.Utilities.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate requestDelegate, ILoggerFactory loggerFactory)
        {
            _requestDelegate = requestDelegate;
            _logger = loggerFactory.CreateLogger<ErrorHandlingMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (Exception e)
            {
                var exceptionType = e.GetType();
                LogException(exceptionType, e);

                if (exceptionType == typeof(UnauthorizedAccessException))
                {
                    await HandleUnauthorizedException(context);
                }
                else
                {
                    await HandleExceptionAsync(context, e);
                }
            }
        }

        public virtual Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = new ApiResponse(OperationResultCode.Success, exception.Message);
            if (exception is BusinessException businessException)
            {
                response.Status = businessException.Status;
                response.Message = response.Message.Contains(nameof(BusinessException)) ? businessException.Description : response.Message;
            }

            var model = JsonConvert.SerializeObject(response,
                new JsonSerializerSettings {ContractResolver = new CamelCasePropertyNamesContractResolver()});
            return context.Response.WriteAsync(model);
        }

        public virtual Task HandleUnauthorizedException(HttpContext context)
        {
            context.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
            return Task.FromResult(0);
        }

        private void LogException(Type exceptionType, Exception exception)
        {
            if (exceptionType != typeof(BusinessException) || exceptionType != typeof(UnauthorizedAccessException))
            {
                _logger.LogError(exception, exception.Message);
            }
        }
    }
}
