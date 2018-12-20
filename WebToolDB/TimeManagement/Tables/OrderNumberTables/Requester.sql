CREATE TABLE [TimeManagement].[Requester] (
    [PK_Id] INT           IDENTITY (1, 1) NOT NULL,
    [Name]  NVARCHAR (100) NOT NULL DEFAULT 'No Requester Assigned', 
    CONSTRAINT [PK_Requester] PRIMARY KEY ([PK_Id]),
);

