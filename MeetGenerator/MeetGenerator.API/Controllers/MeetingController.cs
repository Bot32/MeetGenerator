using MeetGenerator.API.HttpActionResults;
using MeetGenerator.Model.Models;
using MeetGenerator.Model.Repositories;
using MeetGenerator.Repository.SQL.Repositories;
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

        // POST: api/User/Create
        [HttpPost]
        public IHttpActionResult Create(Meeting meeting)
        {
            if ((meeting.Owner == null) | (meeting.Place == null))
                return BadRequest("Invalid model state.");

            if (_userRepository.GetUser(meeting.Owner.Id) == null)
                return new NotFoundWithErrorResult("Owner not found.");

            if (_placeRepository.GetPlace(meeting.Place.Id) == null)
                return new NotFoundWithErrorResult("Place not found.");

            meeting.Id = Guid.NewGuid();
            _meetRepository.CreateMeeting(meeting);

            return Created("", meeting);
        }

        // POST: api/User/Create
        [HttpPost]
        public IHttpActionResult InviteUserToMeeting(Guid userId, Guid meetingId)
        {
            User user = _userRepository.GetUser(userId);
            Meeting meeting = _meetRepository.GetMeeting(meetingId);

            if (user == null)
                return new NotFoundWithErrorResult("User not found.");
            if (meeting == null)
                return new NotFoundWithErrorResult("Meeting not found.");
            if (meeting.InvitedPeople.ContainsKey(userId))
                return BadRequest("User already invited.");

            _meetRepository.InviteUserToMeeting(userId, meetingId);
            return Ok();
        }

        // GET: api/User/Get
        [HttpGet]
        public IHttpActionResult Get(Guid id)
        {
            Meeting meeting = _meetRepository.GetMeeting(id);

            if (meeting == null) return NotFound();

            return Ok(meeting);
        }

        // PUT: api/User/Update
        [HttpPut]
        public IHttpActionResult Update(Meeting meeting)
        {
            if ((meeting.Owner == null) | (meeting.Place == null))
                return BadRequest("Invalid model state.");

            if (_meetRepository.GetMeeting(meeting.Id) == null)
                return NotFound();

            _meetRepository.UpdateMeetingInfo(meeting);

            return Created("", meeting);
        }

        // DELETE: api/User/Delete
        [HttpDelete]
        public IHttpActionResult Delete(Guid id)
        {
            Meeting meeting = _meetRepository.GetMeeting(id);

            if (meeting == null)
                return NotFound();

            _meetRepository.DeleteMeeting(meeting);
            return Ok();
        }
    }
}
