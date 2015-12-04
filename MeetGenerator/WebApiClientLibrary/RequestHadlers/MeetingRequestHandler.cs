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
    public class MeetingRequestHandler : IMeetingRequestHandler
    {
        CRUDGeneralRequestHandler _crudHandler;
        const string _controller = "Meeting";

        public MeetingRequestHandler(string baseAddress)
        {
            _crudHandler = new CRUDGeneralRequestHandler(baseAddress);
        }

        public Task<HttpResponseMessage> Create(Meeting meeting)
        {
            return _crudHandler.Create(_controller, meeting);
        }

        public Task<HttpResponseMessage> Get(Guid id)
        {
            return _crudHandler.Get(_controller, id.ToString());
        }

        public Task<HttpResponseMessage> Update(Meeting meeting)
        {
            return _crudHandler.Update(_controller, meeting);
        }

        public Task<HttpResponseMessage> Delete(Guid id)
        {
            return _crudHandler.Delete(_controller, id.ToString());
        }

        public Task<HttpResponseMessage> GetAllMeetingsCreatedByUser(Guid id)
        {
            return _crudHandler.Get("Meetings", id.ToString());
        }

        public Task<HttpResponseMessage> CreateInvitation(Invitation invitation)
        {
            return _crudHandler.Create("Invitation", invitation);
        }

        public Task<HttpResponseMessage> CancelInvitation(Invitation invitation)
        {
            return _crudHandler.Delete("Invitation", invitation.MeetingID.ToString(), invitation.UserID.ToString());
        }

        public Task<HttpResponseMessage> CheckInvitation(Invitation invitation)
        {
            return _crudHandler.Get("Invitation", invitation.MeetingID.ToString(), invitation.UserID.ToString());
        }
    }
}
