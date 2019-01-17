CREATE PROCEDURE [TimeManagement].[SP_AddTimeType]
	
	@timeTypeName nvarchar(50)
AS

INSERT INTO [TimeManagement].[TimeType]
([Name])

VALUES (@timeTypeName)
