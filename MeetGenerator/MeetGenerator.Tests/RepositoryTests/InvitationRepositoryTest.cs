using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MeetGenerator.Repository.SQL.Repositories;
using MeetGenerator.Model.Models;

namespace MeetGenerator.Tests.RepositoryTests
{
    [TestClass]
    public class InvitationRepositoryTest
    {
        [TestMethod]
        public void CreateInvitation_ShouldCreate()
        {
            //arange
            PlaceRepository placeRep = new PlaceRepository(Properties.Resources.ConnectionString);
            MeetingRepository meetRep = new MeetingRepository(Properties.Resources.ConnectionString);
            UserRepository userRep = new UserRepository(Properties.Resources.ConnectionString);
            InvitationRepository inviteRep = new InvitationRepository(Properties.Resources.ConnectionString);
            Meeting meeting = TestDataHelper.GenerateMeeting();

            //act
            userRep.CreateUser(meeting.Owner);
            placeRep.CreatePlace(meeting.Place);
            meetRep.CreateMeeting(meeting);

            foreach (User user in meeting.InvitedPeople.Values)
            {
                userRep.CreateUser(user);
                inviteRep.Create(CreateInvitation(meeting, user));
            }

            Meeting resultMeeting = meetRep.GetMeeting(meeting.Id);

            //assert
            TestDataHelper.PrintMeetingInfo(meeting);
            TestDataHelper.PrintMeetingInfo(resultMeeting);
            Assert.IsTrue(TestDataHelper.CompareInvitedUsersLists
                (meeting.InvitedPeople.Values, resultMeeting.InvitedPeople.Values));
        }

        [TestMethod]
        public void IsExistInvitation_ShouldExist()
        {
            //arange
            PlaceRepository placeRep = new PlaceRepository(Properties.Resources.ConnectionString);
            MeetingRepository meetRep = new MeetingRepository(Properties.Resources.ConnectionString);
            UserRepository userRep = new UserRepository(Properties.Resources.ConnectionString);
            InvitationRepository inviteRep = new InvitationRepository(Properties.Resources.ConnectionString);

            Meeting meeting = TestDataHelper.GenerateMeeting();
            User invitedUser = TestDataHelper.GenerateUser();

            meeting.InvitedPeople.Clear();

            //act
            userRep.CreateUser(meeting.Owner);
            placeRep.CreatePlace(meeting.Place);
            meetRep.CreateMeeting(meeting);
            userRep.CreateUser(invitedUser);

            inviteRep.Create(CreateInvitation(meeting, invitedUser));

            //assert
            Assert.IsTrue(inviteRep.IsExist(CreateInvitation(meeting, invitedUser)));
        }

        [TestMethod]
        public void IsExistInvitation_ShouldNotExist()
        {
            //arange
            InvitationRepository inviteRep = new InvitationRepository(Properties.Resources.ConnectionString);

            Meeting meeting = TestDataHelper.GenerateMeeting();
            User invitedUser = TestDataHelper.GenerateUser();

            //act

            //assert
            Assert.IsFalse(inviteRep.IsExist(CreateInvitation(meeting, invitedUser)));
        }

        [TestMethod]
        public void DeleteInvitation_ShouldDelete()
        {
            //arange
            PlaceRepository placeRep = new PlaceRepository(Properties.Resources.ConnectionString);
            MeetingRepository meetRep = new MeetingRepository(Properties.Resources.ConnectionString);
            UserRepository userRep = new UserRepository(Properties.Resources.ConnectionString);
            InvitationRepository inviteRep = new InvitationRepository(Properties.Resources.ConnectionString);

            Meeting meeting = TestDataHelper.GenerateMeeting();
            User invitedUser = TestDataHelper.GenerateUser();

            meeting.InvitedPeople.Clear();

            //act
            userRep.CreateUser(meeting.Owner);
            placeRep.CreatePlace(meeting.Place);
            meetRep.CreateMeeting(meeting);

            userRep.CreateUser(invitedUser);
            inviteRep.Create(CreateInvitation(meeting, invitedUser));

            Meeting resultMeeting = meetRep.GetMeeting(meeting.Id);

            bool inviteResult = resultMeeting.InvitedPeople.Count == 1;
            TestDataHelper.PrintMeetingInfo(resultMeeting);

            inviteRep.Delete(CreateInvitation(resultMeeting, invitedUser));

            resultMeeting = meetRep.GetMeeting(meeting.Id);

            bool deleteResult = resultMeeting.InvitedPeople.Count == 0;

            //assert
            TestDataHelper.PrintMeetingInfo(resultMeeting);
            Assert.IsTrue(inviteResult & deleteResult);
        }

        Invitation CreateInvitation(Meeting meeting, User user)
        {
            return new Invitation
            {
                MeetingID = meeting.Id,
                UserID = user.Id
            };
        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
            TestDataHelper.ClearDB();
        }
    }
}
