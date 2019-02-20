﻿CREATE TABLE [TimeManagement].[Users] (
    [PK_Id]   INT            IDENTITY (1, 1) NOT NULL,
    [NK_Name] NVARCHAR (100) NOT NULL,
    [NK_ZId]  NVARCHAR (50)  NOT NULL,
    [NK_Rights] SMALLINT NOT NULL DEFAULT ((3)), 
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([PK_Id] ASC),
	CONSTRAINT [UC_Users] UNIQUE ([NK_ZId])
);

