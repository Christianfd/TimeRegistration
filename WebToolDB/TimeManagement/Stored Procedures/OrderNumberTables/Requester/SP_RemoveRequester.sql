CREATE PROCEDURE [TimeManagement].[SP_RemoveRequester]
	@RemoveId int 

AS
	DELETE FROM [TimeManagement].[Requester]
	where [TimeManagement].[Requester].[PK_Id] = @RemoveId
GO
