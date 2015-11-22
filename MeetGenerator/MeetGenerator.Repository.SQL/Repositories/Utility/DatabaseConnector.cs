using MeetGenerator.Model.Models;
using MeetGenerator.Repository.SQL.Repositories.ObjectBuilders;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetGenerator.Repository.SQL.Repositories.Utility
{
    public static class DatabaseConnector
    {
        static Logger _logger = LogManager.GetCurrentClassLogger();
        static public void PushCommandToDatabase(SqlConnection sqlConnection, SqlCommand command)
        {
            _logger.Trace("Open sql connection with connection string: {0}.", sqlConnection.ConnectionString);
            try
            {
                command.Connection = sqlConnection;
                sqlConnection.Open();
                using (command)
                {
                    _logger.Trace("Begin execute sql command: {0}. Connection string: {1}.", 
                        command.CommandText, sqlConnection.ConnectionString);
                    command.ExecuteNonQuery();
                    _logger.Trace("End execute sql command: {0}. Connection string: {1}.",
                        command.CommandText, sqlConnection.ConnectionString);
                }
            }
            catch(Exception e)
            {
                _logger.Error(e, "Failed to execute sql command: {0}", command.CommandText);
            }
            finally
            {
                sqlConnection.Close();
                _logger.Trace("Close sql connection on connection string: {0}", sqlConnection.ConnectionString);
            }
        }

        static public T GetDataFromDatabase<T>
            (SqlConnection sqlConnection, SqlCommand command, IBuilder<T> builder)
        {
            _logger.Trace("Open sql connection with connection string: {0}.", sqlConnection.ConnectionString);
            try
            {
                command.Connection = sqlConnection;
                sqlConnection.Open();
                using (command)
                {
                    _logger.Trace("Begin execute sql command: {0}. Connection string: {1}.",
                        command.CommandText, sqlConnection.ConnectionString);

                    var reader = command.ExecuteReader();
                    T obj = builder.Build(reader);

                    _logger.Trace("End execute sql command: {0}. Connection string: {1}.",
                        command.CommandText, sqlConnection.ConnectionString);

                    return obj;
                }
            }
            catch(Exception e)
            {
                _logger.Error(e, "Failed to execute sql command: {0}", command.CommandText);
                return default(T);
            }
            finally
            {
                sqlConnection.Close();
                _logger.Trace("Close sql connection on connection string: {0}", sqlConnection.ConnectionString);
            }
        }
    }
}
