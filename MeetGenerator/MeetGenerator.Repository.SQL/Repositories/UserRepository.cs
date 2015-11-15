using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetGenerator;
using MeetGenerator.Model.Repositories;
using MeetGenerator.Model.Models;
using System.Data.SqlClient;
using MeetGenerator.Repository.SQL.DataValidators;
using MeetGenerator.Repository.SQL.Repositories;
using MeetGenerator.Repository.SQL.Repositories.ObjectBuilder;

namespace MeetGenerator.Repository.SQL
{
    public class UserRepository : IUserRepository
    {
        SqlConnection sqlConnection;

        public UserRepository (String connectionString)
        {
            sqlConnection = new SqlConnection(connectionString);
        }


        public void CreateUser(User user)
        {
            string errorsList = UserDataValidator.IsCompleteValidUserObject(user);
            if (errorsList == "OK")
            {
                DatabaseConnector.PushDataToDatabase(sqlConnection, BuildCreateUserCommand(user));
            } else
            {
                Console.WriteLine(errorsList);
            }
        }
        SqlCommand BuildCreateUserCommand(User user)
        {
            SqlCommand command = new SqlCommand();

            command.CommandText = "insert into [dbo].[User] (Id, FirstName, LastName, Email) values (@Id, @FirstName, @LastName, @Email)";
            command.Parameters.AddWithValue("@id", user.Id);
            command.Parameters.AddWithValue("@firstname", user.FirstName);
            command.Parameters.AddWithValue("@lastname", user.LastName);
            command.Parameters.AddWithValue("@email", user.Email);

            return command;
        }


        public User GetUser(Guid id)
        {
            return DatabaseConnector.GetDataFromDatabase<User>
                (sqlConnection, BuildGetUserByIdCommand(id), new UserBuilder());
        }
        SqlCommand BuildGetUserByIdCommand(Guid id)
        {
            SqlCommand command = new SqlCommand();

            command.CommandText = "select id, firstname, lastname, email from [dbo].[User] where id = @id";
            command.Parameters.AddWithValue("@id", id);

            return command;
        }


        public User GetUser(String email)
        {
            return DatabaseConnector.GetDataFromDatabase<User>
                (sqlConnection, BuildGetUserByEmailCommand(email), new UserBuilder());
        }
        SqlCommand BuildGetUserByEmailCommand(String email)
        {
            SqlCommand command = new SqlCommand();

            command.CommandText = "select id, firstname, lastname, email from [dbo].[User] where email = @email";
            command.Parameters.AddWithValue("@email", email);

            return command;
        }


        public void UpdateUserInfo(User user)
        {
            string errorsList = UserDataValidator.IsCompleteValidUserObject(user);
            if (errorsList == "OK")
            {
                DatabaseConnector.PushDataToDatabase(sqlConnection, BuildUpdateUserCommand(user));
            } else
            {
                Console.WriteLine(errorsList);
            }
        }
        SqlCommand BuildUpdateUserCommand(User user)
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


        public void DeleteUser(Guid id)
        {
            DatabaseConnector.PushDataToDatabase(sqlConnection, BuildDeleteUserCommand(id));
        }
        SqlCommand BuildDeleteUserCommand(Guid id)
        {
            SqlCommand command = new SqlCommand();

            command.CommandText = "delete from [dbo].[User] where id = @id";
            command.Parameters.AddWithValue("@id", id);

            return command;
        }
    }
}
