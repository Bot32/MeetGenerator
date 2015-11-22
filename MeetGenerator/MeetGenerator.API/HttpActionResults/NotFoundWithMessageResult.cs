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
    public class NotFoundWithMessageResult : IHttpActionResult
    {
        private string errorsList;

        public NotFoundWithMessageResult(string errorsList)
        {
            this.errorsList = errorsList;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(HttpStatusCode.NotFound);
            response.Content = new StringContent(errorsList);
            return Task.FromResult(response);
        }

    }
}