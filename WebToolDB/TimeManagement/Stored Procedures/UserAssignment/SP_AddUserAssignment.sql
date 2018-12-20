CREATE PROCEDURE [TimeManagement].[SP_AddUserAssignment]
	
	@UserId int,
	@ProjectId int
	
AS

INSERT INTO [TimeManagement].[UserAssignment]
(	
	[FK_UserId], 
	[FK_ProjectId]

)

VALUES (
	@UserId,
	@ProjectId
		) 

GO