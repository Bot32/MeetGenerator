using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetGenerator.Model.Models;

namespace MeetGenerator.Model.Repositories
{
    public interface IMeetingRepository
    {
        void CreateMeeting(Meeting meeting);
        Meeting GetMeeting(Guid meetingId);
        List<Meeting> GetAllMeetingsCreatedByUser(Guid userId);
        void UpdateMeetingInfo(Meeting meeting);
        void InviteUserToMeeting(Guid userId);
        void DeleteMeeting(Guid meetingId);
    }
}
