CREATE PROCEDURE [TimeManagement].[SP_RemovePlatformOrProduct]
	@RemoveId int 

AS
	DELETE FROM [TimeManagement].[PlatformOrProduct]
	where [TimeManagement].[PlatformOrProduct].[PK_Id] = @RemoveId
GO
