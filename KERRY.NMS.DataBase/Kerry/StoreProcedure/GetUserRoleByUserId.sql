CREATE PROCEDURE  [NMS].[GetUserRoleByUserId]
	@UserId INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT	rl.Id
	  FROM	 [NMS].[UserRole] ur INNER JOIN  [NMS].[Role] rl
	    ON	ur.RoleId = rl.Id
	 WHERE	ur.UserId = @UserId
	   AND	rl.Active = 1
END;