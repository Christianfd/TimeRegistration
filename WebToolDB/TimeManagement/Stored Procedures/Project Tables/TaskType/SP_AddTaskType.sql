CREATE PROCEDURE [TimeManagement].[SP_AddTaskType]
	
	@taskTypeName nvarchar(50)
AS

INSERT INTO [TimeManagement].[TaskType]
([Name])

VALUES (@taskTypeName)
