CREATE PROCEDURE [TimeManagement].[SP_UpdateRequester]
	@UpdateId  int,
	@Name nvarchar(100)
AS
	UPDATE [TimeManagement].[Requester] 
	SET	[TimeManagement].[Requester].[Name] = @Name
	WHERE [TimeManagement].[Requester].[PK_Id] = @UpdateId

	GO