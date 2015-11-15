select m.id as MeetingId, m.Date, m.Title, m.Description, m.PlaceID, p.Address, p.Description, m.c, u.FirstName, u.LastName, u.Email 
from [dbo].[Meeting] m
join [dbo].[User] u on u.ID = m.OwnerID
join [dbo].[Place] p on m.PlaceID = p.ID
where m.ID = @id