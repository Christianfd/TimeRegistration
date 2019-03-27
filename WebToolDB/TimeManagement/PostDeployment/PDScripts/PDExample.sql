/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

--IF NOT EXISTS(SELECT TOP(1) [CountryName] FROM TimeManagement.Country)
--BEGIN
--DBCC CHECKIDENT ('[TimeManagement].[Country]', RESEED, 1) 
--INSERT INTO[TimeManagement].[Country] ([CountryCode], [CountryName]) VALUES ( 'ZW', 'Zimbabwe');
--END