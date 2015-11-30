using MeetGenerator.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebApiClientLibrary.Interfaces
{
    public interface IUserRequestHandler
    {
        Task<HttpResponseMessage> Create(User user);
        Task<HttpResponseMessage> Get(string userIdentificator);
        Task<HttpResponseMessage> Update(User user);
        Task<HttpResponseMessage> Delete(Guid id);
    }
}
