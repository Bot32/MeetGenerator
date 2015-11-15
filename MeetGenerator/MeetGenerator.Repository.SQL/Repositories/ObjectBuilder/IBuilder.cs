using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetGenerator.Repository.SQL.Repositories.ObjectBuilder
{
    public interface IBuilder<T>
    {
        T Build(SqlDataReader reader);
    }
}
