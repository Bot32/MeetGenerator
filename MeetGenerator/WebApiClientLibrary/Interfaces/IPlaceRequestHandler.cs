using MeetGenerator.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebApiClientLibrary.Interfaces
{
    public  interface IPlaceRequestHandler
    {
        Task<HttpResponseMessage> Create(Place place);
        Task<HttpResponseMessage> Get(Guid id);
        Task<HttpResponseMessage> Update(Place place);
        Task<HttpResponseMessage> Delete(Guid id);
    }
}
