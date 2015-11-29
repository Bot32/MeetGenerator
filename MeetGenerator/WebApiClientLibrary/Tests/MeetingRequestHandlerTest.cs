using MeetGenerator.Model.Models;
using MeetGenerator.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApiClientLibrary.RequestHadlers;

namespace WebApiClientLibrary.Tests
{
    [TestClass]
    public class MeetingRequestHandlerTest
    {
        string hostAddress = Properties.Resources.host_address;

        [TestMethod]
        public async Task Create_ShouldReturnCreate()
        {
            //arrange 
            var meetingHandler = new MeetingRequestHandler(hostAddress);
            var userHandler = new UserRequestHandler(hostAddress);
            var placeHandler = new PlaceRequestHandler(hostAddress);
            Place place = TestDataHelper.GeneratePlace();
            User user = TestDataHelper.GenerateUser();
            

            Meeting meeting = TestDataHelper.GenerateMeeting();
            meeting.Date = new DateTime(2016, 1, 1);
            meeting.InvitedPeople.Clear();

            //act
            HttpResponseMessage response = await userHandler.Create(user);
            meeting.Owner = await response.Content.ReadAsAsync<User>();

            HttpResponseMessage response2 = await placeHandler.Create(place);
            meeting.Place = await response2.Content.ReadAsAsync<Place>();

            HttpResponseMessage resultResponse = await meetingHandler.Create(meeting);
            Meeting resultMeet = await resultResponse.Content.ReadAsAsync<Meeting>();

            //assert
            TestDataHelper.PrintMeetingInfo(meeting);
            Assert.IsTrue(resultResponse.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task Get_ShouldReturnMeeting()
        {
            //arrange 
            var meetingHandler = new MeetingRequestHandler(hostAddress);
            var userHandler = new UserRequestHandler(hostAddress);
            var placeHandler = new PlaceRequestHandler(hostAddress);

            Place place = TestDataHelper.GeneratePlace();
            User user = TestDataHelper.GenerateUser();

            Meeting meeting = TestDataHelper.GenerateMeeting();
            meeting.Date = new DateTime(2016, 1, 1);
            meeting.InvitedPeople.Clear();

            //act
            HttpResponseMessage response1 = await userHandler.Create(user);
            meeting.Owner = await response1.Content.ReadAsAsync<User>();

            HttpResponseMessage response2 = await placeHandler.Create(place);
            meeting.Place = await response2.Content.ReadAsAsync<Place>();

            HttpResponseMessage response3 = await meetingHandler.Create(meeting);
            Meeting resultMeet = await response3.Content.ReadAsAsync<Meeting>();

            HttpResponseMessage resultResponse = await meetingHandler.Get(resultMeet.Id);
            resultMeet = await resultResponse.Content.ReadAsAsync<Meeting>();

            //assert
            TestDataHelper.PrintMeetingInfo(resultMeet);
            Assert.IsTrue(resultResponse.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task Invite_ShouldReturnOk()
        {
            //arrange 
            var meetingHandler = new MeetingRequestHandler(hostAddress);
            var userHandler = new UserRequestHandler(hostAddress);
            var placeHandler = new PlaceRequestHandler(hostAddress);

            Place place = TestDataHelper.GeneratePlace();
            User user = TestDataHelper.GenerateUser();

            Meeting meeting = TestDataHelper.GenerateMeeting();
            meeting.Date = new DateTime(2016, 1, 1);
            meeting.InvitedPeople.Clear();

            //act
            HttpResponseMessage response1 = await userHandler.Create(user);
            meeting.Owner = await response1.Content.ReadAsAsync<User>();

            HttpResponseMessage response2 = await placeHandler.Create(place);
            meeting.Place = await response2.Content.ReadAsAsync<Place>();

            HttpResponseMessage response3 = await meetingHandler.Create(meeting);
            Meeting resultMeet = await response3.Content.ReadAsAsync<Meeting>();

            HttpResponseMessage resultResponse = await meetingHandler.Invite(resultMeet.Id, resultMeet.Owner.Id);

            //assert
            TestDataHelper.PrintMeetingInfo(resultMeet);
            Assert.IsTrue(resultResponse.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task Update_ShouldReturnOk()
        {
            //arrange 
            var meetingHandler = new MeetingRequestHandler(hostAddress);
            var userHandler = new UserRequestHandler(hostAddress);
            var placeHandler = new PlaceRequestHandler(hostAddress);

            Place place = TestDataHelper.GeneratePlace();
            User user = TestDataHelper.GenerateUser();

            Meeting meeting = TestDataHelper.GenerateMeeting();
            meeting.Date = new DateTime(2016, 1, 1);
            meeting.InvitedPeople.Clear();

            //act
            HttpResponseMessage response1 = await userHandler.Create(user);
            meeting.Owner = await response1.Content.ReadAsAsync<User>();

            HttpResponseMessage response2 = await placeHandler.Create(place);
            meeting.Place = await response2.Content.ReadAsAsync<Place>();

            HttpResponseMessage response3 = await meetingHandler.Create(meeting);
            Meeting resultMeet = await response3.Content.ReadAsAsync<Meeting>();

            HttpResponseMessage resultResponse = await meetingHandler.Update(resultMeet);

            //assert
            TestDataHelper.PrintMeetingInfo(meeting);
            Assert.IsTrue(resultResponse.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task Delete_ShouldReturnOk()
        {
            //arrange 
            var meetingHandler = new MeetingRequestHandler(hostAddress);
            var userHandler = new UserRequestHandler(hostAddress);
            var placeHandler = new PlaceRequestHandler(hostAddress);

            Place place = TestDataHelper.GeneratePlace();
            User user = TestDataHelper.GenerateUser();

            Meeting meeting = TestDataHelper.GenerateMeeting();
            meeting.Date = new DateTime(2016, 1, 1);
            meeting.InvitedPeople.Clear();

            //act
            HttpResponseMessage response1 = await userHandler.Create(user);
            meeting.Owner = await response1.Content.ReadAsAsync<User>();

            HttpResponseMessage response2 = await placeHandler.Create(place);
            meeting.Place = await response2.Content.ReadAsAsync<Place>();

            HttpResponseMessage response3 = await meetingHandler.Create(meeting);
            Meeting resultMeet = await response3.Content.ReadAsAsync<Meeting>();

            await meetingHandler.Invite(resultMeet.Id, resultMeet.Owner.Id);

            HttpResponseMessage response4 = await meetingHandler.Get(resultMeet.Id);
            resultMeet = await response4.Content.ReadAsAsync<Meeting>();

            HttpResponseMessage resultResponse = await meetingHandler.Delete(resultMeet.Id);

            //assert
            TestDataHelper.PrintMeetingInfo(resultMeet);
            Assert.IsTrue(resultResponse.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task GetAllMeetingsCreatedByUser_ShouldReturnCreate()
        {
            //arrange 
            var meetingHandler = new MeetingRequestHandler(hostAddress);
            var userHandler = new UserRequestHandler(hostAddress);
            var placeHandler = new PlaceRequestHandler(hostAddress);

            Place place = TestDataHelper.GeneratePlace();
            User user = TestDataHelper.GenerateUser();
            List<Meeting> meetings = new List<Meeting>();
            List<Meeting> resultMeetings = new List<Meeting>();


            //act
            HttpResponseMessage response1 = await userHandler.Create(user);
            user = await response1.Content.ReadAsAsync<User>();

            HttpResponseMessage response2 = await placeHandler.Create(place);
            place = await response2.Content.ReadAsAsync<Place>();

            for (int i = 0; i < 10; i++)
            {
                Meeting meeting = TestDataHelper.GenerateMeeting();
                meeting.Owner = user;
                meeting.Date = new DateTime(2016, 1, 1);
                meeting.InvitedPeople.Clear();
                meeting.Place = place;
                meetings.Add(meeting);
            }

            foreach (Meeting meet in meetings)
                await meetingHandler.Create(meet);

            HttpResponseMessage response = await meetingHandler.GetAllMeetingsCreatedByUser(user.Id);
            resultMeetings = await response.Content.ReadAsAsync<List<Meeting>>();

            //assert
            foreach (Meeting meet in resultMeetings)
                TestDataHelper.PrintMeetingInfo(meet);
            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
            TestDataHelper.ClearDB();
        }
    }
}
