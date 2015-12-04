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
    public class MeetingsController : ApiController
    {
        IMeetingRepository _meetRepository;
        IUserRepository _userRepository;

        Logger _logger = LogManager.GetCurrentClassLogger();

        public MeetingsController()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MeetGenDB"].ConnectionString;
            _meetRepository = new MeetingRepository(connectionString);
            _userRepository = new UserRepository(connectionString);
        }

        public MeetingsController(String connectionString)
        {
            _meetRepository = new MeetingRepository(connectionString);
            _userRepository = new UserRepository(connectionString);
        }

        public MeetingsController(IMeetingRepository meetingRepository, IUserRepository userReository)
        {
            _meetRepository = meetingRepository;
            _userRepository = userReository;
        }

        [HttpGet]
        public IHttpActionResult Get(Guid id)
        {
            Guid requestId = Guid.NewGuid();

            Log("Received get all meetings, created by user GET HTTP-request. User ID = " + id, requestId);

            if (_userRepository.GetUser(id) == null)
            {
                Log("Send NotFoundResult(404) response to get all meetings, created by user GET HTTP-request. User not found.", requestId);
                return NotFound();
            }

            List<Meeting> meetings = _meetRepository.GetAllMeetingsCreatedByUser(id);

            if (meetings.Count == 0)
            {
                Log("Send NotFoundResult(404) response to get all meetings, created by user GET HTTP-request.", requestId);
                return NotFound();
            }

            Log("Send OkNegotiatedContentResult<List<Meeting>>(200) response to get all meetings, created by user GET HTTP-request.",
                requestId);

            return Ok(meetings);
        }

        [HttpPost]
        public IHttpActionResult Create()
        {
            return new MethodNotAllowedResult("get");
        }

        [HttpPut]
        public IHttpActionResult Update()
        {
            return new MethodNotAllowedResult("get");
        }

        [HttpDelete]
        public IHttpActionResult Delete()
        {
            return new MethodNotAllowedResult("get");
        }

        void Log(String logMessage, Guid requestId)
        {
            _logger.Info("{1} {0}", logMessage, requestId);
        }
    }
}
