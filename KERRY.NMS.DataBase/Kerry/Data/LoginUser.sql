
SET IDENTITY_INSERT  [NMS].[User] ON
Go
MERGE INTO  [NMS].[User] AS TARGET
	USING (VALUES
		(1, 'admin', 'admin'),
		(2, 'user', 'user')
	) AS SOURCE ([Id], [UserName], [Password])
	ON TARGET.[Id] = SOURCE.[Id]	

WHEN MATCHED THEN UPDATE SET [UserName] = Source.[UserName], 
							 [Password] = Source.[Password]
WHEN NOT MATCHED BY TARGET THEN INSERT ([Id], [UserName], [Password]) 
VALUES ([Id], [UserName], [Password]);
Go
SET IDENTITY_INSERT  [NMS].[User] OFF



SET IDENTITY_INSERT  [NMS].[UserRole] ON
Go
MERGE INTO  [NMS].[UserRole] AS TARGET
	USING (VALUES
		(1, 1, 1, 1, 1)
	) AS SOURCE ([Id], [UserId], [RoleId], [SqlCreatedBy], [SqlUpdatedBy])
	ON TARGET.[Id] = SOURCE.[Id]	

WHEN MATCHED THEN UPDATE SET [UserId] = Source.[UserId], 
							 [RoleId] = Source.[RoleId]
WHEN NOT MATCHED BY TARGET THEN INSERT ([Id], [UserId], [RoleId], [SqlCreatedBy], [SqlUpdatedBy])
VALUES ([Id], [UserId], [RoleId], [SqlCreatedBy], [SqlUpdatedBy]);
Go
SET IDENTITY_INSERT  [NMS].[UserRole] OFF