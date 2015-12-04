using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace MeetGenerator.API.HttpActionResults
{
    public class MethodNotAllowedResult : IHttpActionResult
    {
        private string allowedMethods;

        public MethodNotAllowedResult(string allowedMethods)
        {
            this.allowedMethods = allowedMethods;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(HttpStatusCode.MethodNotAllowed);
            response.Headers.Add("Allow", allowedMethods);
            return Task.FromResult(response);
        }
    }
}