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
        IUserRepository repository = new UserRepository
            ("Data Source=KONSTANTIN-PC;Initial Catalog=MeetGenDB;Integrated Security=True;");
        //(ConfigurationManager.ConnectionStrings["MeetGenDB"].ConnectionString);  // Не работает wtf

        // POST: api/User/Create
        [HttpPost]
        public IHttpActionResult Create(User user)
        {  
            List<string> errorList = UserDataValidator.IsCompleteValidUserObject(user);

            if (errorList.Count == 0)
            {
                if (Get(user.Email) is NotFoundResult)
                {
                    user.Id = Guid.NewGuid();
                    repository.CreateUser(user);
                    return Created("", user);
                }
                else
                {
                    return BadRequest("Email is busy.");
                }
            }
            else
            {
                return BadRequest("Invalid model state.");
            }
        }

        // GET: api/User/Get
        [HttpGet]
        public IHttpActionResult Get(Guid id)
        {
            User user = repository.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // GET: api/User/Get
        public IHttpActionResult Get(String email)
        {
            User user = repository.GetUser(email);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // PUT: api/User/Update
        [HttpPut]
        public IHttpActionResult Update(User user)
        {
            List<string> errorList = UserDataValidator.IsCompleteValidUserObject(user);

            if (errorList.Count == 0)
            {
                if (Get(user.Id) is OkNegotiatedContentResult<User>)
                {
                    repository.UpdateUser(user);
                    return Created("", user);
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
            if (Get(id) is NotFoundResult)
            {
                return NotFound();
            }
            else
            {
                repository.DeleteUser(id);
                return Ok();
            }
        }

    }
}
