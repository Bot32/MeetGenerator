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
        string baseAddress;

        public UserRequestHandler(string baseAddress)
        {
            this.baseAddress = baseAddress;
        }

        public async Task<HttpResponseMessage> Create(User user)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                return await client.PostAsJsonAsync("api/User/Create", user);
            }
        }

        public async Task<HttpResponseMessage> Get(string userIdentificator)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                return await client.GetAsync("api/User/Get?userIdentificator=" + userIdentificator);
            }
        }

        public async Task<HttpResponseMessage> Update(User user)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                return await client.PutAsJsonAsync("api/User/Update", user);
            }
        }

        public async Task<HttpResponseMessage> Delete(Guid id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);;
                return await client.DeleteAsync("api/User/Delete?id=" + id);
            }
        }
    }
}
