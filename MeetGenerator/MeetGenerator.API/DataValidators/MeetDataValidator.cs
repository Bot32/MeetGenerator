using MeetGenerator.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetGenerator.API.DataValidators
{
    public class MeetDataValidator
    {
        public static List<string> IsCompleteValidMeetingObject(Meeting meeting)
        {
            List<string> errorsList = new List<string>();

            if (!IsMeetingObjectExist(meeting, errorsList)) return errorsList;
            IsValidDateTime(meeting.Date, errorsList);
            IsValidTitle(meeting.Title, errorsList);
            IsValidDesription(meeting.Description, errorsList);

            return errorsList;
        }

        public static bool IsMeetingObjectExist(Meeting meeting, List<string> errorsList)
        {
            if (meeting == null)
            {
                errorsList.Add("Meeting object does not exist.");
                return false;
            }
            return true;
        }

        public static bool IsValidTitle(String title, List<string> errorsList)
        {
            if (title == null)
            {
                errorsList.Add("Meeting title is null.");
                return false;
            }
            return true;
        }

        public static bool IsValidDesription(String description, List<string> errorsList)
        {
            if (description == null)
            {
                errorsList.Add("Meeting description is null.");
                return false;
            }
            return true;
        }

        public static bool IsValidDateTime(DateTime date, List<string> errorsList)
        {
            bool valid = true;

            if (date == null)
            {
                valid = false;
                errorsList.Add("Date is null.");
                return valid;
            }

            if (date < DateTime.Now)
            {
                valid = false;
                errorsList.Add("Incorrect date.");
            }

            return valid;
        }
    }
}