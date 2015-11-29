using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MeetGenerator.API.Controllers;
using System.Web.Http.Results;
using System.Web.Http;
using MeetGenerator.Model.Models;

namespace MeetGenerator.Tests.ControllerTests
{
    [TestClass]
    public class UserControllerTest
    {
        [TestMethod]
        public void Create_ShouldReturnCreated()
        {
            //arrange
            User user = TestDataHelper.GenerateUser();
            var userController = new UserController(TestDataHelper.GetIUserRepositoryMock(null));

            //act
            IHttpActionResult response = userController.Create(user);

            //assert
            Assert.IsTrue(response is CreatedNegotiatedContentResult<User>);
        }

        [TestMethod]
        public void Create_WithNullField_ShouldReturnBadRequest()
        {
            //arrange
            User user = TestDataHelper.GenerateUser();
            user.FirstName = null;
            user.Email = null;
            var userController = new UserController(TestDataHelper.GetIUserRepositoryMock(user));

            //act
            IHttpActionResult response = userController.Create(user);

            //assert
            Assert.IsTrue(response is BadRequestErrorMessageResult);
        }

        [TestMethod]
        public void Create_WithNullUserObject_ShouldReturnBadRequest()
        {
            //arrange
            User user = null;
            var userController = new UserController(TestDataHelper.GetIUserRepositoryMock(user));

            //act
            IHttpActionResult response = userController.Create(user);

            //assert
            Assert.IsTrue(response is BadRequestErrorMessageResult);
        }

        [TestMethod]
        public void Create_WithExistEmail_ShouldReturnBadRequest()
        {
            //arrange
            User user = TestDataHelper.GenerateUser();
            var userController = new UserController(TestDataHelper.GetIUserRepositoryMock(user));

            //act
            IHttpActionResult response = userController.Create(user);

            //assert
            Assert.IsTrue(response is BadRequestErrorMessageResult);
        }

        [TestMethod]
        public void Get_ByEmail_ShouldReturnOk()
        {
            //arrange
            User user = TestDataHelper.GenerateUser();
            var userController = new UserController(TestDataHelper.GetIUserRepositoryMock(user));

            //act
            IHttpActionResult response = userController.Get(user.Email);

            //assert
            Assert.IsTrue(response is OkNegotiatedContentResult<User>);
        }

        [TestMethod]
        public void Get_ById_ShouldReturnOk()
        {
            //arrange
            User user = TestDataHelper.GenerateUser();
            var userController = new UserController(TestDataHelper.GetIUserRepositoryMock(user));

            //act
            IHttpActionResult response = userController.Get(user.Id.ToString());

            //assert
            Assert.IsTrue(response is OkNegotiatedContentResult<User>);
        }

        [TestMethod]
        public void Get_NonExistUserByEmail_ShouldReturnNotFound()
        {
            //arrange
            User user = TestDataHelper.GenerateUser();
            var userController = new UserController(TestDataHelper.GetIUserRepositoryMock(null));

            //act
            IHttpActionResult response = userController.Get(user.Email);

            //assert
            Assert.IsTrue(response is NotFoundResult);
        }

        [TestMethod]
        public void Update_ShouldReturnCreated()
        {
            //arrange
            User user = TestDataHelper.GenerateUser();
            var userController = new UserController(TestDataHelper.GetIUserRepositoryMock(user));

            //act
            IHttpActionResult response = userController.Update(user);

            //assert
            Assert.IsTrue(response is CreatedNegotiatedContentResult<User>);
        }

        [TestMethod]
        public void Update_WithNullField_ShouldReturnBadRequest()
        {
            //arrange
            User user = TestDataHelper.GenerateUser();
            user.Email = null;
            var userController = new UserController(TestDataHelper.GetIUserRepositoryMock(user));

            //act
            IHttpActionResult response = userController.Update(user);

            //assert
            Assert.IsTrue(response is BadRequestErrorMessageResult);
        }

        [TestMethod]
        public void Update_WithNonExistId_ShouldReturnNotFound()
        {
            //arrange
            User user = TestDataHelper.GenerateUser();
            var userController = new UserController(TestDataHelper.GetIUserRepositoryMock(null));

            //act
            IHttpActionResult response = userController.Update(user);

            //assert
            Assert.IsTrue(response is NotFoundResult);
        }

        [TestMethod]
        public void Delete_ShouldReturnOk()
        {
            //arrange
            User user = TestDataHelper.GenerateUser();
            var userController = new UserController(TestDataHelper.GetIUserRepositoryMock(user));

            //act
            userController.Create(user);
            IHttpActionResult response = userController.Delete(user.Id);

            //assert
            Assert.IsTrue(response is OkResult);
        }

        [TestMethod]
        public void Delete_NonExistUser_ShouldReturnNotFound()
        {
            //arrange
            User user = TestDataHelper.GenerateUser();
            var userController = new UserController(TestDataHelper.GetIUserRepositoryMock(null));

            //act
            IHttpActionResult response = userController.Delete(user.Id);

            //assert
            Assert.IsTrue(response is NotFoundResult);
        }
    }
}
