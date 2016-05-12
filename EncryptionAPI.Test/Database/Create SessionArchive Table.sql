CREATE TABLE [dbo].[SessionArchiveEncryptionParameters] (
    [SessionId]  INT         NOT NULL,
    [FileTypeId] TINYINT     DEFAULT ((0)) NOT NULL,
    [IV]         BINARY (16) NOT NULL,
    [MAC]        BINARY (32) NOT NULL,
    PRIMARY KEY CLUSTERED ([SessionId] ASC, [FileTypeId] ASC), 
    CONSTRAINT [FK_SessionArchiveEncryptionParameters_SessionArchive] FOREIGN KEY ([SessionId]) REFERENCES [SessionArchive]([SessionId])
);

