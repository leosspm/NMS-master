CREATE PROCEDURE  [NMS].[UserLogin]
	@UserName NVARCHAR(50),
	@Password NVARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT	us.Id, 
			us.UserName, 
			us.Blocked
			--ct.Id				AS 'Contact_Id',
			--ct.FirstName		AS 'Contact_FirstName',
			--ct.LastName			AS 'Contact_LastName',
			--ct.Gender			AS 'Contact_Gender',
			--ct.Birthday			AS 'Contact_Birthday',
			--ct.NumberId			AS 'Contact_NumberId',
			--ct.Email1			AS 'Contact_Email1',
			--ct.Email2			AS 'Contact_Email2',
			--ct.Address1			AS 'Contact_Address1',
			--ct.Address2			AS 'Contact_Address2',
			--ct.PhoneNumber1		AS 'Contact_PhoneNumber1',
			--ct.PhoneNumber2		AS 'Contact_PhoneNumber2',
			--ct.Company			AS 'Contact_Company'
	FROM	 [NMS].[User] us
	WHERE	UserName = @UserName
	  AND	[Password] = @Password
	  AND	us.Blocked = 0
END;