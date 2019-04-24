CREATE TABLE [TimeManagement].[Projects] (
    [PK_Id]                INT             IDENTITY (1, 1) NOT NULL,
    [Name]                 NVARCHAR (150)  NOT NULL,
    [FK_OrderNumber]       INT             NOT NULL,
    [TimeEstimation]       INT             NOT NULL,
    [FK_ProjectLeader]     INT             DEFAULT ((1)) NOT NULL,
    [Scope]                NVARCHAR (200)  DEFAULT ('Undefined') NOT NULL,
    [FK_TimeType]          INT             DEFAULT ((1)) NOT NULL,
    [Status]               INT             DEFAULT ((1)) NOT NULL,
    [StartingDate]         DATE            DEFAULT (getdate()) NOT NULL,
    [SiteOrVersion]        NVARCHAR (50)   DEFAULT ('Not Assigned') NOT NULL,
    [FK_Country]           INT             DEFAULT ((56)) NOT NULL,
    [FK_PlatformOrProduct] INT             DEFAULT ((1)) NOT NULL,
    [FK_Turbine]           INT             DEFAULT ((1)) NOT NULL,
    [ProjectComment] NVARCHAR(1000) NOT NULL DEFAULT ('No Comment'), 
    CONSTRAINT [PK_Projects] PRIMARY KEY CLUSTERED ([PK_Id] ASC),
    CONSTRAINT [FK_Projects_Country] FOREIGN KEY ([FK_Country]) REFERENCES [TimeManagement].[Country] ([PK_Id]),
    CONSTRAINT [FK_Projects_OrderNumber] FOREIGN KEY ([FK_OrderNumber]) REFERENCES [TimeManagement].[OrderNumber] ([PK_Id]),
    CONSTRAINT [FK_Projects_PlatformOrProduct] FOREIGN KEY ([FK_PlatformOrProduct]) REFERENCES [TimeManagement].[PlatformOrProduct] ([PK_Id]),
    CONSTRAINT [FK_Projects_TimeType] FOREIGN KEY ([FK_TimeType]) REFERENCES [TimeManagement].[TimeType] ([PK_Id]),
    CONSTRAINT [FK_Projects_Turbine] FOREIGN KEY ([FK_Turbine]) REFERENCES [TimeManagement].[Turbine] ([PK_Id]),
    CONSTRAINT [FK_Projects_Users] FOREIGN KEY ([FK_ProjectLeader]) REFERENCES [TimeManagement].[Users] ([PK_Id])
);



