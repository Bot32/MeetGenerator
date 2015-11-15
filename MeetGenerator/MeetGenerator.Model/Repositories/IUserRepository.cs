using MeetGenerator.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetGenerator.Model.Repositories
{
    public interface IUserRepository
    {
        void CreateUser(User user);
        User GetUser(Guid user);
        User GetUser(String email);
        void UpdateUserInfo(User user);
        void DeleteUser(Guid userId);
    }
}
