using MeetGenerator.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using NLog;

namespace MeetGenerator.Repository.SQL.Repositories.ObjectBuilders
{
    class PlaceBuilder : IBuilder<Place>
    {
        Logger _logger = LogManager.GetCurrentClassLogger();

        public Place Build(SqlDataReader reader)
        {
            Place place = null;

            _logger.Trace("Begin reading Place object from SqlDataReader.");

            if (reader.Read())
            {
                place = new Place
                {
                    Id = reader.GetGuid(reader.GetOrdinal("ID")),
                    Address = reader.GetString(reader.GetOrdinal("Address")),
                    Description = reader.GetString(reader.GetOrdinal("Description"))
                };
            }

            if (place != null)
            {
                _logger.Trace("End reading Place object from SqlDataReader. Place object: " + 
                              "ID = {0}, Address = {2}.", 
                              place.Id, place.Address);
            }
            else
            {
                _logger.Trace("End reading Place object from SqlDataReader. Place object is null.");
            }

            return place;
        }
    }
}
