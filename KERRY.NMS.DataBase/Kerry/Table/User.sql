﻿CREATE TABLE [NMS].[User]
(
	[Id] INT IDENTITY(1,1) NOT NULL,
    [UserName] NVARCHAR(50) NOT NULL,
    [Password] NVARCHAR(50) NOT NULL,
    [Blocked] BIT NOT NULL DEFAULT(0),
    [SqlCreatedDate] DATETIME NOT NULL DEFAULT(SYSDATETIME()),
    [SqlCreatedBy] INT,
    [SqlUpdatedDate] DATETIME NOT NULL DEFAULT(SYSDATETIME()),
    [SqlUpdatedBy] INT,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
    (
        [Id] ASC
    ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
)

GO

ALTER TABLE  [NMS].[User] WITH CHECK ADD CONSTRAINT [FK_SqlCreatedBy_UserId] FOREIGN KEY ([SqlCreatedBy]) REFERENCES  [NMS].[User]([Id])

GO

ALTER TABLE  [NMS].[User] WITH CHECK ADD CONSTRAINT [FK_SqlUpdatedBy_UserId] FOREIGN KEY ([SqlUpdatedBy]) REFERENCES  [NMS].[User]([Id])

GO
