using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MeetGenerator.Model.Models;
using WebApiClientLibrary.Interfaces;
using System.Net.Http.Headers;

namespace WebApiClientLibrary.RequestHadlers
{
    public class PlaceRequestHandler : IPlaceRequestHandler
    {
        CRUDGeneralRequestHandler _crudHandler;
        const string _controller = "Place";

        public PlaceRequestHandler(string baseAddress)
        {
            _crudHandler = new CRUDGeneralRequestHandler(baseAddress);
        }

        public Task<HttpResponseMessage> Create(Place place)
        {
            return _crudHandler.Create(_controller, place);
        }

        public Task<HttpResponseMessage> Get(Guid id)
        {
            return _crudHandler.Get(_controller, id.ToString());
        }

        public Task<HttpResponseMessage> Update(Place place)
        {
            return _crudHandler.Update(_controller, place);
        }

        public Task<HttpResponseMessage> Delete(Guid id)
        {
            return _crudHandler.Delete(_controller, id.ToString());
        }
    }
}
