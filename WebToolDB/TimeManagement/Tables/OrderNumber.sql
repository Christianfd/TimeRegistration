CREATE TABLE [TimeManagement].[OrderNumber] (
    [PK_Id]          INT             IDENTITY (1, 1) NOT NULL,
    [Number]         NVARCHAR (1000) NOT NULL,
	[Title]		     NVARCHAR (100)  DEFAULT ('Placeholder') NOT NULL,
    CONSTRAINT [PK_OrderNumber] PRIMARY KEY CLUSTERED ([PK_Id] ASC),
    
);


