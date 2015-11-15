using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetGenerator.Model.Models;
using MeetGenerator.Model.Repositories;
using System.Data.SqlClient;

namespace MeetGenerator.Repository.SQL.Repositories
{
    public class MeetingRepository : IMeetingRepository
    {
        SqlConnection sqlConnection;

        public MeetingRepository(string connectionString)
        {
            sqlConnection = new SqlConnection(connectionString);
        }

        public void CreateMeeting(Meeting meeting)
        {
            throw new NotImplementedException();
        }

        public void DeleteMeeting(Guid meetingId)
        {
            throw new NotImplementedException();
        }

        public List<Meeting> GetAllMeetingsCreatedByUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Meeting GetMeeting(Guid meetingId)
        {
            throw new NotImplementedException();
        }

        public void InviteUserToMeeting(Guid userId)
        {
            throw new NotImplementedException();
        }

        public void UpdateMeetingInfo(Meeting meeting)
        {
            throw new NotImplementedException();
        }
    }
}
