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
        static List<Meeting> testMeetings = new List<Meeting>();

        [TestMethod]
        public void CreateMeetingTest()
        {

        }

        public Meeting GenerateMeeting()
        {
            Meeting meeting = TestDataHelper.GenerateMeeting();
            testMeetings.Add(meeting);
            return meeting;
        }

        //[ClassCleanup()]
        //public static void ClassCleanup()
        //{
        //    var MeetingRepository = new MeetingRepository(Properties.Resources.ConnectionString);
        //    foreach (Meeting meeting in testMeetings)
        //    {
        //        MeetingRepository.DeleteMeeting(meeting.Id);
        //    }
        //}
    }
}
