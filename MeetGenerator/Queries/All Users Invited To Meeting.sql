select m.ID as MeetingID, i.UserID as UserID, u.FirstName, u.LastName, u.Email 
from dbo.Meeting m 
join dbo.Invitations i ON m.ID = i.MeetingID
join dbo.[User] u ON i.UserID = u.ID
