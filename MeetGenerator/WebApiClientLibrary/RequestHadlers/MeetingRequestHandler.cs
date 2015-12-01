﻿using System;
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
    public class MeetingRequestHandler : IMeetingRequestHandler
    {
        string baseAddress;

        public MeetingRequestHandler(string baseAddress)
        {
            this.baseAddress = baseAddress;
        }

        public async Task<HttpResponseMessage> Create(Meeting meeting)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                return await client.PostAsJsonAsync("api/Meeting/Create", meeting);
            }
        }

        public async Task<HttpResponseMessage> Delete(Guid id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress); ;
                return await client.DeleteAsync("api/Meeting/Delete?id=" + id);
            }
        }

        public async Task<HttpResponseMessage> GetAllMeetingsCreatedByUser(Guid id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                return await client.GetAsync("api/Meetings/Get?userId=" + id);
            }
        }

        public async Task<HttpResponseMessage> Get(Guid id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                return await client.GetAsync("api/Meeting/Get?id=" + id);
            }
        }

        public async Task<HttpResponseMessage> Invite(Guid meetingId, Guid userId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                return await client.PostAsync("api/Meeting/Get?userId=" + userId + "&" + 
                    "meetingId=" + meetingId, new StringContent(""));
            }
        }

        public async Task<HttpResponseMessage> Update(Meeting meeting)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                return await client.PutAsJsonAsync("api/Meeting/Update", meeting);
            }
        }
    }
}