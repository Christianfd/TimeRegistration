CREATE TABLE [TimeManagement].[TimeRegistration] (
    [PK_Id]        INT  IDENTITY (1, 1) NOT NULL,
    [FK_UserId]    INT  NOT NULL,
	[FK_OrderId]   INT  NOT NULL DEFAULT 1,
    [FK_ProjectId] INT  NOT NULL,
    [FK_TaskId]    INT  NOT NULL,
    [Time]         INT  NOT NULL,
    [Date]         DATE NOT NULL,
    [DateEntry]    DATE NOT NULL,
    [Comment] NVARCHAR(300) NULL DEFAULT 'No Comment', 
    CONSTRAINT [PK_TimeRegistration] PRIMARY KEY CLUSTERED ([PK_Id] ASC),
    CONSTRAINT [FK_TimeRegistration_Projects] FOREIGN KEY ([FK_ProjectId]) REFERENCES [TimeManagement].[Projects] ([PK_Id]),
    CONSTRAINT [FK_TimeRegistration_TaskType] FOREIGN KEY ([FK_TaskId]) REFERENCES [TimeManagement].[TaskType] ([PK_Id]),
    CONSTRAINT [FK_TimeRegistration_TimeRegistration] FOREIGN KEY ([PK_Id]) REFERENCES [TimeManagement].[TimeRegistration] ([PK_Id]),
    CONSTRAINT [FK_TimeRegistration_Users] FOREIGN KEY ([FK_UserId]) REFERENCES [TimeManagement].[Users] ([PK_Id])
);

