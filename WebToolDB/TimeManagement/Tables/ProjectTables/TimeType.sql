CREATE TABLE [TimeManagement].[TimeType] (
    [PK_Id] INT           IDENTITY (1, 1) NOT NULL,
    [Name]  NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_TimeType] PRIMARY KEY CLUSTERED ([PK_Id] ASC),
	CONSTRAINT [UC_TimeType] UNIQUE ([Name])
);

