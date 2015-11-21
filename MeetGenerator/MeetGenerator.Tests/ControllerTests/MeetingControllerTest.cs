using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MeetGenerator.API.Controllers;
using MeetGenerator.Model.Models;
using System.Web.Http;
using System.Web.Http.Results;
using MeetGenerator.Repository.SQL.Repositories;
using MeetGenerator.API.HttpActionResults;
using MeetGenerator.Tests.Properties;

namespace MeetGenerator.Tests.ControllerTests
{
    [TestClass]
    public class MeetingControllerTest
    {
        [TestMethod]
        public void Create_ShouldReturnCreated()
        {
            //arrange
            var meetController = new MeetingController(Resources.ConnectionString);
            var userRep = new UserRepository(Resources.ConnectionString);
            var placeRep = new PlaceRepository(Resources.ConnectionString);
            Meeting meet = TestDataHelper.GenerateMeeting();

            //act
            userRep.CreateUser(meet.Owner);
            placeRep.CreatePlace(meet.Place);
            IHttpActionResult response = meetController.Create(meet);

            //assert
            Console.WriteLine(response);
            Assert.IsTrue(response is CreatedNegotiatedContentResult<Meeting>);
        }

        [TestMethod]
        public void Create_WithNullField_ShouldReturnBadRequest()
        {
            //arrange
            var meetController = new MeetingController(Resources.ConnectionString);
            Meeting meet = TestDataHelper.GenerateMeeting();
            meet.Owner = null;

            //act
            IHttpActionResult response = meetController.Create(meet);

            //assert
            Console.WriteLine(response);
            Assert.IsTrue(response is BadRequestErrorMessageResult);
        }

        [TestMethod]
        public void Create_WithNonExistOwner_ShouldReturnNotFound()
        {
            //arrange
            var meetController = new MeetingController(Resources.ConnectionString);
            Meeting meet = TestDataHelper.GenerateMeeting();

            //act
            IHttpActionResult response = meetController.Create(meet);

            //assert
            Console.WriteLine(response);
            Assert.IsTrue(response is NotFoundWithErrorResult);
        }

        [TestMethod]
        public void Create_WithNonExistPlace_ShouldReturnNotFound()
        {
            //arrange
            var meetController = new MeetingController(Resources.ConnectionString);
            var userRep = new UserRepository(Resources.ConnectionString);
            Meeting meet = TestDataHelper.GenerateMeeting();

            //act
            userRep.CreateUser(meet.Owner);
            IHttpActionResult response = meetController.Create(meet);

            //assert
            Console.WriteLine(response);
            Assert.IsTrue(response is NotFoundWithErrorResult);
        }

        [TestMethod]
        public void Get_ById_ShouldReturnOk()
        {
            var meetController = new MeetingController(Resources.ConnectionString);
            var userRep = new UserRepository(Resources.ConnectionString);
            var placeRep = new PlaceRepository(Resources.ConnectionString);
            Meeting meet = TestDataHelper.GenerateMeeting();

            //act
            userRep.CreateUser(meet.Owner);
            placeRep.CreatePlace(meet.Place);
            meetController.Create(meet);
            IHttpActionResult response = meetController.Get(meet.Id);

            //assert
            Console.WriteLine(response);
            Assert.IsTrue(response is OkNegotiatedContentResult<Meeting>);
        }

        [TestMethod]
        public void Get_NonExistMeetingById_ShouldReturnNotFound()
        {
            var meetController = new MeetingController(Resources.ConnectionString);
            Meeting meet = TestDataHelper.GenerateMeeting();

            //act
            IHttpActionResult response = meetController.Get(meet.Id);

            //assert
            Console.WriteLine(response);
            Assert.IsTrue(response is NotFoundResult);
        }

        [TestMethod]
        public void Update_ShouldReturnCreated()
        {
            //arrange
            var meetController = new MeetingController(Resources.ConnectionString);
            var userRep = new UserRepository(Resources.ConnectionString);
            var placeRep = new PlaceRepository(Resources.ConnectionString);
            Meeting meet = TestDataHelper.GenerateMeeting();

            //act
            userRep.CreateUser(meet.Owner);
            placeRep.CreatePlace(meet.Place);
            IHttpActionResult response1 = meetController.Create(meet);
            meet.Title = "another meet";
            meet.Description = "another meet descr";
            IHttpActionResult response2 = meetController.Update(meet);

            //assert
            Assert.IsTrue((response1 is CreatedNegotiatedContentResult<Meeting>) &
                          (response2 is CreatedNegotiatedContentResult<Meeting>));
        }

        [TestMethod]
        public void Update_WithNullField_ShouldReturnBadRequest()
        {
            //arrange
            var meetController = new MeetingController(Resources.ConnectionString);
            Meeting meet = TestDataHelper.GenerateMeeting();
            meet.Owner = null;

            //act
            IHttpActionResult response = meetController.Update(meet);

            //assert
            Assert.IsTrue(response is BadRequestErrorMessageResult);
        }

        [TestMethod]
        public void Update_WithNonExistId_ShouldReturnNotFound()
        {
            //arrange
            var meetController = new MeetingController(Resources.ConnectionString);
            var userRep = new UserRepository(Resources.ConnectionString);
            var placeRep = new PlaceRepository(Resources.ConnectionString);
            Meeting meet = TestDataHelper.GenerateMeeting();

            //act
            userRep.CreateUser(meet.Owner);
            placeRep.CreatePlace(meet.Place);
            meet.Title = "another meet";
            meet.Description = "another meet descr";
            IHttpActionResult response = meetController.Update(meet);

            //assert
            Assert.IsTrue(response is NotFoundResult);
        }

