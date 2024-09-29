using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StockTrack.Helpers.Concrete
{
    public class ServiceResponse
    {
        public ServiceResponse(bool isSuccessful = true,
            HttpStatusCode statusCode=HttpStatusCode.OK)
        {
            IsSuccessful = isSuccessful;
            StatusCode = statusCode;
        }

        public ServiceResponse(string errorMessage,
            bool isSuccesFul=true,
            HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            ErrorMessage = errorMessage;
            IsSuccessful = isSuccesFul;
            StatusCode = statusCode;
        }

        public bool IsSuccessful { get; set; }
        public string ErrorMessage { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
