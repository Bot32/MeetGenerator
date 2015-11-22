using MeetGenerator.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using NLog;

namespace MeetGenerator.Repository.SQL.Repositories.ObjectBuilders
{
    class MeetingBuilder : IBuilder<Meeting>
    {
        Logger _logger = LogManager.GetCurrentClassLogger();

        public Meeting Build(SqlDataReader reader)
        {
            _logger.Trace("Begin reading Meeting object from SqlDataReader.");

            Meeting meeting = null;

            if (reader.Read())
            {
                meeting = new Meeting();
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
                meeting.InvitedPeople = new Dictionary<Guid, User>();
            }

            if (meeting != null)
            {
                _logger.Trace("End reading Meeting object from SqlDataReader. Meeting object: " + 
                    "ID = {0}, Title = {1}, Owner.ID = {2}, Owner.Email = {3}, Place.ID = {4}, Place.Address = {5}",
                    meeting.Id, meeting.Title, meeting.Owner.Id, meeting.Owner.Email, meeting.Place.Id, meeting.Place.Address);
            }
            else
            {
                _logger.Trace("End reading Meeting object from SqlDataReader. Meeting object is null.");
            }

            return meeting;
        }
    }
}
