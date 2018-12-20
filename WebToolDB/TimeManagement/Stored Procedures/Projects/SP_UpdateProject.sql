CREATE PROCEDURE [TimeManagement].[SP_UpdateProject]
	@UpdateId  int,
	@ProjectName nvarchar(150),
	@OrderNumber int,
	@TimeEstimation int,
	@FK_ProjectLeader int,
	@Scope nvarchar(1000),
	@timeType int
	
AS
	UPDATE [TimeManagement].[Projects] 
	SET	[TimeManagement].[Projects].[Name] = @ProjectName,
		[FK_OrderNumber] = @OrderNumber,
		[TimeEstimation] = @TimeEstimation,
		[FK_ProjectLeader] = @FK_ProjectLeader,
		[Scope] = @Scope,
		[FK_TimeType] = @timeType
		
	WHERE [TimeManagement].[Projects].[PK_Id] = @UpdateId

GO
