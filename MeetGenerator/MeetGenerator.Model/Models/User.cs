using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MeetGenerator.Model.Models
{
    public class User : DataModel, ICloneable
    { 
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        //public Dictionary<string, string> SocialLinks { get; set; }

        public object Clone()
        {
            User user = new User();
            user.Id = Id;
            user.FirstName = FirstName;
            user.LastName = LastName;
            user.Email = Email;
            return user;
        }
        
    }
}