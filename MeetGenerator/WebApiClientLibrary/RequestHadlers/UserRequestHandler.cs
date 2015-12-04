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
    public class UserRequestHandler : IUserRequestHandler
    {
        CRUDGeneralRequestHandler _crudHandler;
        const string _controller = "User";

        public UserRequestHandler(string baseAddress)
        {
            _crudHandler = new CRUDGeneralRequestHandler(baseAddress);
        }

        public Task<HttpResponseMessage> Create(User user)
        {
            return _crudHandler.Create(_controller, user);
        }

        public Task<HttpResponseMessage> Get(string identificator)
        {
            return _crudHandler.Get(_controller, identificator);
        }

        public Task<HttpResponseMessage> Update(User user)
        {
            return _crudHandler.Update(_controller, user);
        }

        public Task<HttpResponseMessage> Delete(Guid id)
        {
            return _crudHandler.Delete(_controller, id.ToString());
        }
    }
}