        [TestMethod]
        public void Delete_ShouldReturnOk()
        {
            //arrange
            var meetController = new MeetingController(Resources.ConnectionString);
            var userRep = new UserRepository(Resources.ConnectionString);
            var placeRep = new PlaceRepository(Resources.ConnectionString);
            Meeting meet = TestDataHelper.GenerateMeeting();

            //act
            userRep.CreateUser(meet.Owner);
            placeRep.CreatePlace(meet.Place);
            IHttpActionResult response1 = meetController.Create(meet);
            foreach (User user in meet.InvitedPeople.Values)
            {
                meetController.InviteUserToMeeting(user.Id, meet.Id);
            }
            IHttpActionResult response2 = meetController.Delete(meet.Id);

            //assert
            Assert.IsTrue((response1 is CreatedNegotiatedContentResult<Meeting>) &
                          (response2 is OkResult));
        }

        [TestMethod]
        public void Delete_NonExistMeeting_ShouldReturnNotFound()
        {
            var meetController = new MeetingController(Resources.ConnectionString);
            Meeting meet = TestDataHelper.GenerateMeeting();

            //act
            IHttpActionResult response = meetController.Delete(meet.Id);

            //assert
            Assert.IsTrue(response is NotFoundResult);
        }

        [TestMethod]
        public void InviteUserToMeeting_ShouldReturnCreated()
        {
            //arrange
            var meetController = new MeetingController(Resources.ConnectionString);
            var userRep = new UserRepository(Resources.ConnectionString);
            var meetRep = new MeetingRepository(Resources.ConnectionString);
            var placeRep = new PlaceRepository(Resources.ConnectionString);
            Meeting meet = TestDataHelper.GenerateMeeting();

            foreach (User user in meet.InvitedPeople.Values)
            {
                Console.WriteLine(user.Email);
            }

            //act
            userRep.CreateUser(meet.Owner);
            placeRep.CreatePlace(meet.Place);
            IHttpActionResult response1 = meetController.Create(meet);
            foreach (User user in meet.InvitedPeople.Values)
            {
                userRep.CreateUser(user);
                meetController.InviteUserToMeeting(user.Id, meet.Id);
            }
            Meeting resultMeet = meetRep.GetMeeting(meet.Id);

            foreach (User user in resultMeet.InvitedPeople.Values)
            {
                Console.WriteLine(user.Email);
            }

            bool result = TestDataHelper.CompareInvitedUsersLists
                (meet.InvitedPeople.Values, resultMeet.InvitedPeople.Values);

            //assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void InviteUserToMeeting_NonExistUser_ShouldNotFound()
        {
            //arrange
            var meetController = new MeetingController(Resources.ConnectionString);
            var userRep = new UserRepository(Resources.ConnectionString);
            var meetRep = new MeetingRepository(Resources.ConnectionString);
            var placeRep = new PlaceRepository(Resources.ConnectionString);
            Meeting meet = TestDataHelper.GenerateMeeting();
            User user = TestDataHelper.GenerateUser();

            //act
            userRep.CreateUser(meet.Owner);
            placeRep.CreatePlace(meet.Place);
            IHttpActionResult response1 = meetController.Create(meet);

            IHttpActionResult response = meetController.InviteUserToMeeting(user.Id, meet.Id);


            //assert
            Assert.IsTrue(response is NotFoundWithErrorResult);
        }

        [TestMethod]
        public void InviteUserToMeeting_NonExistMeet_ShouldNotFound()
        {
            //arrange
            var meetController = new MeetingController(Resources.ConnectionString);
            var userRep = new UserRepository(Resources.ConnectionString);
            Meeting meet = TestDataHelper.GenerateMeeting();
            User user = TestDataHelper.GenerateUser();

            //act
            userRep.CreateUser(user);
            IHttpActionResult response = meetController.InviteUserToMeeting(user.Id, meet.Id);

            //assert
            Assert.IsTrue(response is NotFoundWithErrorResult);
        }

        [TestMethod]
        public void InviteUserToMeeting_Alreadyinvited_ShouldReturnBadRequest()
        {
            //arrange
            var meetController = new MeetingController(Resources.ConnectionString);
            var userRep = new UserRepository(Resources.ConnectionString);
            var meetRep = new MeetingRepository(Resources.ConnectionString);
            var placeRep = new PlaceRepository(Resources.ConnectionString);
            Meeting meet = TestDataHelper.GenerateMeeting();
            User user = TestDataHelper.GenerateUser();

            //act
            userRep.CreateUser(meet.Owner);
            userRep.CreateUser(user);
            placeRep.CreatePlace(meet.Place);
            meetController.Create(meet);
            Meeting resultMeet = meetRep.GetMeeting(meet.Id);
            meetController.InviteUserToMeeting(user.Id, meet.Id);

            IHttpActionResult response = meetController.InviteUserToMeeting(user.Id, meet.Id);


            //assert
            Assert.IsTrue(response is BadRequestErrorMessageResult);
        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
            TestDataHelper.ClearDB();
        }
    }
}
