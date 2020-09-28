CREATE PROCEDURE  [NMS].[InsertOrUpdateUser]
		@Id            INT,
        @UserName      NVARCHAR(50),
        @Password      NVARCHAR(50),
        @SqlUserId     INT
AS
BEGIN
	SET NOCOUNT ON;

    MERGE INTO  [NMS].[User] AS TARGET
	USING (VALUES
		(@Id, @UserName, @Password, @SqlUserId)
	) AS SOURCE ([Id], [UserName], [Password], [SqlUserId])
	ON TARGET.[Id] = SOURCE.[Id]	

	WHEN MATCHED THEN UPDATE SET	[UserName]			= Source.[UserName], 
									[Password]			= Source.[Password], 
									[SqlUpdatedBy]		= Source.[SqlUserId],
									[SqlUpdatedDate]	= SYSDATETIME()

	WHEN NOT MATCHED BY TARGET THEN INSERT ([UserName], [Password], [SqlCreatedBy], [SqlUpdatedBy])
									VALUES ([UserName], [Password], [SqlUserId], [SqlUserId]);
END;