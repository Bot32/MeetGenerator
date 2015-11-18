using MeetGenerator.Model.Models;
using MeetGenerator.Model.Repositories;
using MeetGenerator.Repository.SQL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace MeetGenerator.API.Controllers
{
    public class PlaceController : ApiController
    {
        IPlaceRepository repository = new PlaceRepository
    ("Data Source=KONSTANTIN-PC;Initial Catalog=MeetGenDB;Integrated Security=True;");
        //(ConfigurationManager.ConnectionStrings["MeetGenDB"].ConnectionString);  // Не работает wtf

        // POST: api/User/Create
        [HttpPost]
        public IHttpActionResult Create(Place place)
        {
            if (place.Address != null)
            {
                place.Id = Guid.NewGuid();
                repository.CreatePlace(place);
                return Created("", place);
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
            Place place = repository.GetPlace(id);
            if (place == null)
            {
                return NotFound();
            }
            return Ok(place);
        }

        // PUT: api/User/Update
        [HttpPut]
        public IHttpActionResult Update(Place place)
        {
            if (place.Address != null)
            {
                if (Get(place.Id) is OkNegotiatedContentResult<Place>)
                {
                    repository.UpdatePlaceInfo(place);
                    return Created("", place);
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

        // DELETE: api/User/Get
        [HttpDelete]
        public IHttpActionResult Delete(Guid id)
        {

            if (Get(id) is NotFoundResult)
            {
                return NotFound();
            }
            else
            {
                repository.DeletePlace(id);
                return Ok();
            }
        }

    }
}
