CREATE PROCEDURE [TimeManagement].[SP_UpdateUserAssignment]
	@UpdateId int,
	@UserId int,
	@ProjectId int
	
AS
	UPDATE [TimeManagement].[UserAssignment] 
	SET	
		[FK_UserId] = @UserId, 
		[FK_ProjectId] = @ProjectId
	WHERE [TimeManagement].[UserAssignment].[PK_Id] = @UpdateId

	GO
