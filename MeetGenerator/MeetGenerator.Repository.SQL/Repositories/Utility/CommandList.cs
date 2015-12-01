using MeetGenerator.Model.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetGenerator.Repository.SQL.Repositories.Utility
{
    public static class CommandList
    {
        static Logger _logger = LogManager.GetCurrentClassLogger();

        public static SqlCommand Build_CreateUserCommand(User user)
        {
            SqlCommand command = new SqlCommand();

            Log("create user");

            command.CommandText = "insert into [dbo].[User] (Id, FirstName, LastName, Email) values (@Id, @FirstName, @LastName, @Email)";
            command.Parameters.AddWithValue("@id", user.Id);
            command.Parameters.AddWithValue("@firstname", user.FirstName);
            command.Parameters.AddWithValue("@lastname", user.LastName);
            command.Parameters.AddWithValue("@email", user.Email);

            Log("create user", command);

            return command;
        }

        public static SqlCommand Build_GetUserByIdCommand(Guid userId)
        {
            SqlCommand command = new SqlCommand();

            Log("get user by ID");

            command.CommandText = "select id, firstname, lastname, email from [dbo].[User] where id = @id";
            command.Parameters.AddWithValue("@id", userId);

            Log("get user by ID", command);

            return command;
        }

        public static SqlCommand Build_UpdateUserCommand(User user)
        {
            SqlCommand command = new SqlCommand();

            Log("update user");

            command.CommandText = "update [dbo].[User] " +
                "SET FirstName = @FirstName, LastName = @LastName, Email = @Email " +
                "where id = @id";
            command.Parameters.AddWithValue("@id", user.Id);
            command.Parameters.AddWithValue("@firstname", user.FirstName);
            command.Parameters.AddWithValue("@lastname", user.LastName);
            command.Parameters.AddWithValue("@email", user.Email);

            Log("update user", command);

            return command;
        }

        public static SqlCommand Build_GetUserByEmailCommand(String email)
        {
            SqlCommand command = new SqlCommand();

            Log("get user by email");

            command.CommandText = "select id, firstname, lastname, email from [dbo].[User] where email = @email";
            command.Parameters.AddWithValue("@email", email);

            Log("get user by email", command);

            return command;
        }

        public static SqlCommand Build_DeleteUserCommand(Guid userId)
        {
            SqlCommand command = new SqlCommand();

            Log("delete user");

            command.CommandText = "delete from [dbo].[User] where id = @id";
            command.Parameters.AddWithValue("@id", userId);

            Log("delete user", command);

            return command;
        }

        public static SqlCommand Build_CreateMeetingCommand(Meeting meeting)
        {
            SqlCommand command = new SqlCommand();

            Log("create meeting");

            command.CommandText = "insert into [dbo].[Meeting] (ID, OwnerID, Date, Title, Description, PlaceID) " +
                                  "values (@Meeting_Id, @Meeting_OwnerID, @Meeting_Date, @Meeting_Title, @Meeting_Description, @Meeting_PlaceID);";

            command.Parameters.AddWithValue("@Meeting_Id", meeting.Id);
            command.Parameters.AddWithValue("@Meeting_OwnerID", meeting.Owner.Id);
            command.Parameters.AddWithValue("@Meeting_Date", meeting.Date);
            command.Parameters.AddWithValue("@Meeting_Title", meeting.Title);
            command.Parameters.AddWithValue("@Meeting_Description", meeting.Description);
            command.Parameters.AddWithValue("@Meeting_PlaceID", meeting.Place.Id);

            Log("create meeting", command);

            return command;
        }

        public static SqlCommand Build_GetMeetingCommand(Guid meetingId)
        {
            SqlCommand command = new SqlCommand();

            Log("get meeting");

            command.CommandText = "select m.id as MeetingId, m.Date, m.Title, m.Description, m.PlaceID, p.Address, p.Description, m.OwnerID, u.FirstName, u.LastName, u.Email " +
                                  "from [dbo].[Meeting] m " +
                                  "join [dbo].[User] u on u.ID = m.OwnerID " +
                                  "join [dbo].[Place] p on m.PlaceID = p.ID " +
                                  "where m.ID = @id";
            command.Parameters.AddWithValue("@id", meetingId);

            Log("get meeting", command);

            return command;
        }

        public static SqlCommand Build_GetMeetingPlaceCommand(Guid meetingId)
        {
            SqlCommand command = new SqlCommand();

            Log("get meeting place");

            command.CommandText = "select id, Address, Description from [dbo].[Place] where id = @id";
            command.Parameters.AddWithValue("@id", meetingId);

            Log("get meeting place", command);

            return command;
        }

        public static SqlCommand Build_InviteUserToMeetingCommand(Guid userId, Guid meetingId)
        {
            SqlCommand command = new SqlCommand();

            Log("invite user to meeting");

            command.CommandText = "insert into [dbo].[Invitations] (MeetingID, UserID) values (@MeetingID, @UserID)";
            command.Parameters.AddWithValue("@MeetingID", meetingId);
            command.Parameters.AddWithValue("@UserID", userId);

            Log("invite user to meeting", command);

            return command;
        }

        public static SqlCommand Build_DeleteInvitationUserToMeetingCommand(Guid userId, Guid meetingId)
        {
            SqlCommand command = new SqlCommand();

            Log("delete invitation user to meeting");

            command.CommandText = "delete from [dbo].[Invitations] where MeetingID = @MeetingID and UserID = @UserID";
            command.Parameters.AddWithValue("@MeetingID", meetingId);
            command.Parameters.AddWithValue("@UserID", userId);

            Log("delete invitation user to meeting", command);

            return command;
        }

        public static SqlCommand Build_GetAllUsersIdInvitedToMeetingCommand(Guid meetingId)
        {
            SqlCommand command = new SqlCommand();

            Log("get all users, invited to meeting");

            command.CommandText = "select m.ID as MeetingID, i.UserID as UserID, u.FirstName, u.LastName, u.Email " +
                                  "from dbo.Meeting m " +
                                  "join dbo.Invitations i ON m.ID = i.MeetingID " +
                                  "join dbo.[User] u ON i.UserID = u.ID " +
                                  "where m.ID = @MeetingID";
            command.Parameters.AddWithValue("@MeetingID", meetingId);

            Log("get all users, invited to meeting", command);

            return command;
        }

        public static SqlCommand Build_DeleteMeetingCommand(Meeting meeting)
        {
            SqlCommand command = new SqlCommand();

            Log("delete meeting");

            command.CommandText = "delete from [dbo].[Invitations] where MeetingID = @MeetingID; " +          
                                  "delete from [dbo].[Meeting] where ID = @meetingID";
            command.Parameters.AddWithValue("@meetingID", meeting.Id);

            Log("delete meeting", command);

            return command;
        }

        public static SqlCommand Build_UpdateMeetingCommand(Meeting meeting)
        {
            SqlCommand command = new SqlCommand();

            Log("update meeting");

            command.CommandText = "update [dbo].[Meeting] " +
                "SET Date = @Date, Title = @Title,  Description = @Description " +
                "where id = @meetingId";
            command.Parameters.AddWithValue("@meetingId", meeting.Id);
            //command.Parameters.AddWithValue("@OwnerID", meeting.Owner.Id);
            command.Parameters.AddWithValue("@Date", meeting.Date);
            command.Parameters.AddWithValue("@Title", meeting.Title);
            command.Parameters.AddWithValue("@Description", meeting.Description);
            //command.Parameters.AddWithValue("@PlaceID", meeting.Place.Id);

            Log("update meeting", command);

            return command;
        }

        public static SqlCommand Build_CreatePlaceCommand(Place place)
        {
            SqlCommand command = new SqlCommand();

            Log("create place");

            command.CommandText = "insert into [dbo].[Place] (ID, Address, Description) " +
                                  "values (@Place_Id, @Place_Address, @Place_Description); ";

            command.Parameters.AddWithValue("@Place_Id", place.Id);
            command.Parameters.AddWithValue("@Place_Address", place.Address);
            command.Parameters.AddWithValue("@Place_Description", place.Description);

            Log("create place", command);

            return command;
        }

        public static SqlCommand Build_GetPlaceCommand(Guid placeId)
        {
            SqlCommand command = new SqlCommand();

            Log("get place");

            command.CommandText = "select id, Address, Description from [dbo].[Place] where id = @placeId";
            command.Parameters.AddWithValue("@placeId", placeId);

            Log("get place", command);

            return command;
        }

        public static SqlCommand Build_DeletePlaceCommand(Guid placeId)
        {
            SqlCommand command = new SqlCommand();

            Log("delete place");

            command.CommandText = "delete from [dbo].[Place] where ID = @PlaceID;";
            command.Parameters.AddWithValue("@PlaceID", placeId);

            Log("delete place", command);

            return command;
        }

        public static SqlCommand Build_UpdatePlaceCommand(Place place)
        {
            SqlCommand command = new SqlCommand();

            Log("update place");

            command.CommandText = "update [dbo].[Place] " +
                "SET Address = @Address, Description = @Description " +
                "where id = @placeId";
            command.Parameters.AddWithValue("@placeId", place.Id);
            command.Parameters.AddWithValue("@Address", place.Address);
            command.Parameters.AddWithValue("@Description", place.Description);

            Log("update place", command);

            return command;
        }

        public static SqlCommand Build_GetAllMeetingsIdCreatedByUserCommand(Guid userId)
        {
            SqlCommand command = new SqlCommand();

            Log("get all meetings ID, created by user");

            command.CommandText = "select m.ID " +
                                  "from [dbo].[Meeting] m " +
                                  "join [dbo].[User] u on u.ID = m.OwnerID " +
                                  "where u.ID = @userId";
            command.Parameters.AddWithValue("@userId", userId);

            Log("get all meetings ID, created by user", command);

            return command;
        }

        public static SqlCommand BuildClearDBCommand()
        {
            SqlCommand command = new SqlCommand();

            command.CommandText = "delete from[dbo].[Invitations]; " +
                                  "delete from[dbo].[Meeting]; " +
                                  "delete from[dbo].[UserSocialLink]; " +
                                  "delete from[dbo].[User]; " +
                                  "delete from[dbo].[Place]; ";
            return command;
        }

        static void Log(String logMessage)
        {
            _logger.Trace("Start build {0} sql command.", logMessage);
        }
        static void Log(String logMessage, SqlCommand command)
        {
            _logger.Trace("End build {0} sql command. Commant text: {1}.", logMessage, command.CommandText);
        }

    }
}
