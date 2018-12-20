CREATE TABLE [TimeManagement].[CustomerRef] (
    [PK_Id] INT           IDENTITY (1, 1) NOT NULL,
    [Name]  NVARCHAR (100) NOT NULL DEFAULT 'No Reference Assigned', 
    CONSTRAINT [PK_CustomerRef] PRIMARY KEY ([PK_Id]),
);

