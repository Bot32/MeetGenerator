using MeetGenerator.API.DataValidators;
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
    public class MeetingController : ApiController
    {
        IMeetingRepository _meetRepository;
        IPlaceRepository _placeRepository;
        IUserRepository _userRepository;

        Logger _logger = LogManager.GetCurrentClassLogger();

        public MeetingController()
        {
            String connectionString = ConfigurationManager.ConnectionStrings["MeetGenDB"].ConnectionString;
            _meetRepository = new MeetingRepository(connectionString);
            _userRepository = new UserRepository(connectionString);
            _placeRepository = new PlaceRepository(connectionString);
        }

        public MeetingController(String connectionString)
        {
            _meetRepository = new MeetingRepository(connectionString);
            _userRepository = new UserRepository(connectionString);
            _placeRepository = new PlaceRepository(connectionString);
        }

        public MeetingController(IMeetingRepository meetingRepository, 
            IUserRepository userReository, IPlaceRepository placeRepository)
        {
            _meetRepository = meetingRepository;
            _userRepository = userReository;
            _placeRepository = placeRepository;
        }

        // POST: api/Meeting/Create
        [HttpPost]
        public IHttpActionResult Create(Meeting meeting)
        {
            Guid requestId = Guid.NewGuid();

            Log("Received create meeting POST HTTP-request.", meeting, requestId);

            List<string> errorList = MeetDataValidator.IsCompleteValidMeetingObject(meeting);

            if (errorList.Count != 0)
            {
                Log("Send ErrorMessageResult(400) response to create meeting POST HTTP-request. " +
                    "Message: Invalid model state.", requestId);
                return BadRequest("Invalid model state.");
            }

            if (_userRepository.GetUser(meeting.Owner.Id) == null)
            {
                Log("Send NotFoundWithMessageResult(404) response to create meeting POST HTTP-request." + 
                    "Message: Owner not found.", requestId);
                return new NotFoundWithMessageResult("Owner not found.");
            }

            if (_placeRepository.GetPlace(meeting.Place.Id) == null)
            {
                Log("Send NotFoundWithMessageResult(404) response to create meeting POST HTTP-request." +
                    "Message: Place not found.", requestId);
                return new NotFoundWithMessageResult("Place not found.");
            }

            meeting.Id = Guid.NewGuid();
            _meetRepository.CreateMeeting(meeting);

            Log("Send CreatedNegotiatedContentResult<Meeting>(201) response to create meeting POST HTTP-request.",
                meeting, requestId);

            return Created("", meeting);
        }

        // POST: api/Meeting/Create
        [HttpPost]
        public IHttpActionResult InviteUserToMeeting(Invitation invitation)
        {
            Guid requestId = Guid.NewGuid();

            Log("Received invite user to meeting POST HTTP-request. Meeting ID = " +
                invitation.MeetingID + ". User ID = " + invitation.UserID, requestId);

            User user = _userRepository.GetUser(invitation.UserID);
            Meeting meeting = _meetRepository.GetMeeting(invitation.MeetingID);

            if (user == null)
            {
                Log("Send NotFoundWithMessageResult(404) response to invite user to meeting POST HTTP-request." +
                    "Message: User not found.", requestId);
                return new NotFoundWithMessageResult("User not found.");
            }

            if (meeting == null)
            {
                Log("Send NotFoundWithMessageResult(404) response to invite user to meeting POST HTTP-request." +
                    "Message: Meeting not found.", requestId);
                return new NotFoundWithMessageResult("Meeting not found.");
            }

            if (meeting.InvitedPeople.ContainsKey(invitation.UserID))
            {
                Log("Send ErrorMessageResult(400) response to invite user to meeting POST HTTP-request. " +
                    "Message: User already invited.", requestId);
                return BadRequest("User already invited.");
            }

            _meetRepository.InviteUserToMeeting(invitation.UserID, invitation.MeetingID);

            Log("Send OkResult(200) response to invite user to meeting POST HTTP-request.", requestId);

            return Ok();
        }

        // GET: api/Meeting/Get
        [HttpGet]
        public IHttpActionResult Get(Guid id)
        {
            Guid requestId = Guid.NewGuid();

            Log("Received get meeting GET HTTP-request. Meeting ID = " + id, requestId);

            Meeting meeting = _meetRepository.GetMeeting(id);

            if (meeting == null)
            {
                Log("Send NotFoundResult(404) response to get meeting GET HTTP-request.", requestId);
                return NotFound();
            }

            Log("Send OkNegotiatedContentResult<User>(200) response to get meeting GET HTTP-request.",
                meeting, requestId);

            return Ok(meeting);
        }

        // PUT: api/Meeting/Update
        [HttpPut]
        public IHttpActionResult Update(Meeting meeting)
        {
            Guid requestId = Guid.NewGuid();

            Log("Received update meeting PUT HTTP-request.", meeting, requestId);

            List<string> errorList = MeetDataValidator.IsCompleteValidMeetingObject(meeting);

            if (errorList.Count != 0)
            {
                Log("Send ErrorMessageResult(400) response to update meeting PUT HTTP-request. " +
                    "Message: Invalid model state.", requestId);
                return BadRequest("Invalid model state.");
            }

            if (_meetRepository.GetMeeting(meeting.Id) == null)
            {
                Log("Send NotFoundResult(404) response to update meeting PUT HTTP-request.", requestId);
                return NotFound();
            }

            _meetRepository.UpdateMeetingInfo(meeting);

            Log("Send CreatedNegotiatedContentResult<User>(201) response to update meeting PUT HTTP-request.",
                meeting, requestId);

            return Created("", meeting);
        }

        // DELETE: api/Meeting/Delete
        [HttpDelete]
        public IHttpActionResult Delete(Guid id)
        {
            Guid requestId = Guid.NewGuid();

            Log("Received delete meeting DELETE HTTP-request. Meeting ID = " + id, requestId);

            Meeting meeting = _meetRepository.GetMeeting(id);

            if (meeting == null)
            {
                Log("Send NotFoundResult(404) response to delete meeting DELETE HTTP-request.", requestId);
                return NotFound();
            }

            _meetRepository.DeleteMeeting(meeting);

            Log("Send OkResult(200) response to delete meeting DELETE HTTP-request.", requestId);

            return Ok();
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
