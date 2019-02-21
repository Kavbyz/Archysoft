using System;
using System.Linq;
using Archysoft.Domain.Model.Enums;
using Archysoft.Domain.Model.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Archysoft.Web.Api.Model
{
    public class ApiResponse
    {
        public OperationResultCode Status { get; set; }
        public string Message { get; set; }
        public long Timestamp { get; set; }

        public ApiResponse()
        {
            Status = OperationResultCode.Success;
            Message = "Success";
            Timestamp = DateTime.UtcNow.ConvertToTimestamp();
        }

        public ApiResponse(OperationResultCode status, string message)
        {
            Status = status;
            Message = message;
            Timestamp = DateTime.UtcNow.ConvertToTimestamp();
        }

        public ApiResponse(ModelStateDictionary contextModelState)
        {
            if (contextModelState == null)
            {
                return;
            }

            Status = OperationResultCode.Error;
            Timestamp= DateTime.UtcNow.ConvertToTimestamp();
            var validateKeys = contextModelState.Keys.ToList();
            foreach (var validateKey in validateKeys)
            {
                if (contextModelState[validateKey].ValidationState == ModelValidationState.Invalid)
                {
                    Message += contextModelState[validateKey].Errors.FirstOrDefault()?.ErrorMessage;
                }
            }
        }
    }

    public class ApiResponse<T> : ApiResponse where T : class
    {
        public T Model { get; set; }

        public ApiResponse()
        {
            
        }

        public ApiResponse(T model)
        {
            Model = model;
        }
    }
}
