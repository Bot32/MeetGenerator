using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MeetGenerator.Model.Models;
using MeetGenerator.API.Controllers;
using System.Web.Http.Results;
using System.Web.Http;
using MeetGenerator.API.HttpActionResults;
using Moq;
using MeetGenerator.Model.Repositories;

namespace MeetGenerator.Tests.ControllerTests
{
    [TestClass]
    public class InvitationControllerTest
    {
        [TestMethod]
        public void Create_ShouldReturnOK()
        {
            //arrange
            Meeting meet = TestDataHelper.GenerateMeeting();
            User user = TestDataHelper.GenerateUser();
            var invitationController = GetInvitationControlller(meet, meet.Owner);

            //act
            IHttpActionResult response = invitationController.Create(CreateInvitation(meet, user));

            //assert
            Assert.IsTrue(response is OkResult);
        }

        [TestMethod]
        public void Create_NonExistUser_ShouldNotFound()
        {
            //arrange
            Meeting meet = TestDataHelper.GenerateMeeting();
            User user = TestDataHelper.GenerateUser();
            var invitationController = GetInvitationControlller(meet, null);

            //act
            IHttpActionResult response = invitationController.Create(CreateInvitation(meet, user));

            //assert
            Assert.IsTrue(response is NotFoundWithMessageResult);
        }

        [TestMethod]
        public void Create_NonExistMeet_ShouldNotFound()
        {
            //arrange
            //arrange
            Meeting meet = TestDataHelper.GenerateMeeting();
            User user = TestDataHelper.GenerateUser();
            var invitationController = GetInvitationControlller(null, meet.Owner);

            //act
            IHttpActionResult response = invitationController.Create(CreateInvitation(meet, user));

            //assert
            Assert.IsTrue(response is NotFoundWithMessageResult);
        }

        [TestMethod]
        public void Create_AlreadyInvited_ShouldReturnBadRequest()
        {
            //arrange
            Meeting meet = TestDataHelper.GenerateMeeting();
            User user = TestDataHelper.GenerateUser();
            var invitationController = GetInvitationControlller(meet, meet.Owner);

            //act
            meet.InvitedPeople.Add(user.Id, user);
            IHttpActionResult response = invitationController.Create(CreateInvitation(meet, user));

            //assert
            Assert.IsTrue(response is BadRequestErrorMessageResult);
        }

        [TestMethod]
        public void Get_ShouldReturnOK()
        {
            //arrange
            Meeting meet = TestDataHelper.GenerateMeeting();
            User user = TestDataHelper.GenerateUser();
            meet.InvitedPeople.Add(user.Id, user);
            var invitationController = GetInvitationControlller(meet, meet.Owner);

            //act
            IHttpActionResult response = invitationController.Get(CreateInvitation(meet, user));

            //assert
            Console.WriteLine(response);
            Assert.IsTrue(response is OkResult);
        }

        [TestMethod]
        public void Get_NonExistMeet_ShouldNotFound()
        {
            //arrange
            Meeting meet = TestDataHelper.GenerateMeeting();
            User user = TestDataHelper.GenerateUser();
            meet.InvitedPeople.Add(user.Id, user);
            var invitationController = GetInvitationControlller(null, meet.Owner);

            //act
            IHttpActionResult response = invitationController.Get(CreateInvitation(meet, user));

            //assert
            Assert.IsTrue(response is NotFoundWithMessageResult);
        }

        [TestMethod]
        public void Get_NotExistInvitation_ShouldReturnNotFound()
        {
            //arrange
            Meeting meet = TestDataHelper.GenerateMeeting();
            User user = TestDataHelper.GenerateUser();
            var invitationController = GetInvitationControlller(meet, meet.Owner);

            //act
            IHttpActionResult response = invitationController.Get(CreateInvitation(meet, user));

            //assert
            Console.WriteLine(response);
            Assert.IsTrue(response is NotFoundResult);
        }

        [TestMethod]
        public void Delete_ShouldReturnOK()
        {
            //arrange
            Meeting meet = TestDataHelper.GenerateMeeting();
            User user = TestDataHelper.GenerateUser();
            meet.InvitedPeople.Add(user.Id, user);
            var invitationController = GetInvitationControlller(meet, meet.Owner);

            //act
            IHttpActionResult response = invitationController.Delete(CreateInvitation(meet, user));

            //assert
            Console.WriteLine(response);
            Assert.IsTrue(response is OkResult);
        }

        [TestMethod]
        public void Delete_NonExistMeet_ShouldNotFound()
        {
            //arrange
            Meeting meet = TestDataHelper.GenerateMeeting();
            User user = TestDataHelper.GenerateUser();
            meet.InvitedPeople.Add(user.Id, user);
            var invitationController = GetInvitationControlller(null, meet.Owner);

            //act
            IHttpActionResult response = invitationController.Delete(CreateInvitation(meet, user));

            //assert
            Assert.IsTrue(response is NotFoundWithMessageResult);
        }

        [TestMethod]
        public void Delete_NotExistInvitation_ShouldReturnNotFound()
        {
            //arrange
            Meeting meet = TestDataHelper.GenerateMeeting();
            User user = TestDataHelper.GenerateUser();
            var invitationController = GetInvitationControlller(meet, meet.Owner);

            //act
            IHttpActionResult response = invitationController.Delete(CreateInvitation(meet, user));

            //assert
            Console.WriteLine(response);
            Assert.IsTrue(response is NotFoundWithMessageResult);
        }

        Invitation CreateInvitation(Meeting meeting, User user)
        {
            return new Invitation
            {
                MeetingID = meeting.Id,
                UserID = user.Id
            };
        }

        InvitationController GetInvitationControlller(Meeting getMeetingResult, User getUserResult)
        {
            return new InvitationController(
                TestDataHelper.GetIMeetingRepositoryMock(getMeetingResult),
                TestDataHelper.GetIUserRepositoryMock(getUserResult));
        }
    }
}
