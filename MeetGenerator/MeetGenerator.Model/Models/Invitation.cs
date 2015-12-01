using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetGenerator.Model.Models
{
    public class Invitation
    {
        public Guid MeetingID { get; set; }
        public Guid UserID { get; set; }
    }
}
