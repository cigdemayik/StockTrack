using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StockTrack.Helpers.Concrete
{
    public class ServiceResponse<TResult>:ServiceResponse
    {
        public ServiceResponse(TResult result, bool isSuccesful,
            HttpStatusCode statusCode)
        {
            Result = result;
            IsSuccessful = isSuccesful;
            StatusCode = statusCode;
        }
        public ServiceResponse(TResult result, string errorMessage, bool isSuccessful,
            HttpStatusCode statusCode)
        {
            Result = result;
            ErrorMessage = errorMessage;
            IsSuccessful = isSuccessful;
            StatusCode = statusCode;
        }
        public TResult Result { get; set; } 
    }
}
