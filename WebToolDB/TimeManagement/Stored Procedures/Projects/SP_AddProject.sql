CREATE PROCEDURE [TimeManagement].[SP_AddProject]
	
	@ProjectName nvarchar(150),
	@OrderNumber int = 1,
	@TimeEstimation int,
	@FK_ProjectLeader int = 2,
	@Scope nvarchar(200) = 'No Scope',
	@timeType int = 9,
	@SiteOrVersion nvarchar(50) = 'Not Assigned',
	@FK_Country int = 1,
	@FK_PlatformOrProduct int = 1,
	@FK_Turbine int = 1,
	@ProjectComment nvarchar(1000) = 'No Comment'


	
AS

INSERT INTO [TimeManagement].[Projects]
(	
	[Name], 
	[FK_OrderNumber], 
	[TimeEstimation], 
	[FK_ProjectLeader], 
	[Scope],
	[FK_TimeType],
	[SiteOrVersion],
	[FK_Country],
	[FK_PlatformOrProduct],
	[FK_Turbine],
	[ProjectComment]
)

VALUES (
	@ProjectName, 
	@OrderNumber ,
	@TimeEstimation, 
	@FK_ProjectLeader, 
	@Scope,
	@timeType,
	@SiteOrVersion,
	@FK_Country,
	@FK_PlatformOrProduct,
	@FK_Turbine,
	@ProjectComment
		) 
GO