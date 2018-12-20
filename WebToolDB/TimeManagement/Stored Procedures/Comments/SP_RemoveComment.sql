CREATE PROCEDURE [TimeManagement].[SP_RemoveComment]
	@RemoveId int 

AS
	DELETE FROM [TimeManagement].[Comments]
	where [TimeManagement].[Comments].[PK_Id] = @RemoveId
GO

