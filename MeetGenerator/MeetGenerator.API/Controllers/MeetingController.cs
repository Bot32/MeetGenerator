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
        IMeetingRepository meetRepository = new MeetingRepository
            ("Data Source=KONSTANTIN-PC;Initial Catalog=MeetGenDB;Integrated Security=True;");
            //(ConfigurationManager.ConnectionStrings["MeetGenDB"].ConnectionString);
        IPlaceRepository placeRepository = new PlaceRepository
            ("Data Source=KONSTANTIN-PC;Initial Catalog=MeetGenDB;Integrated Security=True;");
            //(ConfigurationManager.ConnectionStrings["MeetGenDB"].ConnectionString);
        IUserRepository userRepository = new UserRepository
            ("Data Source=KONSTANTIN-PC;Initial Catalog=MeetGenDB;Integrated Security=True;");
        //(ConfigurationManager.ConnectionStrings["MeetGenDB"].ConnectionString);

        // POST: api/User/Create
        [HttpPost]
        public IHttpActionResult Create(Meeting meeting)
        {
            if ((meeting.Owner != null) & (meeting.Place != null))  //Должна быть полная проверка
            {
                if (userRepository.GetUser(meeting.Owner.Id) != null)
                {
                    if (placeRepository.GetPlace(meeting.Place.Id) != null)
                    {
                        meeting.Id = Guid.NewGuid();
                        meetRepository.CreateMeeting(meeting);
                        return Created("", meeting);
                    }
                    else
                    {
                        return new NotFoundWithErrorResult("Place not found.");
                    }
                }
                else
                {
                    return new NotFoundWithErrorResult("Owner not found.");
                }
            }
            else
            {
                return BadRequest("Invalid model state.");
            }
        }

        // POST: api/User/Create
        [HttpPost]
        public IHttpActionResult InviteUserToMeeting(Guid userId, Guid meetingId)
        {
            User user = userRepository.GetUser(userId);
            Meeting meeting = meetRepository.GetMeeting(meetingId);

            if (user != null)
            {
                if (meeting != null)
                {
                    if (!meeting.InvitedPeople.ContainsKey(userId))
                    {
                        meetRepository.InviteUserToMeeting(userId, meetingId);
                        return Ok();
                    }
                    else
                    {
                        return BadRequest("User already invited.");
                    }
                }
                else
                {
                    return new NotFoundWithErrorResult("Meeting not found.");
                } 
            }
            else
            {
                return new NotFoundWithErrorResult("User not found.");
            }
        }

        // GET: api/User/Get
        [HttpGet]
        public IHttpActionResult Get(Guid id)
        {
            Meeting meeting = meetRepository.GetMeeting(id);
            if (meeting != null)
            {
                return Ok(meeting);
            }
            else
            {
                return NotFound();
            }
        }

        // PUT: api/User/Update
        [HttpPut]
        public IHttpActionResult Update(Meeting meeting)
        {
            if ((meeting.Owner != null) & (meeting.Place != null))
            {
                if (meetRepository.GetMeeting(meeting.Id) != null)
                {
                    meetRepository.UpdateMeetingInfo(meeting);
                    return Created("", meeting);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest("Invalid model state.");
            }
        }

        // DELETE: api/User/Delete
        [HttpDelete]
        public IHttpActionResult Delete(Guid id)
        {
            Meeting meeting = meetRepository.GetMeeting(id);
            if (meeting != null)
            {
                meetRepository.DeleteMeeting(meeting);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
