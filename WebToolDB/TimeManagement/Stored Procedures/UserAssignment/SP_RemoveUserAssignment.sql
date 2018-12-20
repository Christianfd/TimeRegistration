CREATE PROCEDURE [TimeManagement].[SP_RemoveUserAssignment]
	@RemoveId int 

AS
	DELETE FROM [TimeManagement].[UserAssignment]
	where [TimeManagement].[UserAssignment].[PK_Id] = @RemoveId
GO
