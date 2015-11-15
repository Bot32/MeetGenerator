using MeetGenerator.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace MeetGenerator.Repository.SQL.Repositories.ObjectBuilder
{
    public class UserBuilder : IBuilder<User>
    {
        public User Build(SqlDataReader reader)
        {
            if (reader.Read())
            {
                return new User
                {
                    Id = reader.GetGuid(reader.GetOrdinal("ID")),
                    FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                    LastName = reader.GetString(reader.GetOrdinal("LastName")),
                    Email = reader.GetString(reader.GetOrdinal("Email"))
                };
            }
            return null;
        }
    }
}
