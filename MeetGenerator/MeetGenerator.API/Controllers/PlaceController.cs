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
using System.Web.Http.Results;

namespace MeetGenerator.API.Controllers
{
    [Authorize]
    public class PlaceController : ApiController
    {
        IPlaceRepository _placeRepository;
        Logger _logger = LogManager.GetCurrentClassLogger();

        public PlaceController()
        {
            _placeRepository = new PlaceRepository
                (ConfigurationManager.ConnectionStrings["MeetGenDB"].ConnectionString);
        }

        public PlaceController(String connectionString)
        {
            _placeRepository = new PlaceRepository(connectionString);
        }

        public PlaceController(IPlaceRepository rep)
        {
            _placeRepository = rep;
        }

        [HttpPost]
        public IHttpActionResult Create(Place place)
        {
            Guid requestId = Guid.NewGuid();

            Log("Received create place POST HTTP-request.", place, requestId);

            if ((place.Address == null) | (place.Description == null))
            {
                Log("Send ErrorMessageResult(400) response to create place POST HTTP-request. " +
                    "Message: Inavalid model state.", requestId);
                return BadRequest("Invalid model state.");
            }

            place.Id = Guid.NewGuid();
            _placeRepository.CreatePlace(place);

            Log("Send CreatedNegotiatedContentResult<Place>(201) response to create place POST HTTP-request.",
                place, requestId);

            return Created("", place);
        }

        [HttpGet]
        public IHttpActionResult Get(Guid id)
        {
            Guid requestId = Guid.NewGuid();

            Log("Received get place GET HTTP-request. Attached place ID = " + id, requestId);

            Place place = _placeRepository.GetPlace(id);

            if (place == null)
            {
                Log("Send NotFoundResult(404) response to get place GET HTTP-request.", requestId);
                return NotFound();
            }

            Log("Send OkNegotiatedContentResult<Placw>(200) response to get place GET HTTP-request.",
                place, requestId);

            return Ok(place);
        }

        [HttpPut]
        public IHttpActionResult Update(Place place)
        {
            Guid requestId = Guid.NewGuid();

            Log("Received update place PUT HTTP-request.", place, requestId);

            if ((place.Address == null) | (place.Description == null))
            {
                Log("Send ErrorMessageResult(400) response to update place PUT HTTP-request. " +
                    "Message: Inavalid model state.", requestId);
                return BadRequest("Invalid model state.");
            }

            if (_placeRepository.GetPlace(place.Id) == null)
            {
                Log("Send NotFoundResult(404) response to update place PUT HTTP-request.", requestId);
                return NotFound();
            }

            _placeRepository.UpdatePlaceInfo(place);

            Log("Send CreatedNegotiatedContentResult<Place>(201) response to update place PUT HTTP-request.",
                place, requestId);

            return Created("", place);
        }

        [HttpDelete]
        public IHttpActionResult Delete(Guid id)
        {
            Guid requestId = Guid.NewGuid();

            Log("Received delete place DELETE HTTP-request. Attached place ID = " + id, requestId);

            if (_placeRepository.GetPlace(id) == null)
            {
                Log("Send NotFoundResult(404) response to delete place DELETE HTTP-request.", requestId);
                return NotFound();
            }

            _placeRepository.DeletePlace(id);

            Log("Send OkResult(200) response to delete place DELETE HTTP-request.", requestId);

            return Ok();
        }

        void Log(String logMessage, Guid requestId)
        {
            _logger.Info("{1} {0}", logMessage, requestId);
        }

        void Log(String logMessage, Place place, Guid requestId)
        {
            if (place != null)
            {
                _logger.Info("{3} {0} Attached place object: " +
                    "ID = {1}, Address = {2}.", logMessage, place.Id, place.Address, requestId);
            }
            else
            {
                _logger.Info("{1} Received {0} request. Attached place object is null.",
                    logMessage, requestId);
            }
        }
    }
}
