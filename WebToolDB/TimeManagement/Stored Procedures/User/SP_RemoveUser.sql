CREATE PROCEDURE [TimeManagement].[SP_RemoveUser]
	@RemoveId int 

AS
	DELETE FROM [TimeManagement].[Users]
	where [TimeManagement].[Users].[PK_Id] = @RemoveId
GO
