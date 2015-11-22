using MeetGenerator.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetGenerator.Model.Models;
using MeetGenerator.Repository.SQL.Repositories.Utility;
using System.Data.SqlClient;
using MeetGenerator.Repository.SQL.Repositories.ObjectBuilders;
using NLog;

namespace MeetGenerator.Repository.SQL.Repositories
{
    public class PlaceRepository : IPlaceRepository
    {
        SqlConnection sqlConnection;
        Logger _logger = LogManager.GetCurrentClassLogger();

        public PlaceRepository(string connectionString)
        {
            sqlConnection = new SqlConnection(connectionString);
        }


        public void CreatePlace(Place place)
        {
            Log("Begin creating Place in database. Place object", place);

            DatabaseConnector.PushCommandToDatabase(sqlConnection, CommandList.Build_CreatePlaceCommand(place));

            Log("End creating Place in database. Place object", place);
        }

        public void DeletePlace(Guid id)
        {
            Log("Begin deleting Place from database. Place ID = " + id);

            DatabaseConnector.PushCommandToDatabase(sqlConnection, CommandList.Build_DeletePlaceCommand(id));

            Log("End deleting Place from database. Place ID = " + id);
        }

        public Place GetPlace(Guid id)
        {
            Log("Begin receiving Place from database. Place ID = " + id);

            Place place = DatabaseConnector.GetDataFromDatabase<Place>
                (sqlConnection, CommandList.Build_GetPlaceCommand(id), new PlaceBuilder());

            Log("End receiving Place from database. Place object", place);

            return place;
        }

        public void UpdatePlaceInfo(Place place)
        {
            Log("Begin updating Place in database. Place object", place);

            DatabaseConnector.PushCommandToDatabase(sqlConnection, CommandList.Build_UpdatePlaceCommand(place));

            Log("End updating Place in database. Place object", place);
        }

        void Log(String logMessage)
        {
            _logger.Debug(logMessage);
        }

        void Log(String logMessage, Place place)
        {
            if (place != null)
            {
                _logger.Debug("{0}: ID = {1}, Address = {2}.", logMessage, place.Id, place.Address);
            }
            else
            {
                _logger.Debug("{0} is null.", logMessage);
            }
        }
    }
}
