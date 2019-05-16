CREATE PROCEDURE [TimeManagement].[SP_UpdateOrderNumber]
	@UpdateId  int,
	@OrderNumberName nvarchar(1000),
	@title nvarchar(100)
AS
	UPDATE [TimeManagement].[OrderNumber] 
	SET	[TimeManagement].[OrderNumber].[Number] = @OrderNumberName,
		[TimeManagement].[OrderNumber].[Title] = @title
	WHERE [TimeManagement].[OrderNumber].[PK_Id] = @UpdateId

	GO