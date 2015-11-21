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

namespace MeetGenerator.API.Controllers
{
    public class UserController : ApiController
    {
        IUserRepository _userRepository;

        public UserController()
        {
            _userRepository = new UserRepository
                (ConfigurationManager.ConnectionStrings["MeetGenDB"].ConnectionString);
        }

        public UserController(String connectionString)
        {
            _userRepository = new UserRepository(connectionString);
        }

        // POST: api/User/Create
        [HttpPost]
        public IHttpActionResult Create(User user)
        {  
            List<string> errorList = UserDataValidator.IsCompleteValidUserObject(user);

            if (errorList.Count != 0)
                return BadRequest("Invalid model state.");

            if (_userRepository.GetUser(user.Email) != null)
                return BadRequest("Email is busy.");

            user.Id = Guid.NewGuid();
            _userRepository.CreateUser(user);

            return Created("", user);
        }

        // GET: api/User/Get
        [HttpGet]
        public IHttpActionResult Get(Guid id)
        {
            User user = _userRepository.GetUser(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // GET: api/User/Get
        [HttpGet]
        public IHttpActionResult Get(String email)
        {
            User user = _userRepository.GetUser(email);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // PUT: api/User/Update
        [HttpPut]
        public IHttpActionResult Update(User user)
        {
            List<string> errorList = UserDataValidator.IsCompleteValidUserObject(user);

            if (errorList.Count != 0)
                return BadRequest("Invalid model state.");

            if (_userRepository.GetUser(user.Id) == null)
                return NotFound();

            _userRepository.UpdateUser(user);

            return Created("", user);
        }

        // DELETE: api/User/Delete
        [HttpDelete]
        public IHttpActionResult Delete(Guid id)
        {
            if (_userRepository.GetUser(id) == null)
                return NotFound();

            _userRepository.DeleteUser(id);
        
            return Ok();
        }

    }
}
