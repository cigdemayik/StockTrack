using StockTrack.Helpers.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StockTrack.Helpers.Concrete
{
    public class ServiceResponseHelper : IServiceResponseHelper
    {
        public ServiceResponse SetError(string errorMessage, HttpStatusCode statusCode)
        {
            return new ServiceResponse(errorMessage, false, statusCode);
        }

        public ServiceResponse<T> SetError<T>(T data, string errorMessage, HttpStatusCode statusCode)
        {
            return new ServiceResponse<T>(data, errorMessage, false, statusCode);
        }

        public ServiceResponse SetSuccess(HttpStatusCode statusCode)
        {
            return new ServiceResponse(null, true, statusCode);
        }

        public ServiceResponse<T> SetSuccess<T>(T data, HttpStatusCode statusCode)
        {
            return new ServiceResponse<T>(data, null, true, statusCode);
        }
    }
}
