using MeetGenerator.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace MeetGenerator.Repository.SQL.Repositories.ObjectBuilders
{
    class AllMeetingsIdCreatedByUser : IBuilder<List<Meeting>>
    {
        public List<Meeting> Build(SqlDataReader reader)
        {
            List<Meeting> meetingsIdCreatedByUser = new List<Meeting>();
            while (reader.Read())
            {
                meetingsIdCreatedByUser.Add(
                    new Meeting
                    {
                        Id = reader.GetGuid(reader.GetOrdinal("ID")),
                    }
                );
            }
            return meetingsIdCreatedByUser;
        }
    }
}
