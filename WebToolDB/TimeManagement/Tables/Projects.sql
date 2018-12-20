CREATE TABLE [TimeManagement].[Projects] (
	[PK_Id]            INT            IDENTITY (1, 1) NOT NULL,
	[Name]             NVARCHAR (150) NOT NULL,
	[FK_OrderNumber]              INT  NOT NULL,
	[TimeEstimation]   INT  NOT NULL,
	[FK_ProjectLeader] INT            NOT NULL DEFAULT 2,
	[Scope] NVARCHAR(1000) NOT NULL DEFAULT 'Undefined', 
	[FK_TimeType] INT NOT NULL DEFAULT 9, 
	[Status] INT NOT NULL DEFAULT 1, 
	[StartingDate] DATE NOT NULL DEFAULT getdate(), 
	[SiteOrVersion] NVARCHAR(50) NULL DEFAULT 'Not Assigned', 
	[FK_Country] INT NULL DEFAULT 1, 
	[FK_PlatformOrProduct] INT NULL DEFAULT 1, 
	[FK_Turbine] INT NULL DEFAULT 1, 
	CONSTRAINT [PK_Projects] PRIMARY KEY CLUSTERED ([PK_Id] ASC),
	CONSTRAINT [FK_Projects_Users] FOREIGN KEY ([FK_ProjectLeader]) REFERENCES [TimeManagement].[Users] ([PK_Id]),
	CONSTRAINT [FK_Projects_OrderNumber] FOREIGN KEY ([FK_OrderNumber]) REFERENCES [TimeManagement].[OrderNumber] ([PK_Id]),
	CONSTRAINT [FK_Projects_TimeType] FOREIGN KEY ([FK_TimeType]) REFERENCES [TimeManagement].[TimeType] ([PK_Id])
);

