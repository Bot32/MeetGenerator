using MeetGenerator.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebApiClientLibrary.Interfaces
{
    public interface IMeetingRequestHandler
    {
        Task<HttpResponseMessage> Create(Meeting meeting);
        Task<HttpResponseMessage> Get(Guid id);
        Task<HttpResponseMessage> Update(Meeting meeting);
        Task<HttpResponseMessage> Delete(Guid id);

        Task<HttpResponseMessage> GetAllMeetingsCreatedByUser(Guid id);

        Task<HttpResponseMessage> CreateInvitation(Invitation invitation);
        Task<HttpResponseMessage> CheckInvitation(Invitation invitation);
        Task<HttpResponseMessage> CancelInvitation(Invitation invitation);
    }
}
