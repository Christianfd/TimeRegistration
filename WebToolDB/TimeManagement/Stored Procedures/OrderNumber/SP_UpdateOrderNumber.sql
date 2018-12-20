CREATE PROCEDURE [TimeManagement].[SP_UpdateOrderNumber]
	@UpdateId  int,
	@OrderNumberName nvarchar(1000)
AS
	UPDATE [TimeManagement].[OrderNumber] 
	SET	[TimeManagement].[OrderNumber].[Number] = @OrderNumberName
	WHERE [TimeManagement].[OrderNumber].[PK_Id] = @UpdateId

	GO