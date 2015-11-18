using MeetGenerator.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace MeetGenerator.Repository.SQL.Repositories.ObjectBuilders
{
    class AllUsersInvitedToMeetingIdListBuilder : IBuilder<Dictionary<Guid, User>>
    {
        public Dictionary<Guid, User> Build(SqlDataReader reader)
        {
            Dictionary<Guid, User> invitedUsersIdList = new Dictionary<Guid, User>();
            while (reader.Read())
            {
                invitedUsersIdList.Add(
                    reader.GetGuid(reader.GetOrdinal("UserID")),
                    new User
                    {
                        Id = reader.GetGuid(reader.GetOrdinal("UserID")),
                        FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                        LastName = reader.GetString(reader.GetOrdinal("LastName")),
                        Email = reader.GetString(reader.GetOrdinal("Email"))
                    }
                );
            }
            return invitedUsersIdList;
        }
    }
}
