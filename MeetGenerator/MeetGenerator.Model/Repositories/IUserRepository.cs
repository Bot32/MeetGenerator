using MeetGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetGenerator.Model.Repositories
{
    public interface IUserRepository
    {
        void Create(User user);
        User Get(Guid UserID);
    }
}
