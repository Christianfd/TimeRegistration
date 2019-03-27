CREATE PROCEDURE [TimeManagement].[SP_UpdateOrderNumber]
	@UpdateId  int,
	@OrderNumberName nvarchar(1000),
	@FK_RequestOrg int,
	@FK_Requester int,
	@FK_CustomerRef int,
	@title nvarchar(100)
AS
	UPDATE [TimeManagement].[OrderNumber] 
	SET	[TimeManagement].[OrderNumber].[Number] = @OrderNumberName,
		[TimeManagement].[OrderNumber].[FK_RequestOrg] = @FK_RequestOrg,
		[TimeManagement].[OrderNumber].[FK_Requester] = @FK_Requester,
		[TimeManagement].[OrderNumber].[FK_CustomerRef] = @FK_CustomerRef,
		[TimeManagement].[OrderNumber].[Title] = @title
	WHERE [TimeManagement].[OrderNumber].[PK_Id] = @UpdateId

	GO