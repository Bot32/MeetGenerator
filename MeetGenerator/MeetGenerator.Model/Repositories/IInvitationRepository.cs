using MeetGenerator.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetGenerator.Model.Repositories
{
    public interface IInvitationRepository
    {
        void Create(Invitation invitation);

        bool IsExist(Invitation invitation);

        void Delete(Invitation invitation);
    }
}
