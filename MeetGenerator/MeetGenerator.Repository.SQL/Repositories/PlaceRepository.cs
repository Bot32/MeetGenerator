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

namespace MeetGenerator.Repository.SQL.Repositories
{
    public class PlaceRepository : IPlaceRepository
    {
        SqlConnection sqlConnection;

        public PlaceRepository(string connectionString)
        {
            sqlConnection = new SqlConnection(connectionString);
        }


        public void CreatePlace(Place place)
        {
            DatabaseConnector.PushCommandToDatabase(sqlConnection, CommandList.Build_CreatePlaceCommand(place));
        }

        public void DeletePlace(Guid id)
        {
            //Чтобы удалить Place, нужно удалить все Meeting, где есть этот Place
            DatabaseConnector.PushCommandToDatabase(sqlConnection, CommandList.Build_DeletePlaceCommand(id));
        }

        public Place GetPlace(Guid id)
        {
            return DatabaseConnector.GetDataFromDatabase<Place>
                (sqlConnection, CommandList.Build_GetPlaceCommand(id), new PlaceBuilder());
        }

        public void UpdatePlaceInfo(Place place)
        {
            DatabaseConnector.PushCommandToDatabase(sqlConnection, CommandList.Build_UpdatePlaceCommand(place));
        }
    }
}
