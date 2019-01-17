CREATE PROCEDURE [TimeManagement].[SP_UpdateCustomerRef]
	@UpdateId  int,
	@Name nvarchar(100)
AS
	UPDATE [TimeManagement].[CustomerRef] 
	SET	[TimeManagement].[CustomerRef].[Name] = @Name
	WHERE [TimeManagement].[CustomerRef].[PK_Id] = @UpdateId

	GO