CREATE TABLE [dbo].[MasterKey]
(
	[Key] NVARCHAR(128) NOT NULL PRIMARY KEY
)
GO

INSERT INTO [dbo].[MasterKey] ([Key]) VALUES ('This is a master key')
GO