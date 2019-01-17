CREATE PROCEDURE [TimeManagement].[SP_TaskTypeFind]
	@TaskTypeID int 
AS
 SELECT [PK_Id], [Name] FROM [TaskType] WHERE [TaskType].[PK_Id] = @TaskTypeID