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
using MeetGenerator.DataValidators;
using System.Web.Http.Description;
using System.Web.Http.Results;
using NLog;
using System.Threading.Tasks;

namespace MeetGenerator.API.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {
        IUserRepository _userRepository;
        Logger _logger = LogManager.GetCurrentClassLogger();

        public UserController()
        {
            _userRepository = new UserRepository
                (ConfigurationManager.ConnectionStrings["MeetGenDB"].ConnectionString);
        }

        public UserController(String connectionString)
        {
            _userRepository = new UserRepository(connectionString);
        }

        public UserController(IUserRepository repository)
        {
            _userRepository = repository;
        }

        [HttpPost]
        public IHttpActionResult Create(User user)
        {
            Guid requestId = Guid.NewGuid();

            Log("Received create user POST HTTP-request.", user, requestId);

            List<string> errorList = UserDataValidator.IsCompleteValidUserObject(user);

            if (errorList.Count != 0)
            {
                Log("Send ErrorMessageResult(400) response to create user POST HTTP-request. " +
                    "Message: Inavalid model state.", requestId);
                return BadRequest("Invalid model state.");
            }

            if (_userRepository.GetUser(user.Email) != null)
            {
                Log("Send ErrorMessageResult(400) response to create user POST HTTP-request. " +
                    "Message: Email is busy.", requestId);
                return BadRequest("Email is busy.");
            }

            user.Id = Guid.NewGuid();
            _userRepository.CreateUser(user);

            Log("Send CreatedNegotiatedContentResult<User>(201) response to create user POST HTTP-request.", 
                user, requestId);

            return Created("", user);
        }

        [HttpGet]
        public IHttpActionResult Get(String id)
        {
            Guid requestId = Guid.NewGuid();
            Guid userId;
            User user;

            Log("Received get user GET HTTP-request. Attached user identificator = " + id, requestId);

            if (Guid.TryParse(id, out userId))
                user = _userRepository.GetUser(userId);
            else
                user = _userRepository.GetUser(id);

            if (user == null)
            {
                Log("Send NotFoundResult(404) response to get user GET HTTP-request.", requestId);
                return NotFound();
            }

            Log("Send OkNegotiatedContentResult<User>(200) response to get user GET HTTP-request.",
                user, requestId);

            return Ok(user);
        }

        [HttpPut]
        public IHttpActionResult Update(User user)
        {
            Guid requestId = Guid.NewGuid();

            Log("Received update user PUT HTTP-request.", user, requestId);

            List<string> errorList = UserDataValidator.IsCompleteValidUserObject(user);

            if (errorList.Count != 0)
            {
                Log("Send ErrorMessageResult(400) response to update user PUT HTTP-request. " +
                     "Message: Invalid model state.", requestId);
                return BadRequest("Invalid model state.");
            }

            if (_userRepository.GetUser(user.Id) == null)
            {
                Log("Send NotFoundResult(404) response to update user PUT HTTP-request.", requestId);
                return NotFound();
            }

            _userRepository.UpdateUser(user);

            Log("Send CreatedNegotiatedContentResult<User>(201) response to update user PUT HTTP-request.", 
                user, requestId);

            return Created("", user);
        }

        [HttpDelete]
        public IHttpActionResult Delete(Guid id)
        {
            Guid requestId = Guid.NewGuid();

            Log("Received delete user DELETE HTTP-request. Attached user ID = " + id, requestId);

            if (_userRepository.GetUser(id) == null)
            {
                Log("Send NotFoundResult(404) response to delete user DELETE HTTP-request.", requestId);
                return NotFound();
            }

            _userRepository.DeleteUser(id);

            Log("Send OkResult(200) response to delete user DELETE HTTP-request.", requestId);
            return Ok();
        }

        void Log(String logMessage, Guid requestId)
        {
            _logger.Info("{1} {0}", logMessage, requestId);
        }

        void Log(String logMessage, User user, Guid requestId)
        {
            if (user != null)
            {
                _logger.Info("{5} {0} Attached user object: " +
                    "ID = {1}, Email = {2}, First name = {3}, Last name = {4}.",
                    logMessage, user.Id, user.Email, user.FirstName, user.LastName, requestId);
            }
            else
            {
                _logger.Info("{1} Received {0} request. Attached user object is null.", 
                    logMessage, requestId);
            }
        }

    }
}
