using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MeetGenerator.API.Controllers;
using System.Web.Http.Results;
using System.Web.Http;
using MeetGenerator.Model.Models;
using MeetGenerator.Repository.SQL.Repositories;
using MeetGenerator.Tests.Properties;
using MeetGenerator.API.HttpActionResults;

namespace MeetGenerator.Tests.ControllerTests
{
    [TestClass]
    public class MeetingControllerTest
    {
        [TestMethod]
        public void Create_ShouldReturnCreated()
        {
            //arrange
            Meeting meet = TestDataHelper.GenerateMeeting();
            meet.Date = new DateTime(3000, 12, 31);
            var meetController = GetMeetingControlller(meet, meet.Owner, meet.Place);

            //act
            IHttpActionResult response = meetController.Create(meet);

            //assert
            Assert.IsTrue(response is CreatedNegotiatedContentResult<Meeting>);
        }

        [TestMethod]
        public void Create_WithNullField_ShouldReturnBadRequest()
        {
            //arrange
            Meeting meet = TestDataHelper.GenerateMeeting();
            meet.Owner = null;
            var meetController = GetMeetingControlller(meet, meet.Owner, meet.Place);

            //act
            IHttpActionResult response = meetController.Create(meet);

            //assert
            Assert.IsTrue(response is BadRequestErrorMessageResult);
        }

        [TestMethod]
        public void Create_WithNonExistOwner_ShouldReturnNotFound()
        {
            //arrange
            Meeting meet = TestDataHelper.GenerateMeeting();
            meet.Date = new DateTime(3000, 12, 31);
            var meetController = GetMeetingControlller(meet, null, meet.Place);

            //act
            IHttpActionResult response = meetController.Create(meet);

            //assert
            Console.WriteLine(response);
            Assert.IsTrue(response is NotFoundWithMessageResult);
        }

        [TestMethod]
        public void Create_WithNonExistPlace_ShouldReturnNotFound()
        {
            //arrange
            Meeting meet = TestDataHelper.GenerateMeeting();
            meet.Date = new DateTime(3000, 12, 31);
            var meetController = GetMeetingControlller(meet, meet.Owner, null);

            //act
            IHttpActionResult response = meetController.Create(meet);

            //assert
            Assert.IsTrue(response is NotFoundWithMessageResult);
        }

        [TestMethod]
        public void Get_ById_ShouldReturnOk()
        {
            Meeting meet = TestDataHelper.GenerateMeeting();
            var meetController = GetMeetingControlller(meet, meet.Owner, meet.Place);

            //act
            IHttpActionResult response = meetController.Get(meet.Id);

            //assert
            Console.WriteLine(response);
            Assert.IsTrue(response is OkNegotiatedContentResult<Meeting>);
        }

        [TestMethod]
        public void Get_NonExistMeetingById_ShouldReturnNotFound()
        {
            Meeting meet = TestDataHelper.GenerateMeeting();
            var meetController = GetMeetingControlller(null, meet.Owner, meet.Place);

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
            Meeting meet = TestDataHelper.GenerateMeeting();
            meet.Date = new DateTime(3000, 12, 31);
            var meetController = GetMeetingControlller(meet, meet.Owner, meet.Place);

            //act
            IHttpActionResult response = meetController.Update(meet);

            //assert
            Assert.IsTrue(response is CreatedNegotiatedContentResult<Meeting>);
        }

        [TestMethod]
        public void Update_WithNullField_ShouldReturnBadRequest()
        {
            //arrange
            Meeting meet = TestDataHelper.GenerateMeeting();
            meet.Owner = null;
            var meetController = GetMeetingControlller(meet, meet.Owner, meet.Place);

            //act
            IHttpActionResult response = meetController.Update(meet);

            //assert
            Assert.IsTrue(response is BadRequestErrorMessageResult);
        }

