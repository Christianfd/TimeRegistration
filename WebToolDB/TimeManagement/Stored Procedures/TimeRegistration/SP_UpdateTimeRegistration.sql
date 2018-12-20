CREATE PROCEDURE [TimeManagement].[SP_UpdateTimeRegistration]
	@UpdateId int,
	@UserId int,
	@ProjectId int,
	@OrderId int = 1,
	@TaskId int,
	@TimeRegistered int,
	@Date date,
	@DateEntry date,
	@Comment nvarchar(300) = "No Comment"
	
AS
	UPDATE [TimeManagement].[TimeRegistration] 
	SET	
		[FK_UserId] = @UserId, 
	[FK_ProjectId] = @ProjectId,
	[FK_OrderId] = @OrderId,  
	[FK_TaskId] = @TaskId, 
	[Time] = @TimeRegistered, 
	[Date] = @Date,
	[DateEntry] = @DateEntry,
	[Comment] = @Comment
	WHERE [TimeManagement].[TimeRegistration].[PK_Id] = @UpdateId

	GO
