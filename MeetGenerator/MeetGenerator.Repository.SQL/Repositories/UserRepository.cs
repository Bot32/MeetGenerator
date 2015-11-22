using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetGenerator;
using MeetGenerator.Model.Repositories;
using MeetGenerator.Model.Models;
using System.Data.SqlClient;
using MeetGenerator.Repository.SQL.Repositories;
using MeetGenerator.Repository.SQL.Repositories.ObjectBuilders;
using MeetGenerator.Repository.SQL.Repositories.Utility;
using NLog;

namespace MeetGenerator.Repository.SQL.Repositories
{
    public class UserRepository : IUserRepository
    {
        SqlConnection sqlConnection;
        Logger _logger = LogManager.GetCurrentClassLogger();

        public UserRepository (String connectionString)
        {
            sqlConnection = new SqlConnection(connectionString);
        }


        public void CreateUser(User user)
        {
            Log("Begin creating User in database. User object", user);

            DatabaseConnector.PushCommandToDatabase
                (sqlConnection, CommandList.Build_CreateUserCommand(user));

            Log("End creating User in database. User object", user);
        }

        public User GetUser(Guid id)
        {
            Log("Begin receiving User by ID from database. User id = " + id.ToString());

            User user = DatabaseConnector.GetDataFromDatabase<User>
                (sqlConnection, CommandList.Build_GetUserByIdCommand(id), new UserBuilder());

            Log("End receiving User from database. User object", user);

            return user;
        }

        public User GetUser(String email)
        {
            Log("Begin receiving User by email from database. User email = " + email);

            User user = DatabaseConnector.GetDataFromDatabase<User>
                (sqlConnection, CommandList.Build_GetUserByEmailCommand(email), new UserBuilder());

            Log("End receiving User by email from database. User object", user);

            return user;
        }

        public void UpdateUser(User user)
        {
            Log("Begin updating User in database. User object", user);

            DatabaseConnector.PushCommandToDatabase
                (sqlConnection, CommandList.Build_UpdateUserCommand(user));

            Log("End updating User in database. User object", user);
        }

        public void DeleteUser(Guid id)
        {
            Log("Begin deleting User in database. User ID = " + id);

            DatabaseConnector.PushCommandToDatabase(sqlConnection, CommandList.Build_DeleteUserCommand(id));

            Log("End deleting User in database. User ID = " + id);
        }

        void Log(String logMessage)
        {
            _logger.Debug(logMessage);
        }

        void Log(String logMessage, User user)
        {
            if (user != null)
            {
                _logger.Debug("{0}: ID = {1}, Email = {2}, First name = {3}, Last name = {4}.",
                logMessage, user.Id, user.Email, user.FirstName, user.LastName);
            }
            else
            {
                _logger.Debug("{0} is null.", logMessage);
            }
        }
    }
}