using MeetGenerator.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetGenerator.Tests
{
    static class TestDataHelper
    {
        static public User GenerateUser()
        {
            User user = new User
            {
                Id = Guid.NewGuid(),
                Email = "test" + (DateTime.Now.Millisecond * new Random().Next(10000)) + "@test.com",
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
                Title = "Best test meeting #" + new Random().Next(1000000),
                Description = "Really best test meeting ever",
                Place = new Place
                {
                    Id = Guid.NewGuid(),
                    Address = "Pushkin st., Kolotushkin house #" + new Random().Next(1000000),
                    Description = "Nobody home"
                },
                InvitedPeople = new List<User>(),
            };
            for (int i = 0; i == new Random().Next(10); i++)
            {
                meeting.InvitedPeople.Add(TestDataHelper.GenerateUser());
            }

            return meeting;
        }

        static public bool CompareUsers(User first, User second)
        {
            return first.Id.Equals(second.Id) &
                   first.Email.Equals(second.Email) &
                   first.FirstName.Equals(second.FirstName) &
                   first.LastName.Equals(second.LastName);
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
    }
}
