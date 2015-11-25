using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using MeetGenerator.Model.Models;

namespace MeetGenerator.DataValidators
{
    public static class UserDataValidator
    {
        public static List<string> IsCompleteValidUserObject(User user)
        {
            List<string> errorsList = new List<string>();

            if (!IsUserObjectExist(user, errorsList)) return errorsList;
            IsValidFirstName(user.FirstName, errorsList);
            IsValidLastName(user.LastName, errorsList);
            IsValidEmail(user.Email, errorsList);

            return errorsList;
        }

        public static bool IsUserObjectExist(User user, List<string> errorsList)
        {
            if (user == null)
            {
                errorsList.Add("User object does not exist.");
                return false;
            }
            return true;
        }

        public static bool IsValidFirstName(String userFirstName, List<string> errorsList)
        {
            if (userFirstName == null)
            {
                errorsList.Add("User First Name is null.");
                return false;
            }
            return true;
        }

        public static bool IsValidLastName(String userLastName, List<string> errorsList)
        {
            if (userLastName == null)
            {
                errorsList.Add("User Last Name is null.");
                return false;
            }
            return true;
        }

        public static bool IsValidEmail(String email, List<string> errorsList)
        {
            bool valid = true;

            String theEmailPattern = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$";

            if (email == null)
            {
                valid = false;
                errorsList.Add("User email is null.");
                return valid;
            }

            if (!Regex.IsMatch(email, theEmailPattern))
            {
                valid = false;
                errorsList.Add("Email " + email + " is invalid.");
            }

            return valid;
        }
    }
}
