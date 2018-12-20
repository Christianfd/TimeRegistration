CREATE PROCEDURE [TimeManagement].[SP_RemoveTimeRegistration]
	@RemoveId int 

AS
	DELETE FROM [TimeManagement].[TimeRegistration]
	where [TimeManagement].[TimeRegistration].[PK_Id] = @RemoveId
GO
