using MeetGenerator.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace MeetGenerator.Repository.SQL.Repositories.ObjectBuilders
{
    class AllUsersInvitedToMeetingListBuilder : IBuilder<List<User>>
    {
        public List<User> Build(SqlDataReader reader)
        {
            List<User> invitedUsersIdList = new List<User>();
            while (reader.Read())
            {
                invitedUsersIdList.Add( 
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
