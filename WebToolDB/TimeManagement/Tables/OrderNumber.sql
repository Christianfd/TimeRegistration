CREATE TABLE [TimeManagement].[OrderNumber]
(
	  [PK_Id]	 INT  IDENTITY (1, 1) NOT NULL,
	  [Number]   NVARCHAR (1000) NOT NULL, 
    [FK_RequestOrg] INT NOT NULL DEFAULT 1, 
    [FK_Requester]	 INT NOT NULL DEFAULT 1, 
    [FK_CustomerRef] INT NOT NULL DEFAULT 1, 
    CONSTRAINT [PK_OrderNumber] PRIMARY KEY ([PK_Id])
)
