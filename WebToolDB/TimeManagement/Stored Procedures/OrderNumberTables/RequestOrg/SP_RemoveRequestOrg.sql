CREATE PROCEDURE [TimeManagement].[SP_RemoveRequestOrg]
	@RemoveId int 

AS
	DELETE FROM [TimeManagement].[RequestOrg]
	where [TimeManagement].[RequestOrg].[PK_Id] = @RemoveId
GO
