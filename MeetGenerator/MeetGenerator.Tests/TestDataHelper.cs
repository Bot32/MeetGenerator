using MeetGenerator.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetGenerator.Repository.SQL.Repositories;
using System.Data.SqlClient;
using MeetGenerator.Repository.SQL.Repositories.Utility;

namespace MeetGenerator.Tests
{
    static class TestDataHelper
    {
        static int index = 1;
        static int Index
        {
            get
            {
                return index++;
            }
        }

        static public User GenerateUser()
        {
            User user = new User
            {
                Id = Guid.NewGuid(),
                Email = "test" + Index + "@test.com",
                FirstName = "name",
                LastName = "lastname"
            };
            
            return user;
        }

        static public Meeting GenerateMeeting()
        {
            Meeting meeting = new Meeting
            {
                Id = Guid.NewGuid(),
                Owner = TestDataHelper.GenerateUser(),
                Date = DateTime.Now,
                Title = "Best test meeting #" + Index,
                Description = "Really best test meeting ever",
                Place = new Place
                {
                    Id = Guid.NewGuid(),
                    Address = "Pushkin st., Kolotushkin house #" + Index,
                    Description = "Nobody home"
                },
                InvitedPeople = new List<User>(),
            };

            for (int i = 0; i < 5; i++) meeting.InvitedPeople.Add(GenerateUser());

            return meeting;
        }

        static public bool CompareUsers(User first, User second)
        {
            return first.Id.Equals(second.Id) &
                   first.Email.Equals(second.Email) &
                   first.FirstName.Equals(second.FirstName) &
                   first.LastName.Equals(second.LastName);
        }

        static public bool CompareMeetings(Meeting first, Meeting second)
        {
            return first.Id.Equals(second.Id) &
                   first.Owner.Id.Equals(second.Owner.Id) &
                   //first.Date.Equals(second.Date) &
                   first.Title.Equals(second.Title) &
                   first.Description.Equals(second.Description) &
                   first.Place.Id.Equals(second.Place.Id);
                   
        }

        static public bool CompareInvitedUsersLists(List<User> first, List<User> second)
        {
            int compareInvitedPeopleCount = 0;

            foreach (User user1 in first)
            {
                foreach (User user2 in second)
                {
                    if (user1.Id.Equals(user2.Id))
                    {
                        compareInvitedPeopleCount++;
                    }
                }
            }

            if ((compareInvitedPeopleCount == first.Count) &
                (compareInvitedPeopleCount == second.Count))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static public void PrintUserInfo(User user)
        {
            Console.WriteLine("Id = " + user.Id);
            Console.WriteLine("FirstName = " + user.FirstName);
            Console.WriteLine("LastName = " + user.LastName);
            Console.WriteLine("Email = " + user.Email);
        }

        static public void PrintMeetingInfo(Meeting meeting)
        {
            Console.WriteLine("******Meeting info:");
            Console.WriteLine("Id = " + meeting.Id);
            Console.WriteLine("Owner = " + meeting.Owner.Email);
            Console.WriteLine("Date = " + meeting.Date);
            Console.WriteLine("Title = " + meeting.Title);
            Console.WriteLine("Description = " + meeting.Description);
            Console.WriteLine("******Place info:");
            Console.WriteLine("Id = " + meeting.Place.Id);
            Console.WriteLine("Address = " + meeting.Place.Address);
            Console.WriteLine("Description = " + meeting.Place.Description);
            Console.WriteLine("******Invited users:");
            foreach(User user in meeting.InvitedPeople)
            {
                Console.WriteLine(user.Email);
            }
        }

        static public void ClearDB()
        {
            DatabaseConnector.PushCommandToDatabase
                (new SqlConnection(Properties.Resources.ConnectionString), CommandList.BuildClearDBCommand());
        }
    }
}
