using MeetGenerator.Model.Models;
using MeetGenerator.Model.Repositories;
using MeetGenerator.Repository.SQL.Repositories.ObjectBuilders;
using MeetGenerator.Repository.SQL.Repositories.Utility;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetGenerator.Repository.SQL.Repositories
{
    public class InvitationRepository : IInvitationRepository
    {
        SqlConnection sqlConnection;
        Logger _logger = LogManager.GetCurrentClassLogger();

        public InvitationRepository(string connectionString)
        {
            sqlConnection = new SqlConnection(connectionString);
        }


        public void Create(Invitation invitation)
        {
            Log("Begin creating Invitation User to Meeting in database.", invitation);

            DatabaseConnector.PushCommandToDatabase
                (sqlConnection, CommandList.Build_InviteUserToMeetingCommand(invitation));

            Log("End creating Invitation User to Meeting in database.", invitation);
        }

        public void Delete(Invitation invitation)
        {
            Log("Begin deleting Invitation User to Meeting in database.", invitation);

            DatabaseConnector.PushCommandToDatabase
                (sqlConnection, CommandList.Build_DeleteInvitationUserToMeetingCommand(invitation));

            Log("End deleting Invitation User to Meeting in database.", invitation);
        }

        public bool IsExist(Invitation invitation)
        {
            Log("Begin receiving Invitation User to Meeting in database.", invitation);

            Invitation resultInvitation =  DatabaseConnector.GetDataFromDatabase<Invitation>
                (sqlConnection, CommandList.Build_GetInvitationUserToMeetingCommand(invitation), new InvitationBuilder());

            return resultInvitation != null;

            Log("End receiving Invitation User to Meeting in database.", invitation);
        }

        void Log(string logMessage)
        {
            _logger.Debug(logMessage);
        }

        void Log(String logMessage, Invitation invitation)
        {
            if (invitation != null)
            {
                _logger.Debug("{0} MeetingID = {1}, UserID = {2}.",
                    logMessage, invitation.MeetingID, invitation.UserID);
            }
            else
            {
                _logger.Debug("{0} Invitation is null.", logMessage);
            }
        }
    }
}
