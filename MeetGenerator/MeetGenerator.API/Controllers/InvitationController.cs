using MeetGenerator.API.HttpActionResults;
using MeetGenerator.Model.Models;
using MeetGenerator.Model.Repositories;
using MeetGenerator.Repository.SQL.Repositories;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MeetGenerator.API.Controllers
{
    public class InvitationController : ApiController
    {
        IMeetingRepository _meetRepository;
        IUserRepository _userRepository;
        IInvitationRepository _invitationRepository;

        Logger _logger = LogManager.GetCurrentClassLogger();

        public InvitationController()
        {
            String connectionString = ConfigurationManager.ConnectionStrings["MeetGenDB"].ConnectionString;
            _meetRepository = new MeetingRepository(connectionString);
            _userRepository = new UserRepository(connectionString);
            _invitationRepository = new InvitationRepository(connectionString);
        }

        public InvitationController(String connectionString)
        {
            _meetRepository = new MeetingRepository(connectionString);
            _userRepository = new UserRepository(connectionString);
            _invitationRepository = new InvitationRepository(connectionString);    
        }

        public InvitationController(IMeetingRepository meetingRepository, IUserRepository userReository,
            IInvitationRepository invitationRepository)
        {
            _meetRepository = meetingRepository;
            _userRepository = userReository;
            _invitationRepository = invitationRepository;
        }

        [HttpPost]
        public IHttpActionResult Create(Invitation invitation)
        {
            

            Guid requestId = Guid.NewGuid();

            Log("Received create invitation user to meeting POST HTTP-request. Meeting ID = " +
                invitation.MeetingID + ". User ID = " + invitation.UserID, requestId);

            User user = _userRepository.GetUser(invitation.UserID);
            Meeting meeting = _meetRepository.GetMeeting(invitation.MeetingID);

            if (user == null)
            {
                Log("Send NotFoundWithMessageResult(404) response to create invitation user to meeting POST HTTP-request." +
                    "Message: User not found.", requestId);
                return new NotFoundWithMessageResult("User not found.");
            }

            if (meeting == null)
            {
                Log("Send NotFoundWithMessageResult(404) response to create invitation user to meeting POST HTTP-request." +
                    "Message: Meeting not found.", requestId);
                return new NotFoundWithMessageResult("Meeting not found.");
            }

            if (meeting.InvitedPeople.ContainsKey(invitation.UserID))
            {
                Log("Send ErrorMessageResult(400) response to create invitation user to meeting POST HTTP-request. " +
                    "Message: User already invited.", requestId);
                return BadRequest("User already invited.");
            }

            _invitationRepository.Create(invitation);

            Log("Send OkResult(200) response to create invitation user to meeting POST HTTP-request.", requestId);

            return Ok();
        }

        [HttpGet]
        public IHttpActionResult Get(Guid MeetingID, Guid UserID)
        {
            Guid requestId = Guid.NewGuid();
            Invitation invitation = new Invitation();
            invitation.MeetingID = MeetingID;
            invitation.UserID = UserID;

            Log("Received get invitation GET HTTP-request. Meeting ID = " +
                invitation.MeetingID + ". User ID = " + invitation.UserID, requestId);

            if (_invitationRepository.IsExist(invitation))
            {
                Log("Send OkREsult(200) response to get invitation GET HTTP-request.", requestId);
                return Ok();
            }

            Log("Send NotFound(200) response to get invitation user to meeting GET HTTP-request.", requestId);
            return new NotFoundWithMessageResult("Invitation not found.");
        }

        [HttpDelete]
        public IHttpActionResult Delete(Guid MeetingID, Guid UserID)
        {
            Guid requestId = Guid.NewGuid();
            Invitation invitation = new Invitation();
            invitation.MeetingID = MeetingID;
            invitation.UserID = UserID;

            Log("Received delete invitation DELETE HTTP-request. Meeting ID = " +
                invitation.MeetingID + ". User ID = " + invitation.UserID, requestId);

            if (_invitationRepository.IsExist(invitation))
            {
                _invitationRepository.Delete(invitation);
                Log("Send OkREsult(200) response to delete invitation DELETE HTTP-request.", requestId);
                return Ok();
            }

            Log("Send NotFoundWithMessageResult(404) response to delete invitation DELETE HTTP-request." +
                "Message: Invitation not found.", requestId);
            return new NotFoundWithMessageResult("Invitation not found.");
        }

        [HttpPut]
        public IHttpActionResult Update(Guid MeetingID, Guid UserID)
        {
            return new MethodNotAllowedResult("post,get,delete");
        }

        void Log(String logMessage, Guid requestId)
        {
            _logger.Info("{1} {0}", logMessage, requestId);
        }

        void Log(String logMessage, Invitation invitation, Guid requestId)
        {
            if (invitation != null)
            {
                _logger.Info("{3} {0} Attached invitation object: " +
                    "MeetingID = {1}, UserID = {2}.",
                    logMessage, invitation.MeetingID, invitation.UserID, requestId);
            }
            else
            {
                _logger.Info("{1} Received {0} request. Attached invitation object is null.",
                    logMessage, requestId);
            }
        }
    }
}
