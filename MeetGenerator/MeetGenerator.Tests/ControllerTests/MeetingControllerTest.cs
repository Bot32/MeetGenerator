using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MeetGenerator.API.Controllers;
using MeetGenerator.Model.Models;
using System.Web.Http;
using System.Web.Http.Results;
using MeetGenerator.Repository.SQL.Repositories;
using MeetGenerator.API.HttpActionResults;

namespace MeetGenerator.Tests.ControllerTests
{
    [TestClass]
    public class MeetingControllerTest
    {
        [TestMethod]
        public void CreateTest_ShouldReturnCreated()
        {
            //arrange
            var meetController = new MeetingController();
            var userRep = new UserRepository("Data Source=KONSTANTIN-PC;Initial Catalog=MeetGenDB;Integrated Security=True;");
            var placeRep = new PlaceRepository("Data Source=KONSTANTIN-PC;Initial Catalog=MeetGenDB;Integrated Security=True;");
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
        public void CreateTest_WithNullField_ShouldReturnBadRequest()
        {
            //arrange
            var meetController = new MeetingController();
            Meeting meet = TestDataHelper.GenerateMeeting();
            meet.Owner = null;

            //act
            IHttpActionResult response = meetController.Create(meet);

            //assert
            Console.WriteLine(response);
            Assert.IsTrue(response is BadRequestErrorMessageResult);
        }

        [TestMethod]
        public void CreateTest_WithNonExistOwner_ShouldReturnNotFound()
        {
            //arrange
            var meetController = new MeetingController();
            Meeting meet = TestDataHelper.GenerateMeeting();

            //act
            IHttpActionResult response = meetController.Create(meet);

            //assert
            Console.WriteLine(response);
            Assert.IsTrue(response is NotFoundWithErrorResult);
        }

        [TestMethod]
        public void CreateTest_WithNonExistPlace_ShouldReturnNotFound()
        {
            //arrange
            var meetController = new MeetingController();
            var userRep = new UserRepository("Data Source=KONSTANTIN-PC;Initial Catalog=MeetGenDB;Integrated Security=True;");
            Meeting meet = TestDataHelper.GenerateMeeting();

            //act
            userRep.CreateUser(meet.Owner);
            IHttpActionResult response = meetController.Create(meet);

            //assert
            Console.WriteLine(response);
            Assert.IsTrue(response is NotFoundWithErrorResult);
        }

        [TestMethod]
        public void GetTest_ById_ShouldReturnOk()
        {
            var meetController = new MeetingController();
            var userRep = new UserRepository("Data Source=KONSTANTIN-PC;Initial Catalog=MeetGenDB;Integrated Security=True;");
            var placeRep = new PlaceRepository("Data Source=KONSTANTIN-PC;Initial Catalog=MeetGenDB;Integrated Security=True;");
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
        public void GetTest_NonExistMeetingById_ShouldReturnNotFound()
        {
            var meetController = new MeetingController();
            Meeting meet = TestDataHelper.GenerateMeeting();

            //act
            IHttpActionResult response = meetController.Get(meet.Id);

            //assert
            Console.WriteLine(response);
            Assert.IsTrue(response is NotFoundResult);
        }

        [TestMethod]
        public void UpdateTest_ShouldReturnCreated()
        {
            //arrange
            var meetController = new MeetingController();
            var userRep = new UserRepository("Data Source=KONSTANTIN-PC;Initial Catalog=MeetGenDB;Integrated Security=True;");
            var placeRep = new PlaceRepository("Data Source=KONSTANTIN-PC;Initial Catalog=MeetGenDB;Integrated Security=True;");
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
        public void UpdateTest_WithNullField_ShouldReturnBadRequest()
        {
            //arrange
            var meetController = new MeetingController();
            Meeting meet = TestDataHelper.GenerateMeeting();
            meet.Owner = null;

            //act
            IHttpActionResult response = meetController.Update(meet);

            //assert
            Assert.IsTrue(response is BadRequestErrorMessageResult);
        }

        [TestMethod]
        public void UpdateTest_WithNonExistId_ShouldReturnNotFound()
        {
            //arrange
            var meetController = new MeetingController();
            var userRep = new UserRepository("Data Source=KONSTANTIN-PC;Initial Catalog=MeetGenDB;Integrated Security=True;");
            var placeRep = new PlaceRepository("Data Source=KONSTANTIN-PC;Initial Catalog=MeetGenDB;Integrated Security=True;");
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
        public void DeleteTest_ShouldReturnOk()
        {
            //arrange
            var meetController = new MeetingController();
            var userRep = new UserRepository("Data Source=KONSTANTIN-PC;Initial Catalog=MeetGenDB;Integrated Security=True;");
            var placeRep = new PlaceRepository("Data Source=KONSTANTIN-PC;Initial Catalog=MeetGenDB;Integrated Security=True;");
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
        public void DeleteTest_NonExistMeeting_ShouldReturnNotFound()
        {
            var meetController = new MeetingController();
            Meeting meet = TestDataHelper.GenerateMeeting();

            //act
            IHttpActionResult response = meetController.Delete(meet.Id);

            //assert
            Assert.IsTrue(response is NotFoundResult);
        }

        [TestMethod]
        public void InviteUserToMeetingTest_ShouldReturnCreated()
        {
            //arrange
            var meetController = new MeetingController();
            var userRep = new UserRepository("Data Source=KONSTANTIN-PC;Initial Catalog=MeetGenDB;Integrated Security=True;");
            var meetRep = new MeetingRepository("Data Source=KONSTANTIN-PC;Initial Catalog=MeetGenDB;Integrated Security=True;");
            var placeRep = new PlaceRepository("Data Source=KONSTANTIN-PC;Initial Catalog=MeetGenDB;Integrated Security=True;");
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
        public void InviteUserToMeetingTest_NonExistUser_ShouldNotFound()
        {
            //arrange
            var meetController = new MeetingController();
            var userRep = new UserRepository("Data Source=KONSTANTIN-PC;Initial Catalog=MeetGenDB;Integrated Security=True;");
            var meetRep = new MeetingRepository("Data Source=KONSTANTIN-PC;Initial Catalog=MeetGenDB;Integrated Security=True;");
            var placeRep = new PlaceRepository("Data Source=KONSTANTIN-PC;Initial Catalog=MeetGenDB;Integrated Security=True;");
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
        public void InviteUserToMeetingTest_NonExistMeet_ShouldNotFound()
        {
            //arrange
            var meetController = new MeetingController();
            var userRep = new UserRepository("Data Source=KONSTANTIN-PC;Initial Catalog=MeetGenDB;Integrated Security=True;");
            var meetRep = new MeetingRepository("Data Source=KONSTANTIN-PC;Initial Catalog=MeetGenDB;Integrated Security=True;");
            var placeRep = new PlaceRepository("Data Source=KONSTANTIN-PC;Initial Catalog=MeetGenDB;Integrated Security=True;");
            Meeting meet = TestDataHelper.GenerateMeeting();
            User user = TestDataHelper.GenerateUser();

            //act
            userRep.CreateUser(user);
            IHttpActionResult response = meetController.InviteUserToMeeting(user.Id, meet.Id);

            //assert
            Assert.IsTrue(response is NotFoundWithErrorResult);
        }

        [TestMethod]
        public void InviteUserToMeetingTest_Alreadyinvited_ShouldReturnBadRequest()
        {
            //arrange
            var meetController = new MeetingController();
            var userRep = new UserRepository("Data Source=KONSTANTIN-PC;Initial Catalog=MeetGenDB;Integrated Security=True;");
            var meetRep = new MeetingRepository("Data Source=KONSTANTIN-PC;Initial Catalog=MeetGenDB;Integrated Security=True;");
            var placeRep = new PlaceRepository("Data Source=KONSTANTIN-PC;Initial Catalog=MeetGenDB;Integrated Security=True;");
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
