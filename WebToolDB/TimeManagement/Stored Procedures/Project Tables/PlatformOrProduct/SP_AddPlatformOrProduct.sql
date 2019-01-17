CREATE PROCEDURE [TimeManagement].[SP_AddPlatformOrProduct]
	
	@platformOrProject nvarchar(50)


AS

INSERT INTO [TimeManagement].[PlatformOrProduct]
([ProductName])

VALUES (@platformOrProject)
GO