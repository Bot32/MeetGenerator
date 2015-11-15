using MeetGenerator.Model.Models;
using MeetGenerator.Repository.SQL.Repositories.ObjectBuilder;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetGenerator.Repository.SQL.Repositories
{
    public static class DatabaseConnector
    {
        static public void PushDataToDatabase(SqlConnection sqlConnection, SqlCommand command)
        {
            try
            {
                command.Connection = sqlConnection;
                sqlConnection.Open();
                using (command)
                {
                    command.ExecuteNonQuery();
                }
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        static public T GetDataFromDatabase<T>
            (SqlConnection sqlConnection, SqlCommand command, IBuilder<T> builder)
        {
            try
            {
                command.Connection = sqlConnection;
                sqlConnection.Open();
                using (command)
                {
                    var reader = command.ExecuteReader();
                    T obj = builder.Build(reader);
                    return obj;
                }
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
