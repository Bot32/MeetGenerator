using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MeetGenerator.Model.Models;
using System.Web.Http.Results;
using System.Web.Http;
using MeetGenerator.API.Controllers;

namespace MeetGenerator.Tests.ControllerTests
{
    [TestClass]
    public class UserControllerTest
    {
        [TestMethod]
        public void CreateTest_ShouldReturnCreated()
        {
            //arrange
            var userController = new UserController();
            User user = TestDataHelper.GenerateUser();

            //act
            IHttpActionResult response = userController.Create(user);

            //assert
            Assert.IsTrue(response is CreatedNegotiatedContentResult<User>);
        }

        [TestMethod]
        public void CreateTest_WithNullField_ShouldReturnBadRequest()
        {
            //arrange
            var userController = new UserController();
            User user = TestDataHelper.GenerateUser();
            user.FirstName = null;

            //act
            IHttpActionResult response = userController.Create(user);

            //assert
            Console.WriteLine(response);
            Assert.IsTrue(response is BadRequestErrorMessageResult);
        }

        [TestMethod]
        public void CreateTest_WithExistEmail_ShouldReturnBadRequest()
        {
            //arrange
            var userController = new UserController();
            User user1 = TestDataHelper.GenerateUser();
            User user2 = TestDataHelper.GenerateUser();
            user2.Email = user1.Email;

            //act
            IHttpActionResult response1 = userController.Create(user1);
            IHttpActionResult response2 = userController.Create(user2);

            //assert
            Assert.IsTrue((response1 is CreatedNegotiatedContentResult<User>) & (response2 is BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void GetTest_ByEmail_ShouldReturnOk()
        {
            //arrange
            var userController = new UserController();
            User user = TestDataHelper.GenerateUser();

            //act
            userController.Create(user);
            IHttpActionResult response = userController.Get(user.Email);

            //assert
            Assert.IsTrue(response is OkNegotiatedContentResult<User>);
        }

        [TestMethod]
        public void GetTest_ById_ShouldReturnOk()
        {
            //arrange
            var userController = new UserController();
            User user = TestDataHelper.GenerateUser();

            //act
            userController.Create(user);
            IHttpActionResult response = userController.Get(user.Id);

            //assert
            Assert.IsTrue(response is OkNegotiatedContentResult<User>);
        }

        [TestMethod]
        public void GetTest_NonExistUserByEmail_ShouldReturnNotFound()
        {
            //arrange
            var userController = new UserController();
            User user = TestDataHelper.GenerateUser();

            //act
            IHttpActionResult response = userController.Get(user.Email);

            //assert
            Assert.IsTrue(response is NotFoundResult);
        }

        [TestMethod]
        public void UpdateTest_ShouldReturnCreated()
        {
            //arrange
            var userController = new UserController();
            User user = TestDataHelper.GenerateUser();

            //act
            IHttpActionResult response1 = userController.Create(user);
            user.FirstName = "vasiliy";
            user.LastName = "pupen";
            user.Email = "pupen@vasya.vp";
            IHttpActionResult response2 = userController.Update(user);
            

            //assert
            Assert.IsTrue((response1 is CreatedNegotiatedContentResult<User>) & 
                          (response2 is CreatedNegotiatedContentResult<User>));
        }

        [TestMethod]
        public void UpdateTest_WithNullField_ShouldReturnBadRequest()
        {
            //arrange
            var userController = new UserController();
            User user = TestDataHelper.GenerateUser();
            user.Email = null;

            //act
            IHttpActionResult response = userController.Update(user);

            //assert
            Assert.IsTrue(response is BadRequestErrorMessageResult);
        }

        [TestMethod]
        public void UpdateTest_WithNonExistId_ShouldReturnNotFound()
        {
            //arrange
            var userController = new UserController();
            User user = TestDataHelper.GenerateUser();

            //act
            IHttpActionResult response = userController.Update(user);

            //assert
            Assert.IsTrue(response is NotFoundResult);
        }

        [TestMethod]
        public void DeleteTest_ShouldReturnOk()
        {
            //arrange
            var userController = new UserController();
            User user = TestDataHelper.GenerateUser();

            //act
            userController.Create(user);
            IHttpActionResult response = userController.Delete(user.Id);

            //assert
            Assert.IsTrue(response is OkResult);
        }

        [TestMethod]
        public void DeleteTest_NonExistUser_ShouldReturnNotFound()
        {
            //arrange
            var userController = new UserController();
            User user = TestDataHelper.GenerateUser();

            //act
            IHttpActionResult response = userController.Delete(user.Id);

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
