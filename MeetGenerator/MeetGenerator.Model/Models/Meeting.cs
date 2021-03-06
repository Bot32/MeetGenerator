﻿using MeetGenerator.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetGenerator.Model.Models
{
    public class Meeting : DataModel
    {
        public Guid Id { get; set; }
        public User Owner { get; set; }
        public DateTime Date { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public Place Place { get; set; }
        public Dictionary<Guid, User> InvitedPeople { get; set; }
    }
}
