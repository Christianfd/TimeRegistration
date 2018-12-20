CREATE PROCEDURE [TimeManagement].[SP_UpdateUser]
	@UpdateId  int,
	@UserName nvarchar(100),
	@UserAuthentity nvarchar(50)
AS
	UPDATE [TimeManagement].[Users] 
	SET	[TimeManagement].[Users].[NK_Name] = @UserName,
		[TimeManagement].[Users].[NK_ZId] = @UserAuthentity
	WHERE [TimeManagement].[Users].[PK_Id] = @UpdateId

	GO