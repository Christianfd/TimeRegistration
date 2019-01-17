CREATE TABLE [TimeManagement].[OrderNumber] (
    [PK_Id]          INT             IDENTITY (1, 1) NOT NULL,
    [Number]         NVARCHAR (1000) NOT NULL,
    [FK_RequestOrg]  INT             DEFAULT ((1)) NOT NULL,
    [FK_Requester]   INT             DEFAULT ((1)) NOT NULL,
    [FK_CustomerRef] INT             DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_OrderNumber] PRIMARY KEY CLUSTERED ([PK_Id] ASC),
    CONSTRAINT [FK_OrderNumber_CustomerRef] FOREIGN KEY ([FK_CustomerRef]) REFERENCES [TimeManagement].[CustomerRef] ([PK_Id]),
    CONSTRAINT [FK_OrderNumber_Requester] FOREIGN KEY ([FK_Requester]) REFERENCES [TimeManagement].[Requester] ([PK_Id]),
    CONSTRAINT [FK_OrderNumber_RequestOrg] FOREIGN KEY ([FK_RequestOrg]) REFERENCES [TimeManagement].[RequestOrg] ([PK_Id])
);


