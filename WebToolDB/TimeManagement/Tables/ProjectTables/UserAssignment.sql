CREATE TABLE [TimeManagement].[UserAssignment] (
    [PK_Id]        INT IDENTITY (1, 1) NOT NULL,
    [FK_UserId]    INT NOT NULL,
    [FK_ProjectId] INT NOT NULL,
    CONSTRAINT [PK_UserAssignment] PRIMARY KEY CLUSTERED ([PK_Id] ASC),
    CONSTRAINT [FK_UserAssignment_Projects] FOREIGN KEY ([FK_ProjectId]) REFERENCES [TimeManagement].[Projects] ([PK_Id]),
    CONSTRAINT [FK_UserAssignment_Users] FOREIGN KEY ([FK_UserId]) REFERENCES [TimeManagement].[Users] ([PK_Id])
);

