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
using System.Web.Http.Results;

namespace MeetGenerator.API.Controllers
{
    public class PlaceController : ApiController
    {
        IPlaceRepository _placeRepository;

        public PlaceController()
        {
            _placeRepository = new PlaceRepository
                (ConfigurationManager.ConnectionStrings["MeetGenDB"].ConnectionString);
        }

        public PlaceController(String connectionString)
        {
            _placeRepository = new PlaceRepository(connectionString);
        }

        // POST: api/User/Create
        [HttpPost]
        public IHttpActionResult Create(Place place)
        {
            if (place.Address == null)
                return BadRequest("Invalid model state.");

            place.Id = Guid.NewGuid();
            _placeRepository.CreatePlace(place);

            return Created("", place);
        }

        // GET: api/User/Get
        [HttpGet]
        public IHttpActionResult Get(Guid id)
        {
            Place place = _placeRepository.GetPlace(id);

            if (place == null)
                return NotFound();

            return Ok(place);
        }

        // PUT: api/User/Update
        [HttpPut]
        public IHttpActionResult Update(Place place)
        {
            if (place.Address == null)
                return BadRequest("Invalid model state.");

            if (_placeRepository.GetPlace(place.Id) == null)
                return NotFound();

            _placeRepository.UpdatePlaceInfo(place);

            return Created("", place);
        }

        // DELETE: api/User/Get
        [HttpDelete]
        public IHttpActionResult Delete(Guid id)
        {
            if (_placeRepository.GetPlace(id) == null)
                return NotFound();

            _placeRepository.DeletePlace(id);

            return Ok();
        }
    }
}
