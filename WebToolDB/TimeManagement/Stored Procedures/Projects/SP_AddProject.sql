CREATE PROCEDURE [TimeManagement].[SP_AddProject]
	
	@ProjectName nvarchar(150),
	@OrderNumber int = 1,
	@TimeEstimation int,
	@FK_ProjectLeader int = 2,
	@Scope nvarchar(1000) = "Scope has not been set",
	@timeType int = 9
	
AS

INSERT INTO [TimeManagement].[Projects]
(	
	[Name], 
	[FK_OrderNumber], 
	[TimeEstimation], 
	[FK_ProjectLeader], 
	[Scope],
	[FK_TimeType]
)

VALUES (
	@ProjectName, 
	@OrderNumber ,
	@TimeEstimation, 
	@FK_ProjectLeader, 
	@Scope,
	@timeType
		) 
GO