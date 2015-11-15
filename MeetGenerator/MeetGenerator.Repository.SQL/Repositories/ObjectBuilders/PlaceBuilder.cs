using MeetGenerator.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace MeetGenerator.Repository.SQL.Repositories.ObjectBuilders
{
    class PlaceBuilder : IBuilder<Place>
    {
        public Place Build(SqlDataReader reader)
        {
            if (reader.Read())
            {
                return new Place
                {
                    Id = reader.GetGuid(reader.GetOrdinal("ID")),
                    Address = reader.GetString(reader.GetOrdinal("Address")),
                    Description = reader.GetString(reader.GetOrdinal("Description"))
                };
            }
            return null;
        }
    }
}
