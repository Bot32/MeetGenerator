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
    class InvitationBuilder : IBuilder<Invitation>
    {
        Logger _logger = LogManager.GetCurrentClassLogger();

        public Invitation Build(SqlDataReader reader)
        {
            Invitation invitation = null;

            _logger.Trace("Begin reading Invitation object from SqlDataReader.");

            if (reader.Read())
            {
                invitation = new Invitation
                {
                    MeetingID = reader.GetGuid(reader.GetOrdinal("MeetingID")),
                    UserID = reader.GetGuid(reader.GetOrdinal("UserID")),
                };
            }

            if (invitation != null)
            {
                _logger.Trace("End reading Invitation object from SqlDataReader. Invitation object: " +
                              "MeetingID = {0}, UserID = {1}.",
                              invitation.MeetingID, invitation.UserID);
            }
            else
            {
                _logger.Trace("End reading Invitation object from SqlDataReader. Invitation object is null.");
            }

            return invitation;
        }
    }
}
