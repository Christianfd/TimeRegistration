CREATE TABLE [TimeManagement].[Country] (
    [PK_Id] INT           IDENTITY (1, 1) NOT NULL,
    [CountryName]  NVARCHAR (50) NOT NULL DEFAULT 'No Country Assigned', 
    [CountryCode] NVARCHAR(3) NOT NULL DEFAULT '-',
);

