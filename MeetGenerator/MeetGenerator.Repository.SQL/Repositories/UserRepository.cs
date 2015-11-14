using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetGenerator;
using MeetGenerator.Model.Repositories;
using MeetGenerator.Models;
using System.Data.SqlClient;
using MeetGenerator.Repository.SQL.DataValidators;

namespace MeetGenerator.Repository.SQL
{
    public class UserRepository : IUserRepository
    {
        SqlConnection sqlConnection;

        public UserRepository (String connectionString)
        {
            sqlConnection = new SqlConnection(connectionString);
        }


        public void Create(User user)
        {
            string errorsList = UserDataValidator.IsCompleteValidUserObject(user);
            if (errorsList == "OK")
            {
                PushUserToDatabase(user);
            }
        }

        void PushUserToDatabase(User user)
        {
            try
            {
                sqlConnection.Open();
                using (var command = sqlConnection.CreateCommand())
                {
                    BuildCreateUserCommand(command, user);

                    command.ExecuteNonQuery();
                }
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        void BuildCreateUserCommand(SqlCommand command, User user)
        {
            command.CommandText = "insert into [dbo].[User] (Id, FirstName, LastName, Email) values (@Id, @FirstName, @LastName, @Email)";
            command.Parameters.AddWithValue("@id", user.Id);
            command.Parameters.AddWithValue("@firstname", user.FirstName);
            command.Parameters.AddWithValue("@lastname", user.LastName);
            command.Parameters.AddWithValue("@email", user.Email);
        }


        public User Get(Guid id)
        {
            return GetUserFromDatabase(id);
        }

        User GetUserFromDatabase(Guid id)
        {
            User user;

            try
            {
                sqlConnection.Open();
                using (var command = sqlConnection.CreateCommand())
                {
                    BuildGetUserCommand(command, id);
                    user = GetUserFromSqlReader(command);
                }
                return user;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        User GetUserFromSqlReader(SqlCommand command)
        {
            User user;

            using (var reader = command.ExecuteReader())
            {
                reader.Read();
                user = new User
                {
                    Id = reader.GetGuid(reader.GetOrdinal("ID")),
                    FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                    LastName = reader.GetString(reader.GetOrdinal("LastName")),
                    Email = reader.GetString(reader.GetOrdinal("Email"))
                };
            }

            return user;
        }
        void BuildGetUserCommand(SqlCommand command, Guid id)
        {
            command.CommandText = "select id, firstname, lastname, email from [dbo].[User] where id = @id";
            command.Parameters.AddWithValue("@id", id);
        }


        public void Update(User user)
        {
            UpdateUserInDatabase(user);
        }

        void UpdateUserInDatabase(User user)
        {
            try
            {
                sqlConnection.Open();
                using (var command = sqlConnection.CreateCommand())
                {
                    BuildUpdateUserCommand(command, user);

                    command.ExecuteNonQuery();
                }
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        void BuildUpdateUserCommand(SqlCommand command, User user)
        {
            command.CommandText = "update [dbo].[User] " +
                "SET FirstName = @FirstName, LastName = @LastName, Email = @Email " + 
                "where id = @id";
            command.Parameters.AddWithValue("@id", user.Id);
            command.Parameters.AddWithValue("@firstname", user.FirstName);
            command.Parameters.AddWithValue("@lastname", user.LastName);
            command.Parameters.AddWithValue("@email", user.Email);
        }


        public void Delete(Guid id)
        {
            DeleteUserFromDatabase(id); 
        }

        void DeleteUserFromDatabase(Guid id)
        {
            try
            {
                sqlConnection.Open();
                using (var command = sqlConnection.CreateCommand())
                {
                    BuildDeleteUserCommand(command, id);
                    command.ExecuteNonQuery();
                }
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        void BuildDeleteUserCommand(SqlCommand command, Guid id)
        {
            command.CommandText = "delete from [dbo].[User] where id = @id";
            command.Parameters.AddWithValue("@id", id);
        }


        public bool IsUserExist(Guid id)
        {
            return CheckUserInDatabase(id);
        }

        bool CheckUserInDatabase(Guid id)
        {
            try
            {
                sqlConnection.Open();
                using (var command = sqlConnection.CreateCommand())
                {
                    BuildCheckUserCommand(command, id);
                    return CheckEmailInSqlReader(command);
                }
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        bool CheckUserInSqlReader(SqlCommand command)
        {
            using (var reader = command.ExecuteReader())
            {
                return reader.Read();
            }
        }
        void BuildCheckUserCommand(SqlCommand command, Guid id)
        {
            command.CommandText = "select id from [dbo].[User] where id = @id";
            command.Parameters.AddWithValue("@id", id);
        }


        public bool IsEmailBusy(string email)
        {
            return CheckEmailInDatabase(email);
        }

        bool CheckEmailInDatabase(String email)
        {
            try
            {
                sqlConnection.Open();
                using (var command = sqlConnection.CreateCommand())
                {
                    BuildCheckEmailCommand(command, email);
                    return CheckEmailInSqlReader(command);
                }
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        bool CheckEmailInSqlReader(SqlCommand command)
        {
            using (var reader = command.ExecuteReader())
            {
                return reader.Read();
            }
        }
        void BuildCheckEmailCommand(SqlCommand command, String email)
        {
            command.CommandText = "select email from [dbo].[User] where email = @email";
            command.Parameters.AddWithValue("@email", email);
        }
    }
}
