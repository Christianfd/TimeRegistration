CREATE PROCEDURE [TimeManagement].[SP_UpdateProject]
	@UpdateId  int,
	@ProjectName nvarchar(150),
	@OrderNumber int,
	@TimeEstimation int,
	@FK_ProjectLeader int,
	@Scope nvarchar(200),
	@timeType int,
	@SiteOrVersion nvarchar(50),
	@FK_Country int,
	@FK_PlatformOrProduct int,
	@FK_Turbine int,
	@ProjectComment nvarchar(1000),


	@FK_RequestOrg int,
	@FK_Requester int,
	@FK_CustomerRef int



AS
	UPDATE [TimeManagement].[Projects] 
	SET	[TimeManagement].[Projects].[Name] = @ProjectName,
		[FK_OrderNumber] = @OrderNumber,
		[TimeEstimation] = @TimeEstimation,
		[FK_ProjectLeader] = @FK_ProjectLeader,
		[Scope] = @Scope,
		[FK_TimeType] = @timeType,
		[SiteOrVersion] = @SiteOrVersion,
		[FK_Country] = @FK_Country,
		[FK_PlatformOrProduct] = @FK_PlatformOrProduct,
		[FK_Turbine] = @FK_Turbine,
		[ProjectComment] = @ProjectComment,


		FK_RequestOrg = @FK_RequestOrg,
		FK_Requester = @FK_Requester,
		FK_CustomerRef = @FK_CustomerRef
		
	WHERE [TimeManagement].[Projects].[PK_Id] = @UpdateId

GO
