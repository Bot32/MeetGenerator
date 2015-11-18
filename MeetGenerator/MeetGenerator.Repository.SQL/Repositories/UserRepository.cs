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

namespace MeetGenerator.Repository.SQL.Repositories
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
            DatabaseConnector.PushCommandToDatabase
                (sqlConnection, CommandList.Build_CreateUserCommand(user));
        }

        public User GetUser(Guid id)
        {
            return DatabaseConnector.GetDataFromDatabase<User>
                (sqlConnection, CommandList.Build_GetUserByIdCommand(id), new UserBuilder());
        }

        public User GetUser(String email)
        {
            return DatabaseConnector.GetDataFromDatabase<User>
                (sqlConnection, CommandList.Build_GetUserByEmailCommand(email), new UserBuilder());
        }

        public void UpdateUser(User user)
        {
            DatabaseConnector.PushCommandToDatabase
                (sqlConnection, CommandList.Build_UpdateUserCommand(user));
        }

        public void DeleteUser(Guid id)
        {
            DatabaseConnector.PushCommandToDatabase(sqlConnection, CommandList.Build_DeleteUserCommand(id));
        }
    }
}