using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetGenerator.Model.Models
{
    public class Place
    {
        public Guid Id { get; set; }
        public String Address { get; set; }
        public String Description { get; set; }
    }
}
