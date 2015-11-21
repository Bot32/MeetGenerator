﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MeetGenerator.API.Controllers;
using MeetGenerator.Model.Models;
using System.Web.Http;
using System.Web.Http.Results;
using MeetGenerator.Tests.Properties;

namespace MeetGenerator.Tests.ControllerTests
{
    [TestClass]
    public class PlaceControllerTest
    {
        [TestMethod]
        public void Create_ShouldReturnCreated()
        {
            //arrange
            var placeController = new PlaceController(Resources.ConnectionString);
            Place place = TestDataHelper.GeneratePlace();

            //act
            IHttpActionResult response = placeController.Create(place);

            //assert
            Assert.IsTrue(response is CreatedNegotiatedContentResult<Place>);
        }

        [TestMethod]
        public void Create_WithNullField_ShouldReturnBadRequest()
        {
            //arrange
            var placeController = new PlaceController(Resources.ConnectionString);
            Place place = TestDataHelper.GeneratePlace();
            place.Address = null;

            //act
            IHttpActionResult response = placeController.Create(place);

            //assert
            Console.WriteLine(response);
            Assert.IsTrue(response is BadRequestErrorMessageResult);
        }

        [TestMethod]
        public void Get_ById_ShouldReturnOk()
        {
            //arrange
            var placeController = new PlaceController(Resources.ConnectionString);
            Place place = TestDataHelper.GeneratePlace();

            //act
            placeController.Create(place);
            IHttpActionResult response = placeController.Get(place.Id);

            //assert
            Assert.IsTrue(response is OkNegotiatedContentResult<Place>);
        }

        [TestMethod]
        public void Get_NonExistPlaceById_ShouldReturnNotFound()
        {
            //arrange
            var placeController = new PlaceController(Resources.ConnectionString);
            Place place = TestDataHelper.GeneratePlace();

            //act
            IHttpActionResult response = placeController.Get(place.Id);

            //assert
            Assert.IsTrue(response is NotFoundResult);
        }

        [TestMethod]
        public void Update_ShouldReturnCreated()
        {
            //arrange
            var placeController = new PlaceController(Resources.ConnectionString);
            Place place = TestDataHelper.GeneratePlace();

            //act
            IHttpActionResult response1 = placeController.Create(place);
            place.Address = "vasiliy_home";
            place.Description = "3331";
            IHttpActionResult response2 = placeController.Update(place);

            //assert
            Assert.IsTrue((response1 is CreatedNegotiatedContentResult<Place>) &
                          (response2 is CreatedNegotiatedContentResult<Place>));
        }

        [TestMethod]
        public void Update_WithNullField_ShouldReturnBadRequest()
        {
            //arrange
            var placeController = new PlaceController(Resources.ConnectionString);
            Place place = TestDataHelper.GeneratePlace();
            place.Address = null;

            //act
            IHttpActionResult response = placeController.Update(place);

            //assert
            Assert.IsTrue(response is BadRequestErrorMessageResult);
        }

        [TestMethod]
        public void Update_WithNonExistId_ShouldReturnNotFound()
        {
            //arrange
            var placeController = new PlaceController(Resources.ConnectionString);
            Place place = TestDataHelper.GeneratePlace();

            //act
            IHttpActionResult response = placeController.Update(place);

            //assert
            Assert.IsTrue(response is NotFoundResult);
        }

        [TestMethod]
        public void Delete_ShouldReturnOk()
        {
            //arrange
            var placeController = new PlaceController(Resources.ConnectionString);
            Place place = TestDataHelper.GeneratePlace();

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
            var placeController = new PlaceController(Resources.ConnectionString);
            Place place = TestDataHelper.GeneratePlace();

            //act
            IHttpActionResult response = placeController.Delete(place.Id);

            //assert
            Console.WriteLine(response);
            Assert.IsTrue(response is NotFoundResult);
        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
            TestDataHelper.ClearDB();
        }
    }
}
