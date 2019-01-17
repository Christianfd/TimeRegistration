CREATE PROCEDURE [TimeManagement].[SP_RemoveTimeType]
	@RemoveId int 

AS
	DELETE FROM [TimeManagement].[TimeType]
	where [TimeManagement].[TimeType].[PK_Id] = @RemoveId
GO
