select m.ID
from [dbo].[Meeting] m
join [dbo].[User] u on u.ID = m.OwnerID
where u.ID = @userId