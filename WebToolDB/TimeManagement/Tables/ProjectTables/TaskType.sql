CREATE TABLE [TimeManagement].[TaskType] (
    [PK_Id] INT           IDENTITY (1, 1) NOT NULL,
    [Name]  NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_TaskType] PRIMARY KEY CLUSTERED ([PK_Id] ASC)
);

