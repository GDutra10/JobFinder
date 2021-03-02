using JobFinder.Models.API.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace JobFinder.Factories
{
    public static class APIResponseFactory
    {

        public static APIResponse Create(HttpStatusCode httpStatusCode)
        {

            switch (httpStatusCode)
            {
                case HttpStatusCode.OK:
                    return new APIResponse
                    {
                        Title = "OK",
                        Status = Convert.ToInt32(httpStatusCode),
                        Errors = null
                    };
                case HttpStatusCode.BadRequest:
                    return new APIResponse
                    {
                        Title = "One or more validation errors occurred.",
                        Status = Convert.ToInt32(httpStatusCode),
                        Errors = new Dictionary<string, List<string>>()
                    };
                case HttpStatusCode.Unauthorized:
                    return new APIResponse
                    {
                        Title = "Unauthorized.",
                        Status = Convert.ToInt32(httpStatusCode),
                        Errors = new Dictionary<string, List<string>>()
                    };
                default:
                    throw new Exception($"HttpStatusCode '{httpStatusCode}' not Implemented!");
            }

        }

    }
}
