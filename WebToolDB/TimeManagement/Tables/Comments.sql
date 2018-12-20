CREATE TABLE [TimeManagement].[Comments] (
    [PK_Id]        INT            IDENTITY (1, 1) NOT NULL,
    [WeekNr]       INT            NOT NULL,
    [Year]         INT            NOT NULL,
    [Text]         NVARCHAR (1000) NOT NULL,
    [FK_ProjectId] INT            NOT NULL,
    [FK_User]      INT            NOT NULL,
    CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED ([PK_Id] ASC),
    CONSTRAINT [FK_Comments_Projects] FOREIGN KEY ([FK_ProjectId]) REFERENCES [TimeManagement].[Projects] ([PK_Id]),
    CONSTRAINT [FK_Comments_Users] FOREIGN KEY ([FK_User]) REFERENCES [TimeManagement].[Users] ([PK_Id])
);

