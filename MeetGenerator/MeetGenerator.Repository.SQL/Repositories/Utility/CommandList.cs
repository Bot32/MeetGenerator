using MeetGenerator.Model.Models;
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
        public static SqlCommand Build_CreateUserCommand(User user)
        {
            SqlCommand command = new SqlCommand();

            command.CommandText = "insert into [dbo].[User] (Id, FirstName, LastName, Email) values (@Id, @FirstName, @LastName, @Email)";
            command.Parameters.AddWithValue("@id", user.Id);
            command.Parameters.AddWithValue("@firstname", user.FirstName);
            command.Parameters.AddWithValue("@lastname", user.LastName);
            command.Parameters.AddWithValue("@email", user.Email);

            return command;
        }

        public static SqlCommand Build_GetUserByIdCommand(Guid userId)
        {
            SqlCommand command = new SqlCommand();

            command.CommandText = "select id, firstname, lastname, email from [dbo].[User] where id = @id";
            command.Parameters.AddWithValue("@id", userId);

            return command;
        }

        public static SqlCommand Build_UpdateUserCommand(User user)
        {
            SqlCommand command = new SqlCommand();

            command.CommandText = "update [dbo].[User] " +
                "SET FirstName = @FirstName, LastName = @LastName, Email = @Email " +
                "where id = @id";
            command.Parameters.AddWithValue("@id", user.Id);
            command.Parameters.AddWithValue("@firstname", user.FirstName);
            command.Parameters.AddWithValue("@lastname", user.LastName);
            command.Parameters.AddWithValue("@email", user.Email);

            return command;
        }

        public static SqlCommand Build_GetUserByEmailCommand(String email)
        {
            SqlCommand command = new SqlCommand();

            command.CommandText = "select id, firstname, lastname, email from [dbo].[User] where email = @email";
            command.Parameters.AddWithValue("@email", email);

            return command;
        }

        public static SqlCommand Build_DeleteUserCommand(Guid userId)
        {
            SqlCommand command = new SqlCommand();

            command.CommandText = "delete from [dbo].[User] where id = @id";
            command.Parameters.AddWithValue("@id", userId);

            return command;
        }

        public static SqlCommand Build_CreateMeetingCommand(Meeting meeting)
        {
            SqlCommand command = new SqlCommand();

            command.CommandText = "insert into [dbo].[Place] (ID, Address, Description) " +
                                  "values (@Place_Id, @Place_Address, @Place_Description); " +
                                  "insert into [dbo].[Meeting] (ID, OwnerID, Date, Title, Description, PlaceID) " +
                                  "values (@Meeting_Id, @Meeting_OwnerID, @Meeting_Date, @Meeting_Title, @Meeting_Description, @Meeting_PlaceID);";

            command.Parameters.AddWithValue("@Meeting_Id", meeting.Id);
            command.Parameters.AddWithValue("@Meeting_OwnerID", meeting.Owner.Id);
            command.Parameters.AddWithValue("@Meeting_Date", meeting.Date);
            command.Parameters.AddWithValue("@Meeting_Title", meeting.Title);
            command.Parameters.AddWithValue("@Meeting_Description", meeting.Description);
            command.Parameters.AddWithValue("@Meeting_PlaceID", meeting.Place.Id);

            command.Parameters.AddWithValue("@Place_Id", meeting.Place.Id);
            command.Parameters.AddWithValue("@Place_Address", meeting.Place.Address);
            command.Parameters.AddWithValue("@Place_Description", meeting.Place.Description);

            return command;
        }

        public static SqlCommand Build_GetMeetingCommand(Guid meetingId)
        {
            SqlCommand command = new SqlCommand();

            command.CommandText = "select m.id as MeetingId, m.Date, m.Title, m.Description, m.PlaceID, p.Address, p.Description, m.OwnerID, u.FirstName, u.LastName, u.Email " +
                                  "from [dbo].[Meeting] m " +
                                  "join [dbo].[User] u on u.ID = m.OwnerID " +
                                  "join [dbo].[Place] p on m.PlaceID = p.ID " +
                                  "where m.ID = @id";
            command.Parameters.AddWithValue("@id", meetingId);

            return command;
        }

        public static SqlCommand Build_GetMeetingPlaceCommand(Guid meetingId)
        {
            SqlCommand command = new SqlCommand();

            command.CommandText = "select id, Address, Description from [dbo].[Place] where id = @id";
            command.Parameters.AddWithValue("@id", meetingId);

            return command;
        }

        public static SqlCommand Build_InviteUserToMeetingCommand(Guid userId, Guid meetingId)
        {
            SqlCommand command = new SqlCommand();

            command.CommandText = "insert into [dbo].[Invitations] (MeetingID, UserID) values (@MeetingID, @UserID)";
            command.Parameters.AddWithValue("@MeetingID", meetingId);
            command.Parameters.AddWithValue("@UserID", userId);

            return command;
        }

        public static SqlCommand Build_GetAllUsersInvitedToMeetingCommand(Guid meetingId)
        {
            SqlCommand command = new SqlCommand();

            command.CommandText = "select m.ID as MeetingID, i.UserID as UserID, u.FirstName, u.LastName, u.Email " +
                                  "from dbo.Meeting m " +
                                  "join dbo.Invitations i ON m.ID = i.MeetingID " +
                                  "join dbo.[User] u ON i.UserID = u.ID " +
                                  "where m.ID = @MeetingID";
            command.Parameters.AddWithValue("@MeetingID", meetingId);

            return command;
        }

        public static SqlCommand Build_DeleteMeetingCommand(Meeting meeting)
        {
            SqlCommand command = new SqlCommand();

            command.CommandText = "delete from [dbo].[Invitations] where MeetingID = @MeetingID; " +          
                                  "delete from [dbo].[Meeting] where ID = @meetingID";
            command.Parameters.AddWithValue("@meetingID", meeting.Id);

            return command;
        }

        public static SqlCommand Build_UpdateMeetingCommand(Meeting meeting)
        {
            SqlCommand command = new SqlCommand();

            command.CommandText = "update [dbo].[Meeting] " +
                "SET Date = @Date, Title = @Title,  Description = @Description " +
                "where id = @meetingId";
            command.Parameters.AddWithValue("@meetingId", meeting.Id);
            //command.Parameters.AddWithValue("@OwnerID", meeting.Owner.Id);
            command.Parameters.AddWithValue("@Date", meeting.Date);
            command.Parameters.AddWithValue("@Title", meeting.Title);
            command.Parameters.AddWithValue("@Description", meeting.Description);
            //command.Parameters.AddWithValue("@PlaceID", meeting.Place.Id);

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

    }
}
