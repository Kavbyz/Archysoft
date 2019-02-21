using System;
using Archysoft.Domain.Model.Enums;

namespace Archysoft.Domain.Model.Exceptions
{
    public class BusinessException:Exception
    {
        public OperationResultCode Status { get; set; }
        public string Description { get; set; }

        public BusinessException(OperationResultCode status, string description):base(description)
        {
            Status = status;
            Description = description;
        }
    }
}
