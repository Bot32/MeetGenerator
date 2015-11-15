using MeetGenerator.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace MeetGenerator.Repository.SQL.Repositories.ObjectBuilders
{
    class MeetingBuilder : IBuilder<Meeting>
    {
        public Meeting Build(SqlDataReader reader)
        {
            if (reader.Read())
            {
                Meeting meeting = new Meeting();
                meeting.Id = reader.GetGuid(reader.GetOrdinal("MeetingId"));
                meeting.Owner = new User()
                {
                    Id = reader.GetGuid(reader.GetOrdinal("OwnerID")),
                    FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                    LastName = reader.GetString(reader.GetOrdinal("LastName")),
                    Email = reader.GetString(reader.GetOrdinal("Email"))
                };
                meeting.Date = reader.GetDateTime(reader.GetOrdinal("Date"));
                meeting.Title = reader.GetString(reader.GetOrdinal("Title"));
                meeting.Description = reader.GetString(reader.GetOrdinal("Description"));
                meeting.Place = new Place()
                {
                    Id = reader.GetGuid(reader.GetOrdinal("PlaceID")),
                    Address = reader.GetString(reader.GetOrdinal("Address")),
                    Description = reader.GetString(reader.GetOrdinal("Description"))
                };
                meeting.InvitedPeople = new List<User>();

                return meeting;
            }
            return null;
        }
    }
}
