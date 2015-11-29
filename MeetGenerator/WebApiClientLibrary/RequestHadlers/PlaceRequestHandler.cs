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
    class PlaceRequestHandler : IPlaceRequestHandler
    {
        string baseAddress;

        public PlaceRequestHandler(string baseAddress)
        {
            this.baseAddress = baseAddress;
        }

        public async Task<HttpResponseMessage> Create(Place place)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                return await client.PostAsJsonAsync("api/Place/Create", place);
            }
        }

        public async Task<HttpResponseMessage> Get(Guid id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                return await client.GetAsync("api/Place/Get?id=" + id);
            }
        }

        public async Task<HttpResponseMessage> Update(Place place)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                return await client.PutAsJsonAsync("api/Place/Update", place);
            }
        }

        public async Task<HttpResponseMessage> Delete(Guid id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress); ;
                return await client.DeleteAsync("api/Place/Delete?id=" + id);
            }
        }
    }
}
