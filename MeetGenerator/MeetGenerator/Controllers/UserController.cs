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

namespace MeetGenerator.Controllers
{
    public class UserController : ApiController
    {
        readonly IUserRepository repository = new UserRepository
            ("Data Source=KONSTANTIN-PC;Initial Catalog=MeetGenDB;Integrated Security=True;");
        //(ConfigurationManager.ConnectionStrings["MeetGenDB"].ConnectionString);  // Не работает wtf

        // POST: api/User/Create
        [HttpPost]
        public HttpResponseMessage Create(User user)
        {
            user.Id = Guid.NewGuid();
            HttpResponseMessage response = new HttpResponseMessage();
            string checkResult = UserDataValidator.IsCompleteValidUserObject(user);

            if (checkResult == "OK")
            {
                repository.CreateUser(user);
                response.StatusCode = HttpStatusCode.Created;
                return response;
            }
            else
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.ReasonPhrase = checkResult;
                return response;
            }
        }

        // POST: api/User/Get
        [HttpPost]
        public User Get(Guid id)
        {
            User user = repository.GetUser(id);
            return user;
        }


    }
}
