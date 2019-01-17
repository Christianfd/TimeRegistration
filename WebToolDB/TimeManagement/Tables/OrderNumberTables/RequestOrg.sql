CREATE TABLE [TimeManagement].[RequestOrg] (
    [PK_Id] INT           IDENTITY (1, 1) NOT NULL,
    [Organization]  NVARCHAR (100) NOT NULL DEFAULT 'No Org Assigned', 
    CONSTRAINT [PK_RequestOrg] PRIMARY KEY ([PK_Id]),
	CONSTRAINT [UC_Organization] UNIQUE ([Organization]),
);

