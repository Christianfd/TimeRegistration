CREATE PROCEDURE [TimeManagement].[SP_UpdateComments]

	@UpdateId  int,
	@WeekNr int,
	@Year int,
	@Text nvarchar(1000),
	@ProjectId int,
	@UserId int
AS
	UPDATE [TimeManagement].[Comments] 
	SET	
		[WeekNr] = @WeekNr,
		[Year] = @Year,
		[TimeManagement].[Comments].[Text] = @Text,
		[FK_ProjectId] = @ProjectId,
		[FK_User] = @UserId
	WHERE [TimeManagement].[Comments].[PK_Id] = @UpdateId

	GO