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
using NLog;

namespace MeetGenerator.Repository.SQL.Repositories
{
    public class MeetingRepository : IMeetingRepository
    {
        SqlConnection sqlConnection;
        Logger _logger = LogManager.GetCurrentClassLogger();

        public MeetingRepository(string connectionString)
        {
            sqlConnection = new SqlConnection(connectionString);
        }


        public void CreateMeeting(Meeting meeting)
        {
            Log("Begin creating Meeting in database. Meeting object", meeting);

            DatabaseConnector.PushCommandToDatabase
                (sqlConnection, CommandList.Build_CreateMeetingCommand(meeting));

            Log("End creating Meeting in database. Meeting object", meeting);
        }

        public Meeting GetMeeting(Guid meetingId)
        {
            Log("Begin receiving Meeting from database. Meeting ID = " + meetingId);

            Meeting meeting = DatabaseConnector.GetDataFromDatabase<Meeting>
                (sqlConnection, CommandList.Build_GetMeetingCommand(meetingId), new MeetingBuilder());
            if (meeting != null)  meeting.InvitedPeople = GetAllUsersInvitedToMeeting(meeting.Id);

            Log("End receiving Meeting from database. Meeting object", meeting);

            return meeting;
        }

        public void InviteUserToMeeting(Guid userId, Guid meetingId)
        {
            Log("Begin creating Invitation User to Meeting in database. Meeting ID = " + meetingId + ". User ID = " + userId);

            DatabaseConnector.PushCommandToDatabase
                (sqlConnection, CommandList.Build_InviteUserToMeetingCommand(userId, meetingId));

            Log("End creating Invitation User to Meeting in database. Meeting ID = " + meetingId + ". User ID = " + userId);
        }

        public Dictionary<Guid, User> GetAllUsersInvitedToMeeting(Guid meetingId)
        {
            Log("Begin receiving all users, invited to meeting from database. Meeting ID = " + meetingId);

            Dictionary<Guid, User> allUsersInvitedToMeeting = DatabaseConnector.GetDataFromDatabase<Dictionary<Guid, User>>
                (sqlConnection, CommandList.Build_GetAllUsersIdInvitedToMeetingCommand(meetingId), 
                new AllUsersInvitedToMeetingIdListBuilder());

            Log("End receiving all users, invited to meeting from database. Meeting ID = " + 
                meetingId + ". Received " + allUsersInvitedToMeeting.Count + " invitations.");

            return allUsersInvitedToMeeting;
        }

        public void DeleteMeeting(Meeting meeting)
        {
            Log("Begin deleting Meeting from database. Meeting object ", meeting);

            DatabaseConnector.PushCommandToDatabase(sqlConnection, CommandList.Build_DeleteMeetingCommand(meeting));

            Log("End deleting Meeting from database. Meeting object ", meeting);
        }

        public void UpdateMeetingInfo(Meeting meeting)
        {
            Log("Begin updating Meeting in database. Meeting object ", meeting);

            DatabaseConnector.PushCommandToDatabase(sqlConnection, CommandList.Build_UpdateMeetingCommand(meeting));

            Log("End updating Meeting in database. Meeting object ", meeting);
        }

        public List<Meeting> GetAllMeetingsCreatedByUser(Guid userId)
        {
            List<Meeting> allMeetingsCreatedByUser = new List<Meeting>();

            Log("Begin receiving all Meetings, created by User, from database. User ID = " + userId);

            allMeetingsCreatedByUser = DatabaseConnector.GetDataFromDatabase<List<Meeting>>
                (sqlConnection, CommandList.Build_GetAllMeetingsIdCreatedByUserCommand(userId), new AllMeetingsIdCreatedByUser());

            for (int i = 0; i < allMeetingsCreatedByUser.Count; i++)
            {
                allMeetingsCreatedByUser[i] = GetMeeting(allMeetingsCreatedByUser[i].Id);
            }

            Log("End receiving all Meetings, created by User, from database. " + 
                allMeetingsCreatedByUser.Count + "Meetings received.");

            return allMeetingsCreatedByUser;
        }

        void Log(String logMessage)
        {
            _logger.Debug(logMessage);
        }

        void Log(String logMessage, Meeting meeting)
        {
            if (meeting != null)
            {
                _logger.Debug("{0}: ID = {1}, Title = {2}, Owner.ID = {3}, Owner.Email = {4}, Place.ID = {5}, Place.Address = {6}.",
                    logMessage, meeting.Id, meeting.Title, meeting.Owner.Id, meeting.Owner.Email, meeting.Place.Id, meeting.Place.Address);
            }
            else
            {
                _logger.Debug("{0} is null.", logMessage);
            }
        }
    }
}
