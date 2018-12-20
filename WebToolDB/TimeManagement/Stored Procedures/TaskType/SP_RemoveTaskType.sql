CREATE PROCEDURE [TimeManagement].[SP_RemoveTaskType]
	@RemoveId int 

AS
	DELETE FROM [TimeManagement].[TaskType]
	where [TimeManagement].[TaskType].[PK_Id] = @RemoveId
GO
