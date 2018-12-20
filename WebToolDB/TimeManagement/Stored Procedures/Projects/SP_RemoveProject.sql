CREATE PROCEDURE [TimeManagement].[SP_RemoveProject]
	@RemoveId int 

AS
	DELETE FROM [TimeManagement].[Projects]
	where [TimeManagement].[Projects].[PK_Id] = @RemoveId
GO