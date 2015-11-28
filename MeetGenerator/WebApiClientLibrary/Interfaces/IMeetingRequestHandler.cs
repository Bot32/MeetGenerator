using MeetGenerator.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebApiClientLibrary.Interfaces
{
    interface IMeetingRequestHandler
    {
        Task<HttpResponseMessage> Create(Meeting meeting);
        Task<HttpResponseMessage> Get(Guid id);
        Task<HttpResponseMessage> GetAllMeetingsCreatedByUser(Guid id);
        Task<HttpResponseMessage> Invite(Guid meetingId, Guid userId);
        Task<HttpResponseMessage> Update(Meeting meeting);
        Task<HttpResponseMessage> Delete(Guid id);
    }
}
