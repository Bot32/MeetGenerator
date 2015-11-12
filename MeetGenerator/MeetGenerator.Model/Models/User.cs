﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetGenerator.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Dictionary<string, string> SocialLinks { get; set; }
    }
}