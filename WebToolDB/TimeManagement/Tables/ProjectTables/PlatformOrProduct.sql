CREATE TABLE [TimeManagement].[PlatformOrProduct] (
    [PK_Id] INT           IDENTITY (1, 1) NOT NULL,
    [ProductName]  NVARCHAR (50) NOT NULL DEFAULT '-', 
    CONSTRAINT [PK_PlatformOrProduct] PRIMARY KEY ([PK_Id]),
	CONSTRAINT [UC_PlatformOrProduct] UNIQUE ([ProductName]),
    
);

