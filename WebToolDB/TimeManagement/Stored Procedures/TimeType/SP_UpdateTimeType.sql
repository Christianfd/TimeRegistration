CREATE PROCEDURE [TimeManagement].[SP_UpdateTimeType]
	@UpdateId  int,
	@timeTypeName nvarchar(50)
AS
	UPDATE [TimeManagement].[TimeType] 
	SET	[TimeManagement].[Timetype].[Name] = @timeTypeName
	WHERE [TimeManagement].[TimeType].[PK_Id] = @UpdateId

	GO