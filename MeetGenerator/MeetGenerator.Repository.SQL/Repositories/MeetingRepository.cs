using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetGenerator.Model.Models;
using MeetGenerator.Model.Repositories;
using System.Data.SqlClient;
using MeetGenerator.Repository.SQL.Repositories.ObjectBuilders;
using MeetGenerator.Repository.SQL.Repositories.Utility;

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
            DatabaseConnector.PushCommandToDatabase
                (sqlConnection, CommandList.Build_CreateMeetingCommand(meeting));
        }


        public Meeting GetMeeting(Guid meetingId)
        {
            Meeting meeting = DatabaseConnector.GetDataFromDatabase<Meeting>
                (sqlConnection, CommandList.Build_GetMeetingCommand(meetingId), new MeetingBuilder());
            if (meeting != null)  meeting.InvitedPeople = GetAllUsersInvitedToMeeting(meeting.Id);

            return meeting;
        }


        public void InviteUserToMeeting(Guid userId, Guid meetingId)
        {
            DatabaseConnector.PushCommandToDatabase
                (sqlConnection, CommandList.Build_InviteUserToMeetingCommand(userId, meetingId));
        }


        public List<User> GetAllUsersInvitedToMeeting(Guid meetingId)
        {
            return DatabaseConnector.GetDataFromDatabase<List<User>>
                (sqlConnection, CommandList.Build_GetAllUsersInvitedToMeetingCommand(meetingId), 
                new AllUsersInvitedToMeetingListBuilder());
        }


        public void DeleteMeeting(Meeting meeting)
        {
            //Чтобы удалить Meeting, нужно удалить все Invitations, где есть этот Meeting
            DatabaseConnector.PushCommandToDatabase(sqlConnection, CommandList.Build_DeleteMeetingCommand(meeting));
        }


        public void UpdateMeetingInfo(Meeting meeting)
        {
            DatabaseConnector.PushCommandToDatabase(sqlConnection, CommandList.Build_UpdateMeetingCommand(meeting));
        }


        public List<Meeting> GetAllMeetingsCreatedByUser(Guid userId)
        {
            List<Meeting> allMeetingsCreatedByUser = new List<Meeting>();

            allMeetingsCreatedByUser = DatabaseConnector.GetDataFromDatabase<List<Meeting>>
                (sqlConnection, CommandList.Build_GetAllMeetingsIdCreatedByUserCommand(userId), new AllMeetingsIdCreatedByUser());

            for (int i = 0; i < allMeetingsCreatedByUser.Count; i++)
            {
                Meeting meeting = allMeetingsCreatedByUser[i];
                meeting = GetMeeting(meeting.Id);
            }

            return allMeetingsCreatedByUser;
        }

    }
}
