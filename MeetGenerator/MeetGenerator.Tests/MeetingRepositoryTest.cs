using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MeetGenerator.Model.Models;
using MeetGenerator.Repository.SQL;
using MeetGenerator.Repository.SQL.Repositories;

namespace MeetGenerator.Tests
{
    [TestClass]
    public class MeetingRepositoryTest
    {
        [TestMethod]
        public void CreateMeetingTest()
        {
            //arange
            PlaceRepository placeRep = new PlaceRepository(Properties.Resources.ConnectionString);
            MeetingRepository meetRep = new MeetingRepository(Properties.Resources.ConnectionString);
            UserRepository userRep = new UserRepository(Properties.Resources.ConnectionString);
            Meeting meeting = TestDataHelper.GenerateMeeting();

            //act
            userRep.CreateUser(meeting.Owner);
            placeRep.CreatePlace(meeting.Place);
            meetRep.CreateMeeting(meeting);
            Meeting resultMeeting = meetRep.GetMeeting(meeting.Id);


            //assert
            TestDataHelper.PrintMeetingInfo(meeting);
            TestDataHelper.PrintMeetingInfo(resultMeeting);
            Assert.IsTrue(TestDataHelper.CompareMeetings(meeting, resultMeeting));
        }

        [TestMethod]
        public void InviteUserToMeetingTest()
        {
            //arange
            PlaceRepository placeRep = new PlaceRepository(Properties.Resources.ConnectionString);
            MeetingRepository meetRep = new MeetingRepository(Properties.Resources.ConnectionString);
            UserRepository userRep = new UserRepository(Properties.Resources.ConnectionString);
            Meeting meeting = TestDataHelper.GenerateMeeting();

            //act
            userRep.CreateUser(meeting.Owner);
            placeRep.CreatePlace(meeting.Place);
            meetRep.CreateMeeting(meeting);

            foreach (User user in meeting.InvitedPeople)
            {
                userRep.CreateUser(user);
                meetRep.InviteUserToMeeting(user.Id, meeting.Id);
            }

            Meeting resultMeeting = meetRep.GetMeeting(meeting.Id);


            //assert
            TestDataHelper.PrintMeetingInfo(meeting);
            TestDataHelper.PrintMeetingInfo(resultMeeting);
            Assert.IsTrue(TestDataHelper.CompareInvitedUsersLists(meeting.InvitedPeople, resultMeeting.InvitedPeople));

        }

        [TestMethod]
        public void DeleteMeetingTest()
        {
            //arange
            PlaceRepository placeRep = new PlaceRepository(Properties.Resources.ConnectionString);
            MeetingRepository meetRep = new MeetingRepository(Properties.Resources.ConnectionString);
            UserRepository userRep = new UserRepository(Properties.Resources.ConnectionString);
            Meeting meeting = TestDataHelper.GenerateMeeting();

            //act
            userRep.CreateUser(meeting.Owner);
            placeRep.CreatePlace(meeting.Place);
            meetRep.CreateMeeting(meeting);
            foreach (User user in meeting.InvitedPeople)
            {
                userRep.CreateUser(user);
                meetRep.InviteUserToMeeting(user.Id, meeting.Id);
            }

            Meeting resultMeeting = meetRep.GetMeeting(meeting.Id);
            if (resultMeeting != null) TestDataHelper.PrintMeetingInfo(resultMeeting);
            else Console.WriteLine("Meeting not exist");

            meetRep.DeleteMeeting(resultMeeting);
            resultMeeting = meetRep.GetMeeting(meeting.Id);

            //assert
            if (resultMeeting != null) TestDataHelper.PrintMeetingInfo(resultMeeting);
            else Console.WriteLine("Meeting deleted");
            Assert.IsNull(resultMeeting);
        }

        [TestMethod]
        public void UpdateMeetingTest()
        {
            //arange
            PlaceRepository placeRep = new PlaceRepository(Properties.Resources.ConnectionString);
            MeetingRepository meetRep = new MeetingRepository(Properties.Resources.ConnectionString);
            UserRepository userRep = new UserRepository(Properties.Resources.ConnectionString);
            Meeting firstMeeting = TestDataHelper.GenerateMeeting();
            Meeting secondMeeting = TestDataHelper.GenerateMeeting();
            secondMeeting.Id = firstMeeting.Id;
            secondMeeting.Owner = firstMeeting.Owner;
            secondMeeting.Place = firstMeeting.Place;
            secondMeeting.Title = "secondMeeting";
            secondMeeting.Description = "second descr";

            //act
            placeRep.CreatePlace(firstMeeting.Place);
            userRep.CreateUser(firstMeeting.Owner);
            meetRep.CreateMeeting(firstMeeting);

            meetRep.UpdateMeetingInfo(secondMeeting);
            Meeting resultMeeting = meetRep.GetMeeting(firstMeeting.Id);


            //assert
            TestDataHelper.PrintMeetingInfo(firstMeeting);
            TestDataHelper.PrintMeetingInfo(secondMeeting);
            TestDataHelper.PrintMeetingInfo(resultMeeting);
            Assert.IsTrue(TestDataHelper.CompareMeetings(secondMeeting, resultMeeting));
        }

        [TestMethod]
        public void GetAllMeetingsCreatedByUserTest()
        {
            //arange
            PlaceRepository placeRep = new PlaceRepository(Properties.Resources.ConnectionString);
            MeetingRepository meetRep = new MeetingRepository(Properties.Resources.ConnectionString);
            UserRepository userRep = new UserRepository(Properties.Resources.ConnectionString);
            User user = TestDataHelper.GenerateUser();
            List<Meeting> meetingList = new List<Meeting>();

            for (int i = 0; i == 5; i++)
            {
                Meeting meeting = TestDataHelper.GenerateMeeting();
                meeting.Owner = user;
                meetingList.Add(meeting);
            }

            //act
            userRep.CreateUser(user);
            foreach(Meeting meeting in meetingList)
            {
                placeRep.CreatePlace(meeting.Place);
                meetRep.CreateMeeting(meeting);
            }
            List<Meeting> resultMeetingList = meetRep.GetAllMeetingsCreatedByUser(user.Id);

            //assert
            Assert.IsTrue(TestDataHelper.CompareMeetingsLists(meetingList, resultMeetingList));
        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
            TestDataHelper.ClearDB();
        }
    }
}
