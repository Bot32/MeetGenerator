using MeetGenerator.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiClientLibrary.Interfaces;
using System.Net.Http;
using System.Net.Http.Headers;

namespace WebApiClientLibrary.RequestHadlers
{
    class CRUDGeneralRequestHandler
    {
        string _baseAddress;

        public CRUDGeneralRequestHandler(string baseAddress)
        {
            _baseAddress = baseAddress;
        }

        public async Task<HttpResponseMessage> Create(string controller, DataModel model)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                return await client.PostAsJsonAsync("api/" + controller + "/", model);
            }
        }


        public async Task<HttpResponseMessage> Get(string controller, string identificator)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseAddress);
                return await client.GetAsync("api/" + controller + "/" + identificator + "/");
            }
        }

        public async Task<HttpResponseMessage> Get(string controller, string identificator1, string identificator2)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseAddress);
                return await client.GetAsync("api/" + controller + "/" + identificator1 + "/" + identificator2 + "/");
            }
        }


        public async Task<HttpResponseMessage> Update(string controller, DataModel model)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                return await client.PutAsJsonAsync("api/" + controller + "/", model);
            }
        }


        public async Task<HttpResponseMessage> Delete(string controller, string identificator)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseAddress);
                return await client.DeleteAsync("api/" + controller + "/" + identificator + "/");
            }
        }

        public async Task<HttpResponseMessage> Delete(string controller, string identificator1, string identificator2)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseAddress);
                return await client.DeleteAsync
                    ("api/" + controller + "/" + identificator1 + "/" + identificator2 + "/");
            }
        }
    }
}
