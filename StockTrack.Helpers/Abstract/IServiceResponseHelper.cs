using StockTrack.Helpers.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StockTrack.Helpers.Abstract
{
    public interface IServiceResponseHelper
    {
        ServiceResponse SetError(string errorMessage, HttpStatusCode statusCode);
        ServiceResponse<T> SetError<T>(T data,string errorMessage, HttpStatusCode statusCode);
        ServiceResponse SetSuccess(HttpStatusCode statusCode);
        ServiceResponse<T> SetSuccess<T>(T data, HttpStatusCode statusCode);
    }

}
