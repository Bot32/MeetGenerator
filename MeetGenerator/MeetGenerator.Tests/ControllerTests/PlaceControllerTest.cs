using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Http.Results;
using System.Web.Http;
using MeetGenerator.Model.Models;
using MeetGenerator.API.Controllers;

namespace MeetGenerator.Tests.ControllerTests
{
    [TestClass]
    public class PlaceControllerTest
    {
        [TestMethod]
        public void Create_ShouldReturnCreated()
        {
            //arrange
            Place place = TestDataHelper.GeneratePlace();
            var placeController = new PlaceController(TestDataHelper.GetIPlaceRepositoryMock(place));

            //act
            IHttpActionResult response = placeController.Create(place);

            //assert
            Assert.IsTrue(response is CreatedNegotiatedContentResult<Place>);
        }

        [TestMethod]
        public void Create_WithNullField_ShouldReturnBadRequest()
        {
            //arrange
            Place place = TestDataHelper.GeneratePlace();
            place.Address = null;
            var placeController = new PlaceController(TestDataHelper.GetIPlaceRepositoryMock(place));

            //act
            IHttpActionResult response = placeController.Create(place);

            //assert
            Assert.IsTrue(response is BadRequestErrorMessageResult);
        }

        [TestMethod]
        public void Get_ById_ShouldReturnOk()
        {
            //arrange
            Place place = TestDataHelper.GeneratePlace();
            var placeController = new PlaceController(TestDataHelper.GetIPlaceRepositoryMock(place));

            //act
            IHttpActionResult response = placeController.Get(place.Id);

            //assert
            Assert.IsTrue(response is OkNegotiatedContentResult<Place>);
        }

        [TestMethod]
        public void Get_NonExistPlaceById_ShouldReturnNotFound()
        {
            //arrange
            Place place = TestDataHelper.GeneratePlace();
            var placeController = new PlaceController(TestDataHelper.GetIPlaceRepositoryMock(null));

            //act
            IHttpActionResult response = placeController.Get(place.Id);

            //assert
            Assert.IsTrue(response is NotFoundResult);
        }

        [TestMethod]
        public void Update_ShouldReturnCreated()
        {
            //arrange
            Place place = TestDataHelper.GeneratePlace();
            var placeController = new PlaceController(TestDataHelper.GetIPlaceRepositoryMock(place));

            //act
            IHttpActionResult response = placeController.Update(place);

            //assert
            Assert.IsTrue(response is CreatedNegotiatedContentResult<Place>);
        }

        [TestMethod]
        public void Update_WithNullField_ShouldReturnBadRequest()
        {
            //arrange
            Place place = TestDataHelper.GeneratePlace();
            place.Address = null;
            var placeController = new PlaceController(TestDataHelper.GetIPlaceRepositoryMock(place));

            //act
            IHttpActionResult response = placeController.Update(place);

            //assert
            Assert.IsTrue(response is BadRequestErrorMessageResult);
        }

        [TestMethod]
        public void Update_WithNonExistId_ShouldReturnNotFound()
        {
            //arrange
            Place place = TestDataHelper.GeneratePlace();
            var placeController = new PlaceController(TestDataHelper.GetIPlaceRepositoryMock(null));

            //act
            IHttpActionResult response = placeController.Update(place);

            //assert
            Assert.IsTrue(response is NotFoundResult);
        }

        [TestMethod]
        public void Delete_ShouldReturnOk()
        {
            //arrange
            Place place = TestDataHelper.GeneratePlace();
            var placeController = new PlaceController(TestDataHelper.GetIPlaceRepositoryMock(place));

            //act
            placeController.Create(place);
            IHttpActionResult response = placeController.Delete(place.Id);

            //assert
            Assert.IsTrue(response is OkResult);
        }

        [TestMethod]
        public void Delete_NonExistPlace_ShouldReturnNotFound()
        {
            //arrange
            Place place = TestDataHelper.GeneratePlace();
            var placeController = new PlaceController(TestDataHelper.GetIPlaceRepositoryMock(null));

            //act
            IHttpActionResult response = placeController.Delete(place.Id);

            //assert
            Console.WriteLine(response);
            Assert.IsTrue(response is NotFoundResult);
        }
    }
}
