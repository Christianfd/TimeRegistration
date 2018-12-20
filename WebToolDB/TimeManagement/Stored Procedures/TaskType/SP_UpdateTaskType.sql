CREATE PROCEDURE [TimeManagement].[SP_UpdateTaskType]
	@UpdateId  int,
	@taskTypeName nvarchar(50)
AS
	UPDATE [TimeManagement].[TaskType] 
	SET	[TimeManagement].[Tasktype].[Name] = @taskTypeName
	WHERE [TimeManagement].[TaskType].[PK_Id] = @UpdateId

	GO