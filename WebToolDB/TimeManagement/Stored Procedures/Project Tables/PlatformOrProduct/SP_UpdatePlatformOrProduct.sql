CREATE PROCEDURE [TimeManagement].[SP_UpdatePlatformOrProduct]
	@UpdateId  int,
	@ProductName nvarchar(50)
AS
	UPDATE [TimeManagement].[PlatformOrProduct] 
	SET	[TimeManagement].[PlatformOrProduct].[ProductName] = @ProductName
	WHERE [TimeManagement].[PlatformOrProduct].[PK_Id] = @UpdateId

	GO