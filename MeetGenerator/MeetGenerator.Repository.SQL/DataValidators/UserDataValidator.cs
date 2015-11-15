using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using MeetGenerator.Model.Models;

namespace MeetGenerator.Repository.SQL.DataValidators
{
    public static class UserDataValidator
    {
        public static String IsCompleteValidUserObject(User user)
        {
            StringBuilder errorsList = new StringBuilder();

            IsUserObjectExist(user, errorsList);
            IsValidUserFirstName(user.FirstName, errorsList);
            IsValidEmail(user.Email, errorsList);

            if (errorsList.Length == 0) errorsList.Append("OK");

            return errorsList.ToString();
        }

        public static bool IsUserObjectExist(User user, StringBuilder errorsList)
        {
            if (user == null)
            {
                errorsList.AppendLine("User object does not exist.");
                return false;
            }

            return true;
        }

        public static bool IsValidUserFirstName(String userFirstName, StringBuilder errorsList)
        {
            bool valid = true;

            if (userFirstName == null)
            {
                valid = false;
                errorsList.AppendLine("User First Name is null.");
                return valid;
            }

            return valid;
        }

        public static bool IsValidEmail(String email, StringBuilder errorsList)
        {
            bool valid = true;

            String theEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                                   + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                                   + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            if (email == null)
            {
                valid = false;
                errorsList.AppendLine("User email is null.");
                return valid;
            }

            //валидатор не пропустил secondUser411@test.com, нужно найти новый

            //if (!Regex.IsMatch(email, theEmailPattern))
            //{
            //    valid = false;
            //    errorsList.AppendLine("Email " + email + " is invalid.");
            //} 

            return valid;
        }
    }
}
