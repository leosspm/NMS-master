﻿SET IDENTITY_INSERT [NMS].[Role] ON
Go
MERGE INTO [NMS].[Role] AS TARGET
	USING (VALUES
		(1, 'Administrator'),
		(2, 'User')
	) AS SOURCE ([Id], [Label])
	ON TARGET.[Id] = SOURCE.[Id]	

WHEN MATCHED THEN UPDATE SET [Label] = Source.[Label]
WHEN NOT MATCHED BY TARGET THEN INSERT ([Id], [Label])
VALUES ([Id], [Label]);
Go
SET IDENTITY_INSERT [NMS].[Role] OFF