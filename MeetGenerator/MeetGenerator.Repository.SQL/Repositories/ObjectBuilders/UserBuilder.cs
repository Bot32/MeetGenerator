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
    public class UserBuilder : IBuilder<User>
    {
        Logger _logger = LogManager.GetCurrentClassLogger();

        public User Build(SqlDataReader reader)
        {
            User user = null;

            _logger.Trace("Begin reading User object from SqlDataReader.");

            if (reader.Read())
            {
                user = new User
                {
                    Id = reader.GetGuid(reader.GetOrdinal("ID")),
                    FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                    LastName = reader.GetString(reader.GetOrdinal("LastName")),
                    Email = reader.GetString(reader.GetOrdinal("Email"))
                };
            }

            if (user != null)
            {
                _logger.Trace("End reading User object from SqlDataReader. User object: " + 
                              "ID = {0}, Email = {1}, First name = {2}, Last name = {3}.",
                              user.Id, user.Email, user.FirstName, user.LastName);
            }
            else
            {
                _logger.Trace("End reading User object from SqlDataReader.User object is null.");
            }

            return user;
        }
    }
}
