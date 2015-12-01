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

        Logger _logger = LogManager.GetCurrentClassLogger();

        public InvitationController()
        {
            String connectionString = ConfigurationManager.ConnectionStrings["MeetGenDB"].ConnectionString;
            _meetRepository = new MeetingRepository(connectionString);
            _userRepository = new UserRepository(connectionString);
        }

        public InvitationController(String connectionString)
        {
            _meetRepository = new MeetingRepository(connectionString);
            _userRepository = new UserRepository(connectionString);
        }

        public InvitationController(IMeetingRepository meetingRepository, IUserRepository userReository)
        {
            _meetRepository = meetingRepository;
            _userRepository = userReository;
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

            _meetRepository.CreateInvitation(invitation.UserID, invitation.MeetingID);

            Log("Send OkResult(200) response to create invitation user to meeting POST HTTP-request.", requestId);

            return Ok();
        }

        [HttpGet]
        public IHttpActionResult Get (Invitation invitation)
        {
            Guid requestId = Guid.NewGuid();

            Log("Received get invitation GET HTTP-request. Meeting ID = " +
                invitation.MeetingID + ". User ID = " + invitation.UserID, requestId);

            User user = _userRepository.GetUser(invitation.UserID);
            Meeting meeting = _meetRepository.GetMeeting(invitation.MeetingID);

            if (meeting == null)
            {
                Log("Send NotFoundWithMessageResult(404) response to get invitation GET HTTP-request." +
                    "Message: Meeting not found.", requestId);
                return new NotFoundWithMessageResult("Meeting not found.");
            }

            if (meeting.InvitedPeople.ContainsKey(invitation.UserID))
            {
                Log("Send OkREsult(200) response to get invitation GET HTTP-request.", requestId);
                return Ok();
            }

            Log("Send NotFound(200) response to invite user to meeting GET HTTP-request.", requestId);
            return NotFound();
        }

        [HttpDelete]
        public IHttpActionResult Delete(Invitation invitation)
        {
            Guid requestId = Guid.NewGuid();

            Log("Received delete invitation DELETE HTTP-request. Meeting ID = " +
                invitation.MeetingID + ". User ID = " + invitation.UserID, requestId);

            User user = _userRepository.GetUser(invitation.UserID);
            Meeting meeting = _meetRepository.GetMeeting(invitation.MeetingID);

            if (meeting == null)
            {
                Log("Send NotFoundWithMessageResult(404) response to delete invitation DELETE HTTP-request." +
                    "Message: Meeting not found.", requestId);
                return new NotFoundWithMessageResult("Meeting not found.");
            }

            if (meeting.InvitedPeople.ContainsKey(invitation.UserID))
            {
                _meetRepository.DeleteInvitation(invitation.UserID, invitation.MeetingID);
                Log("Send OkREsult(200) response to delete invitation DELETE HTTP-request.", requestId);
                return Ok();
            }

            Log("Send NotFoundWithMessageResult(404) response to delete invitation DELETE HTTP-request." +
                "Message: Invitation not found.", requestId);
            return new NotFoundWithMessageResult("Invitation not found.");
        }

        void Log(String logMessage, Guid requestId)
        {
            _logger.Info("{1} {0}", logMessage, requestId);
        }

        void Log(String logMessage, Meeting meeting, Guid requestId)
        {
            if (meeting != null)
            {
                _logger.Info("{5} {0} Attached meeting object: " +
                    "ID = {1}, Title = {2}, Owner.ID = {3}, Place.ID = {4}.",
                    logMessage, meeting.Id, meeting.Title,
                    ((meeting.Owner == null) ? "null" : meeting.Owner.Id.ToString()),
                    meeting.Place.Id, requestId);
            }
            else
            {
                _logger.Info("{1} Received {0} request. Attached meeting object is null.",
                    logMessage, requestId);
            }
        }
    }
}
