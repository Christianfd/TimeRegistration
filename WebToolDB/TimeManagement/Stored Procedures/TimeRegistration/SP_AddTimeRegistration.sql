CREATE PROCEDURE [TimeManagement].[SP_AddTimeRegistration]
	
	@UserId int,
	@ProjectId int,
	@OrderId int = 1,
	@TaskId int,
	@TimeRegistered int,
	@Date date,
	@DateEntry date,
	@Comment nvarchar(300) = "No Comment"
	
AS

INSERT INTO [TimeManagement].[TimeRegistration]
(	
	[FK_UserId], 
	[FK_ProjectId],
	[FK_OrderId], 
	[FK_TaskId], 
	[Time], 
	[Date],
	[DateEntry],
	[Comment]
)

VALUES (
	@UserId,
	@ProjectId,
	@OrderId,
	@TaskId,
	@TimeRegistered,
	@Date,
	@DateEntry,
	@Comment
		) 
GO