        [TestMethod]
        public void Update_WithNonExistId_ShouldReturnNotFound()
        {
            //arrange
            Meeting meet = TestDataHelper.GenerateMeeting();
            meet.Date = new DateTime(3000, 12, 31);
            var meetController = GetMeetingControlller(null, meet.Owner, meet.Place);

            //act
            IHttpActionResult response = meetController.Update(meet);

            //assert
            Assert.IsTrue(response is NotFoundResult);
        }

        [TestMethod]
        public void Delete_ShouldReturnOk()
        {
            //arrange
            Meeting meet = TestDataHelper.GenerateMeeting();
            var meetController = GetMeetingControlller(meet, meet.Owner, meet.Place);

            //act
            IHttpActionResult response = meetController.Delete(meet.Id);

            //assert
            Assert.IsTrue(response is OkResult);
        }

        [TestMethod]
        public void Delete_NonExistMeeting_ShouldReturnNotFound()
        {
            Meeting meet = TestDataHelper.GenerateMeeting();
            var meetController = GetMeetingControlller(null, meet.Owner, meet.Place);

            //act
            IHttpActionResult response = meetController.Delete(meet.Id);

            //assert
            Assert.IsTrue(response is NotFoundResult);
        }

        [TestMethod]
        public void InviteUserToMeeting_ShouldReturnOK()
        {
            //arrange
            Meeting meet = TestDataHelper.GenerateMeeting();
            User user = TestDataHelper.GenerateUser();
            var meetController = GetMeetingControlller(meet, meet.Owner, meet.Place);

            //act
            IHttpActionResult response = meetController.InviteUserToMeeting(
                new Invitation
                {
                    MeetingID = meet.Id,
                    UserID = user.Id
                });

            //assert
            Assert.IsTrue(response is OkResult);
        }

        [TestMethod]
        public void InviteUserToMeeting_NonExistUser_ShouldNotFound()
        {
            //arrange
            Meeting meet = TestDataHelper.GenerateMeeting();
            User user = TestDataHelper.GenerateUser();
            var meetController = GetMeetingControlller(meet, null, meet.Place);

            //act
            IHttpActionResult response = meetController.InviteUserToMeeting(
                new Invitation
                {
                    MeetingID = meet.Id,
                    UserID = user.Id
                });

            //assert
            Assert.IsTrue(response is NotFoundWithMessageResult);
        }

        [TestMethod]
        public void InviteUserToMeeting_NonExistMeet_ShouldNotFound()
        {
            //arrange
            //arrange
            Meeting meet = TestDataHelper.GenerateMeeting();
            User user = TestDataHelper.GenerateUser();
            var meetController = GetMeetingControlller(null, meet.Owner, meet.Place);

            //act
            IHttpActionResult response = meetController.InviteUserToMeeting(
                new Invitation
                {
                    MeetingID = meet.Id,
                    UserID = user.Id
                });

            //assert
            Assert.IsTrue(response is NotFoundWithMessageResult);
        }

        [TestMethod]
        public void InviteUserToMeeting_Alreadyinvited_ShouldReturnBadRequest()
        {
            //arrange
            Meeting meet = TestDataHelper.GenerateMeeting();
            User user = TestDataHelper.GenerateUser();
            var meetController = GetMeetingControlller(meet, meet.Owner, meet.Place);

            //act
            meet.InvitedPeople.Add(user.Id, user);
            IHttpActionResult response = meetController.InviteUserToMeeting(
                new Invitation
                {
                    MeetingID = meet.Id,
                    UserID = user.Id
                });

            //assert
            Assert.IsTrue(response is BadRequestErrorMessageResult);
        }

        MeetingController GetMeetingControlller(Meeting getMeetingResult, User getUserResult, Place getPlaceResult)
        {
            return new MeetingController(
                TestDataHelper.GetIMeetingRepositoryMock(getMeetingResult),
                TestDataHelper.GetIUserRepositoryMock(getUserResult),
                TestDataHelper.GetIPlaceRepositoryMock(getPlaceResult));
        }
    }
}
